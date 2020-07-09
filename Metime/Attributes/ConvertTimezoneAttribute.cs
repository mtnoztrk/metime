﻿using AspectInjector.Broker;
using Metime.Enums;
using Metime.Helpers;
using Metime.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Metime.Attributes
{
    /// <summary>
    /// Bu attribute, AspectInjector kütüphanesi yardımıyla, çağrıldığı sınıfın bütün methodlarının başına ve sonuna timezone çevrimi yapan 
    /// fonksiyonları compile timeda ekliyor. Methodların argümanları UTC'ye, sonuç objeleri Local'e çevriliyor. Bu argümanların ve objelerin
    /// çevrim yapan fonksiyonlar tarafından dikkate alınması için, <see cref="ITimezoneConvertible"/> interface'inden türemiş olması gerekiyor.
    /// <code></code> 
    /// Bazı senaryolarda servis methodları iç içe çağrılabiliyor, bu durumlarda, obje iç içe kaç methoddan geçerse geçsin timezone
    /// çevriminin sadece bir kere yapılması gerekiyor. <see cref="ITimezoneConvertible.Kind"/> ise objenin hedeflenen tipe (UTC/Local) sadece
    /// bir kere çevrilmesini garanti ediyor.
    /// <code></code>
    /// Timezone offset şimdilik configden okunuyor. Attribute'ün bunu okuyabilmesi için ve gelecekte DbContext'e erişebilmesi için static <see cref="ServiceLocator"/>
    /// yazıldı. Startup'ta <see cref="ICanGetOffset"/>'in ServiceLocator'a kaydedilmesi gerekiyor.
    /// <code></code>
    /// Metin
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(ConvertTimezoneAttribute))]
    public sealed class ConvertTimezoneAttribute : Attribute
    {
        // below approach is being used
        // https://github.com/pamidur/aspect-injector/issues/77#issuecomment-443518810
        // If there are any problems with async methods(deadlock etc) author suggessted below example too.
        // https://github.com/pamidur/aspect-injector/blob/master/samples/UniversalWrapper/UniversalWrapper.cs

        private static MethodInfo _asyncTimezoneConverter = typeof(ConvertTimezoneAttribute).GetMethod(nameof(WrapAsync), BindingFlags.NonPublic | BindingFlags.Static);
        private static MethodInfo _syncTimezoneConverter = typeof(ConvertTimezoneAttribute).GetMethod(nameof(WrapSync), BindingFlags.NonPublic | BindingFlags.Static);

        [Advice(Kind.Around, Targets = Target.Method)]
        public object TimezoneConverter(
            [Argument(Source.Target)] Func<object[], object> target,
            [Argument(Source.Arguments)] object[] args,
            [Argument(Source.ReturnType)] Type retType
            )
        {
            //TODO: sadece public methodlar icin calisir hale getir
            if (retType == typeof(void) || retType == typeof(Task)) // return type is not an object. method signature is void or Task
            {
                ConvertTimes(args, TimezoneFormat.UTC);
                return target(args);
            }

            if (typeof(Task).IsAssignableFrom(retType)) // return type is Task<object>. method signature is Task<object>
            {
                var syncResultType = retType.IsConstructedGenericType ? retType.GenericTypeArguments[0] : typeof(object);
                return _asyncTimezoneConverter.MakeGenericMethod(syncResultType).Invoke(this, new object[] { target, args });
            }
            else // return type is object. method signature is object
            {
                return _syncTimezoneConverter.MakeGenericMethod(retType).Invoke(this, new object[] { target, args });
            }
        }

        /// <summary>
        /// Converts times before and after service method call for sync methods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static T WrapSync<T>(Func<object[], object> target, object[] args)
        {
            ConvertTimes(args, TimezoneFormat.UTC);
            var result = (T)target(args);
            ConvertTimes(result, TimezoneFormat.Local);
            return result;
        }

        /// <summary>
        /// Converts time before and after service method call for async methods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static async Task<T> WrapAsync<T>(Func<object[], object> target, object[] args)
        {
            ConvertTimes(args, TimezoneFormat.UTC);
            var result = await (Task<T>)target(args);
            ConvertTimes(result, TimezoneFormat.Local);
            return result;
        }

        private static int GetOffset()
        {
            // multi-tenancy eklendiğinde configden timezone okumak yerine, ilgili tenantın timezonuna göre çevrim yapılacak. 
            // IOptions yerine DbContext kullanılacak.
            var service = ServiceLocator.GetService<ICanGetOffset>();
            return service.GetOffset();
        }

        private static DateTime ToLocal(DateTime utcDateTime)
        {
            return utcDateTime.AddMinutes(GetOffset());
        }

        private static TimeSpan ToLocal(TimeSpan utcTimeSpan)
        {
            var result = utcTimeSpan.Add(TimeSpan.FromMinutes(GetOffset()));
            if (result.Ticks > TimeSpan.TicksPerDay)
                result = result.Subtract(TimeSpan.FromDays(1));
            return result;
        }

        private static DateTime ToUTC(DateTime localDateTime)
        {
            return localDateTime.AddMinutes(-GetOffset());
        }

        private static TimeSpan ToUTC(TimeSpan localTimeSpan)
        {
            var result = localTimeSpan.Subtract(TimeSpan.FromMinutes(GetOffset()));
            if (result.Ticks < 0)
                result = result.Add(TimeSpan.FromDays(1));
            return result;
        }

        /// <summary>
        /// Recursive method that iterates through entire object and their DateTime and TimeSpan properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">Object that should be converted.</param>
        /// <param name="targetFormat">What is the targeted time format.</param>
        /// <param name="processedProperties">No need to fill it, this is used inside this function for keeping track of recursion.</param>
        private static void ConvertTimes<T>(T entity, TimezoneFormat targetFormat, List<string> processedProperties = null)
        {
            if (entity == null)
                return;

            // initialize processedProperties for recursive function
            if (processedProperties == null)
                processedProperties = new List<string>();

            var type = entity.GetType();

            if (IsObjectList(type))
            {
                foreach (var element in (IEnumerable)entity)
                {
                    ConvertTimes(element, targetFormat, processedProperties);
                }
            }
            else
            {
                var convertible = entity as ITimezoneConvertible;
                if (convertible == null || convertible.Kind == targetFormat)
                    return;

                var properties = type.GetProperties();
                foreach (var p in properties)
                {
                    // propertyNames list consists of already processed objects and lists so ReferenceLoop is dealt with.
                    // EF Core creates object that references each other when Include and ThenInclude is used together sometimes.
                    if (!processedProperties.Contains(p.Name) && !p.PropertyType.IsPrimitive) // primitive properties is not considered.
                    {
                        var attributes = p.GetCustomAttributes(true);
                        if (attributes.Any(c => c.GetType() == typeof(IgnoreTimezoneAttribute))) // Don't consider ignored properties.
                            continue;

                        if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                        {
                            var propValue = p.GetValue(entity, null);
                            if (propValue == null)
                                continue;

                            if (targetFormat == TimezoneFormat.UTC)
                                propValue = ToUTC((DateTime)propValue);
                            else
                                propValue = ToLocal((DateTime)propValue);

                            p.SetValue(entity, propValue);
                        }
                        else if (p.PropertyType == typeof(TimeSpan) || p.PropertyType == typeof(TimeSpan?))
                        {
                            var propValue = p.GetValue(entity, null);
                            if (propValue == null)
                                continue;

                            if (targetFormat == TimezoneFormat.UTC)
                                propValue = ToUTC((TimeSpan)propValue);
                            else
                                propValue = ToLocal((TimeSpan)propValue);

                            p.SetValue(entity, propValue);
                        }
                        else if (!p.PropertyType.Module.ScopeName.Contains("System") || // property is a class
                            IsObjectList(p.PropertyType)) // property is an object list
                        {
                            // add to the processed list
                            processedProperties.Add(p.Name);
                            // this line changes kind, before going into recursion. So if the child entity has reference to the parent
                            // entity, Parent doesn't get changed again.
                            ((ITimezoneConvertible)entity).Kind = targetFormat;
                            ConvertTimes(p.GetValue(entity, null), targetFormat, processedProperties);
                        }
                    }
                }
                // this line changes kind after looping entire class. If entity doesn't have any child entity, it still
                // has the correct kind.
                ((ITimezoneConvertible)entity).Kind = targetFormat;
            }
        }

        /// <summary>
        /// String is enumerable but not object list.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsObjectList(Type type)
        {
            return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
        }

    }
}
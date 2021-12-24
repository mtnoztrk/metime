# Metime
Helper framework for timezone calculations. Business services marked with special attribute is injected aspect injector in build time. All of the service methods are wrapped by timezone conversion logic. Parameters of the method are converted to `UTC`(if not already) and results of the method are converted to `Local`(if not already).



Special thanks to the [**AspectInjector**](https://github.com/pamidur/aspect-injector)
# Motivation
Well.. Enterprise applications's timezone management suck. There are probably some resources on different timezones and these resources should be viewed in their own timezones instead of one global timezone. This framework trying to ease the pain.
# Installation
Available on [NuGet](https://www.nuget.org/packages/Metime)

	PM> Install-Package Metime

`Startup.cs`
```csharp
using Metime.Extensions;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
    {
        ...
        services.AddMetime<DefaultResolver>();
        services.AddCustomResolver<CustomEntity, CustomEntityResolver>();
        ...
    }
    
    public void Configure(IApplicationBuilder app) 
    {
        ...
        app.UseMetime();
        ...
    }
}
```
# Important Notes

* If you are assigning UTC value to the property (like `CreatedAt`), you should do it inside service method so it doesn't converted to UTC again by mistake.
* Database entities should have `Kind = TimezoneFormat.UTC` and request DTOs should have `Kind = TimezoneFormat.Local` default values.
* You can add custom resolver per entity; however, properties must be marked with `[UseCustomResolver]` attribute as well.
* Date only properties can be marked with `[IgnoreTimezone]` attribute.


# Usage
Detailed example can be found in example project.

Services that marked with the `ConvertTimezone` will process all of the methods available. Their parameters (`DateTime` objects are not considered only objects implementing `ITimezoneConvertible` are) and result objects will be processed.
```csharp
using Metime.Attributes;

[ConvertTimezone]
public class BlogService : IBlogService 
{
    ...
}
```
Objects that needs to be processed needs to implement `ITimezoneConvertible`. Since datetimes are stored in UTC format, it's default value should be UTC. This way, queried results will have default UTC value.
```csharp
using Metime.Models;

public class Blog : ITimezoneConvertible
    {
        ...
        public DateTime PostedAt { get; set; }
        [NotMapped] public TimezoneFormat Kind { get; set; } = TimezoneFormat.UTC;
    }
```
# License
MIT License

Copyright (c) 2022 Metin ÖZTÜRK

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
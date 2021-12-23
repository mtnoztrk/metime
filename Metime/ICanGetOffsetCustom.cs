﻿namespace Metime
{
    public interface ICanGetOffsetCustom<T> where T : class, ITimezoneConvertible
    {
        /// <summary>
        /// gets offset
        /// </summary>
        /// <param name="rootEntity"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        int GetOffset(T rootEntity, string propertyName);
    }
}

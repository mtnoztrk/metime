﻿namespace Metime
{
    public interface IOffsetResolver
    {
        /// <summary>
        /// gets offset.
        /// </summary>
        /// <param name="rootEntity">root object being processed</param>
        /// <returns></returns>
        int GetOffset(object rootEntity);
    }
}

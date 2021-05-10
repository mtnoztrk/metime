namespace Metime
{
    public interface ITimezoneConvertible
    {
        /// <summary>
        /// This keeps track of the object, if it is converted to current timezone or not.
        /// </summary>
        TimezoneFormat Kind { get; set; }
    }
}

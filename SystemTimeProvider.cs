
namespace TimeManagement
{
    /// <summary>
    /// Класс для получения системного времени.
    /// </summary>
    public static class SystemTimeProvider
    {
        public static Time GetCurrentTime()
        {
            DateTime now = DateTime.Now;
            return new Time((byte)now.Hour, (byte)now.Minute);
        }
    }
}

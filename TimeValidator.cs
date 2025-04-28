
namespace TimeManagement
{
    /// <summary>
    /// Класс ввода времени, предоставляющий получение и валидацию времени от пользователя
    /// </summary>
    public static class TimeValidator
    {
        public static bool TryGetTime(string hourInput, string minuteInput, out Time result, out string error)
        {
            result = null;
            error = null;   //сообщение об ошибке
            byte hours;
            byte minutes;
            bool ok1 = byte.TryParse(hourInput, out hours);
            bool ok2 = byte.TryParse(minuteInput, out minutes);

            if (!ok1 || hours > 23)
            {
                error = "Часы должны быть целым числом от 0 до 23.";
                return false;
            }
            if (!ok2 || minutes > 59)
            {
                error = "Минуты должны быть целым числом от 0 до 59.";
                return false;
            }

            try
            {
                result = new Time(hours, minutes);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}

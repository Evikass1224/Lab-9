
namespace TimeManagement
{
    /// <summary>
    /// Класс для валидации входных данных, связанных с вводом минут.
    /// </summary>
    public static class MinuteInputValidator
    {
        public static bool TryParseMinutes(string input, out uint minutes, out string error)
        {
            minutes = 0;
            error = null;  //сообщение об ошибке
            bool ok = uint.TryParse(input, out minutes);
            if (!ok)
            {
                error = "Введите неотрицательное целое число минут.";
                return false;
            }
            return true;
        }
    }
}

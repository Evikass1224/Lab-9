
namespace TimeManagement
{
    /// <summary>
    /// Класс для работы с временем, включая вычитание минут, а также операции сравнения и преобразования.
    /// </summary>
    public class Time
    {
        private byte _hours;
        private byte _minutes;

        public byte Hours
        {
            get { return _hours; }
        }
        public byte Minutes
        {
            get { return _minutes; }
        }

        public Time(byte hours, byte minutes)
        {
            _hours = hours;
            _minutes = minutes;
        }

        //метод вычитания минут из времени
        public Time SubtractMinutes(uint min)
        {
            int totalMinutes = _hours * 60 + _minutes;
            totalMinutes = totalMinutes - (int)min;

            totalMinutes = (totalMinutes % 1440 + 1440) % 1440;

            //новые часы и минуты
            byte newHours = (byte)(totalMinutes / 60);
            byte newMinutes = (byte)(totalMinutes % 60);

            return new Time(newHours, newMinutes);
        }

        //унарный оператор для обнуления часов и минут
        public static Time operator -(Time time)
        {
            return new Time(0, 0);
        }

        //унарный оператор для обнуления минут
        public static Time operator --(Time time)
        {
            return new Time(time._hours, 0);
        }

        //неявная операция (минуты отбрасываются)
        public static implicit operator byte(Time time)
        {
            return time._hours;
        }

        //явная операция (true - если часы и минуты не равны нулю)
        public static explicit operator bool(Time time)
        {
            return (time._hours != 0 && time._minutes != 0);
        }

        //бинарная операция ==
        public static bool operator ==(Time left, Time right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }
            return (left._minutes == right._minutes) && (left._hours == right._hours);
        }

        //бинарная операция !=
        public static bool operator !=(Time left, Time right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{_hours:D2}:{_minutes:D2}";
        }
    }
}

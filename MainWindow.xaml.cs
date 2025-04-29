using System.Windows;

namespace TimeManagement
{
    /// <summary>
    /// Класс окна приложения, который управляет вводом времени и его обработкой.
    /// </summary>
    public partial class MainWindow : Window
    {
        private Time _currentTime;
        private Time _originalTime;

        public MainWindow()
        {
            InitializeComponent();
            timeKeyboard.Checked += TimeEntryWindowActivity;
            systemTime.Checked += TimeEntryWindowActivity;
            presentTime.Text = "--:--";
            _currentTime = null;
            _originalTime = null;
        }

        private void TimeEntryWindowActivity(object sender, RoutedEventArgs e)
        {
            bool showInput = timeKeyboard.IsChecked == true;
            panelTimeInput.IsEnabled = showInput;
            inputHours.IsEnabled = showInput;
            inputMinutes.IsEnabled = showInput;
        }

        /// <summary>
        /// Устанавливает текущее время в зависимости от выбранного ввода времени.
        /// Если выбрано системное время, оно считывается и отображается в интерфейсе.
        /// Если выбрано пользовательское время, проверяется ввод пользователя и устанавливается введённое время.
        /// </summary>
        /// <param name="sender">объект, инициировавший событие</param>
        /// <param name="e">событие, содержащее данные о событии</param>
        private void SettingTheTime(object sender, RoutedEventArgs e)
        {
            if (systemTime.IsChecked == true)
            {
                _originalTime = SystemTimeProvider.GetCurrentTime();
                _currentTime = _originalTime;
                inputHours.Text = _currentTime.Hours.ToString();
                inputMinutes.Text = _currentTime.Minutes.ToString();
                presentTime.Text = _currentTime.ToString();
            }
            else
            {
                string hourInput = inputHours.Text.Trim();
                string minuteInput = inputMinutes.Text.Trim();

                if (TimeValidator.TryGetTime(hourInput, minuteInput, out var t, out var error))
                {
                    _originalTime = t;
                    _currentTime = _originalTime;
                    presentTime.Text = _currentTime.ToString();
                }
                else
                {
                    var customDialog = new CustomMessageBox(error);
                    customDialog.ShowDialog();
                }
            }
            deductionOutput.Text = "";
        }

        private void SubtractionOfMinutes(object sender, RoutedEventArgs e)
        {
            deductionOutput.Text = "";

            if (_originalTime == null)
            {
                var customDialog = new CustomMessageBox("Сначала задайте исходное время.");
                customDialog.ShowDialog();
                return;
            }

            string input = minutesSubtract.Text.Trim();

            if (!MinuteInputValidator.TryParseMinutes(input, out var mins, out var error))
            {
                var customDialog = new CustomMessageBox(error);
                customDialog.ShowDialog();
                return;
            }

            try
            {
                var resultTime = _originalTime.SubtractMinutes(mins);
                deductionOutput.Text = resultTime.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OutputResultsOverTime(object sender, RoutedEventArgs e)
        {
            if (_currentTime == null)
            {
                var customDialog = new CustomMessageBox("Сначала задайте исходное время.");
                customDialog.ShowDialog();
                return;
            }

            _currentTime = _originalTime;

            byte hoursAsByte = _currentTime;
            bool isNotZero = (bool)_currentTime;
            Time zeroTime = -_currentTime;
            Time decrementedTime = --_currentTime;

            outputResult.Text =
                $"Количество часов: {hoursAsByte} (часы как байты)\n";
                if (isNotZero)
                {
                    outputResult.Text += "Время не содержит нулевых минут или нулевых часов.\n";
                }
                else
                {
                    outputResult.Text += "Время содержит нулевые минуты или нулевые часы.\n";
                }
            outputResult.Text += $"Время после обнуления: {zeroTime}\n" +
                              $"Время без минут: {decrementedTime}\n";
        }

        private void EqualityCheck(object sender, RoutedEventArgs e)
        {
            outputComparisonResult.Text = "";

            if (!TimeValidator.TryGetTime(hoursTime1.Text.Trim(), minutesTime1.Text.Trim(), out var time1, out var error))
            {
                var customDialog = new CustomMessageBox(error);
                customDialog.ShowDialog();
                return;
            }

            if (!TimeValidator.TryGetTime(hoursTime2.Text.Trim(), minutesTime2.Text.Trim(), out var time2, out error))
            {
                var customDialog = new CustomMessageBox(error);
                customDialog.ShowDialog();
                return;
            }

            bool areEqual = time1 == time2;
            bool areNotEqual = time1 != time2;

            string equalMessage;     //сообщение о равенстве
            string notEqualMessage;  //сообщение о неравенстве

            if (areEqual)
            {
                equalMessage = "Время 1 равно Время 2 - верно";
            }
            else
            {
                equalMessage = "Время 1 равно Время 2 - не верно";
            }

            if (areNotEqual)
            {
                notEqualMessage = "Время 1 не равно Время 2 - верно";
            }
            else
            {
                notEqualMessage = "Время 1 не равно Время 2 - не верно";
            }

            outputComparisonResult.Text =
                $"Время 1: {time1}\n" +
                $"Время 2: {time2}\n\n" +
                $"Сравнение:\n\n" +
                $"{equalMessage}\n" +
                $"{notEqualMessage}";
        }
    }
}
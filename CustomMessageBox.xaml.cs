using System.Windows;

namespace TimeManagement
{
    /// <summary>
    /// Класс для создания пользовательского окна сообщения.
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message)
        {
            InitializeComponent();
            txtMessage.Text = message;
        }

        /// <summary>
        /// Обработчик события клика по кнопке "ок". Устанавливает результат диалога в true и закрывает окно.
        /// </summary>
        /// <param name="sender">объект, инициировавший событие</param>
        /// <param name="e">событие, содержащее данные о событии</param>
        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;  //результат выполнения диалогового окна
            Close();
        }
    }
}

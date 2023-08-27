using System.Windows.Input;

namespace ModernWPF_MVVM.Views.Window
{
    /// <summary>
    /// Логика взаимодействия для AddNewPersonWindow.xaml
    /// </summary>
    public partial class AddNewPersonWindow : System.Windows.Window
    {
        public AddNewPersonWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}

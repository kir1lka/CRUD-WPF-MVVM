using System.Windows.Input;

namespace ModernWPF_MVVM.Views.Window
{
    /// <summary>
    /// Логика взаимодействия для EditPersonWindow.xaml
    /// </summary>
    public partial class EditPersonWindow : System.Windows.Window
    {
        public EditPersonWindow()
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

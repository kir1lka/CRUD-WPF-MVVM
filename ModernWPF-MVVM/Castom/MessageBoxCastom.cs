using ModernWPF_MVVM.ViewModels;
using ModernWPF_MVVM.Views.Window;
using System.Windows.Media;

namespace ModernWPF_MVVM.Castom
{
    internal class MessageBoxCastom
    {

        public static void SuccessWithColoredName(string startText, string name, Brush nameColor, string additionalText)
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
            messageBoxViewModel.StartText = startText;
            messageBoxViewModel.Name = name;
            messageBoxViewModel.NameColor = nameColor;
            messageBoxViewModel.AdditionalText = additionalText;

            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.DataContext = messageBoxViewModel;
            messageBoxWindow.Show();
        }
        public static void ErrorMessage(string startText)
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
            messageBoxViewModel.StartText = startText;
            messageBoxViewModel.Name = "";
            messageBoxViewModel.NameColor = Brushes.Red;
            messageBoxViewModel.AdditionalText = "";

            MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
            messageBoxWindow.DataContext = messageBoxViewModel;
            messageBoxWindow.Show();
        }
        //public static void Success(string text)
        //{
        //    MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
        //    messageBoxViewModel.Name = text;
        //    MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
        //    messageBoxWindow.DataContext = messageBoxViewModel;
        //    messageBoxWindow.Show();
        //}
        //public static void MessageCastomError(string text)
        //{
        //    MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel();
        //    messageBoxViewModel.Label = text;
        //    MessageBoxWindow messageBoxWindow = new MessageBoxWindow();
        //    messageBoxWindow.DataContext = messageBoxViewModel;
        //    messageBoxWindow.Show();
        //}
    }
}

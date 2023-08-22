using ModernWPF_MVVM.Commands;
using ModernWPF_MVVM.ViewModels.Base;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernWPF_MVVM.ViewModels
{
    internal class MessageBoxViewModel : ViewModel
    {
        /// 
        /// Свойства
        /// 

        #region Label
        private string _label;
        public string Label
        {
            get => _label;
            set { Set(ref _label, value); }
        }

        #endregion

        #region StartText
        private string _startText;
        public string StartText
        {
            get { return _startText; }
            set { Set(ref _startText, value); }
        }
        #endregion

        #region Name
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
        #endregion

        #region NameColor
        private Brush _nameColor = Brushes.Red;
        public Brush NameColor
        {
            get { return _nameColor; }
            set { Set(ref _nameColor, value); }
        }
        #endregion

        #region AdditionalText
        private string _additionalText;
        public string AdditionalText
        {
            get { return _additionalText; }
            set { Set(ref _additionalText, value); }
        }
        #endregion

        /// 
        /// Команды
        /// 

        #region OkButtonCommand
        public ICommand OkButtonCommand { get; }

        private void OnExecuteOkButtonCommand(object p)
        {
            Window window = p as Window;
            window.Close();
        }

        private bool CanExecuteOkButtonCommand(object p) => true;


        #endregion


        public MessageBoxViewModel()
        {
            OkButtonCommand = new LambdaCommand(OnExecuteOkButtonCommand, CanExecuteOkButtonCommand);
        }
    }
}

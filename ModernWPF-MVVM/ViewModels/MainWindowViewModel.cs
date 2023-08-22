using ModernWPF_MVVM.Castom;
using ModernWPF_MVVM.Commands;
using ModernWPF_MVVM.Models;
using ModernWPF_MVVM.Repositories;
using ModernWPF_MVVM.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernWPF_MVVM.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region пример
        private string _textBlock;

        public string TextBlock
        {
            get { return _textBlock; }
            set { Set(ref _textBlock, value); }
        }
        #endregion

        /// 
        /// Поля
        /// 

        public ObservableCollection<Person> Persons { get; }

        /// 
        /// Свойства
        /// 

        #region WindowState
        private WindowState _windowState;

        public WindowState WindowState
        {
            get { return _windowState; }
            set { Set(ref _windowState, value); }
        }
        #endregion

        #region MaxHeight
        private double _maxHeight;

        public double MaxHeight
        {
            get { return _maxHeight; }
            set { Set(ref _maxHeight, value); }
        }
        #endregion

        #region SelectedItemGrid
        private Person _selectedItemGrid;

        public Person SelectedItemGrid
        {
            get { return _selectedItemGrid; }
            set { Set(ref _selectedItemGrid, value); }
        }
        #endregion

        /// 
        /// Команды
        /// 

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecute(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        #endregion

        #region MinimizeApplicationCommand
        public ICommand MinimizeApplicationCommand { get; }

        private void OnMinimizeApplicationCommandExecute(object p)
        {
            if (WindowState == WindowState.Normal) WindowState = WindowState.Minimized;
            //else if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            //else WindowState = WindowState.Normal;

        }

        private bool CanMinimizeApplicationCommandExecute(object p) => true;
        #endregion

        #region NormMaxApplicationCommand
        public ICommand NormMaxApplicationCommand { get; }

        private void OnNormMaxApplicationCommandExecute(object p)
        {
            if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;

        }

        private bool CanNormMaxApplicationCommandExecute(object p) => true;
        #endregion

        #region AddPersonCommand
        public ICommand AddPersonCommand { get; }

        private void OnAddPersonCommandExecute(object p)
        {
            var personNew = new Person
            {
                Id = 10,
                Name = "123",
                Adress = "123",
                Number = "123",
            };

            Persons.Add(personNew);

            MessageBoxCastom.SuccessWithColoredName("Пользователь ", personNew.Name.Trim(), Brushes.Red, " добавлен!");
        }

        private bool CanAddPersonCommandExecute(object p) => true;
        #endregion

        #region DeletePersonCommand
        public ICommand DeletePersonCommand { get; }

        private void OnDeletePersonCommandExecute(object p)
        {
            if (!(p is Person person)) return;

            MessageBoxCastom.SuccessWithColoredName("Пользователь ", person.Name.Trim(), Brushes.Red, " удален!");

            var index_group = Persons.IndexOf(person);
            Persons.Remove(person);

        }

        private bool CanDeletePersonCommandExecute(object p) => true;
        #endregion



        public MainWindowViewModel()
        {
            //заполнение таблицы
            UserRepository userRepository = new UserRepository();
            Persons = new ObservableCollection<Person>(userRepository.GetAllUsers());

            //команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
            MinimizeApplicationCommand = new LambdaCommand(OnMinimizeApplicationCommandExecute, CanMinimizeApplicationCommandExecute);
            NormMaxApplicationCommand = new LambdaCommand(OnNormMaxApplicationCommandExecute, CanNormMaxApplicationCommandExecute);

            AddPersonCommand = new LambdaCommand(OnAddPersonCommandExecute, CanAddPersonCommandExecute);
            DeletePersonCommand = new LambdaCommand(OnDeletePersonCommandExecute, CanDeletePersonCommandExecute);

            //панель задач не скрывается
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }


    }
}

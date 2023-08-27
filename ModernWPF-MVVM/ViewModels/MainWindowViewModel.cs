﻿using ModernWPF_MVVM.Castom;
using ModernWPF_MVVM.Commands;
using ModernWPF_MVVM.Models;
using ModernWPF_MVVM.Repositories;
using ModernWPF_MVVM.ViewModels.Base;
using ModernWPF_MVVM.Views.Window;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
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

        #region SearchValue
        private string _searchValue;

        public string SearchValue
        {
            get { return _searchValue; }
            set
            {
                Set(ref _searchValue, value);
                PersonCollection.Filter = FilterByName;
            }
        }
        #endregion

        #region PersonCollection-ICollectionView
        private ICollectionView _personCollection;

        public ICollectionView PersonCollection
        {
            get { return _personCollection; }
            set { Set(ref _personCollection, value); }
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
            AddNewPersonViewModel addNewPersonViewModel = new AddNewPersonViewModel();
            addNewPersonViewModel.PersonAdded += AddNewPersonViewModel_PersonAdded;
            AddNewPersonWindow addNewPersonWindow = new AddNewPersonWindow
            {
                DataContext = addNewPersonViewModel
            };
            addNewPersonWindow.ShowDialog();

        }

        private bool CanAddPersonCommandExecute(object p) => true;
        #endregion

        #region DeletePersonCommand
        public ICommand DeletePersonCommand { get; }

        private void OnDeletePersonCommandExecute(object p)
        {
            if (!(p is Person person)) return;

            // Удаление записи о пользователе из базы данных
            UserRepository userRepository = new UserRepository();
            userRepository.DeleteUser(person.Id);

            Persons.Remove(person);

            MessageBoxCastom.SuccessWithColoredName("Пользователь ", person.Name.Trim(), Brushes.Red, " удален!");
        }

        private bool CanDeletePersonCommandExecute(object p) => true;
        #endregion



        public MainWindowViewModel()
        {
            //заполнение таблицы
            UserRepository userRepository = new UserRepository();
            Persons = new ObservableCollection<Person>(userRepository.GetAllUsers());
            PersonCollection = CollectionViewSource.GetDefaultView(Persons);

            //поиск
            PersonCollection.Filter = FilterByName;

            //команды
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
            MinimizeApplicationCommand = new LambdaCommand(OnMinimizeApplicationCommandExecute, CanMinimizeApplicationCommandExecute);
            NormMaxApplicationCommand = new LambdaCommand(OnNormMaxApplicationCommandExecute, CanNormMaxApplicationCommandExecute);

            AddPersonCommand = new LambdaCommand(OnAddPersonCommandExecute, CanAddPersonCommandExecute);
            DeletePersonCommand = new LambdaCommand(OnDeletePersonCommandExecute, CanDeletePersonCommandExecute);

            //панель задач не скрывается при WindowState.Maximized
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        #region Methods
        private bool FilterByName(object obj)
        {
            if (!string.IsNullOrEmpty(SearchValue) && obj is Person person) return person != null && person.Name.ToLower().Contains(SearchValue.ToLower());
            return true;
        }
        private void AddNewPersonViewModel_PersonAdded(object sender, Person person)
        {
            Persons.Add(person);
            PersonCollection.Refresh();
        }
        #endregion
    }
}

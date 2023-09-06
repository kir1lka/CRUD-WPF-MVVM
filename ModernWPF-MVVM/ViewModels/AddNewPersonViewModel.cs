using ModernWPF_MVVM.Castom;
using ModernWPF_MVVM.Commands;
using ModernWPF_MVVM.Models;
using ModernWPF_MVVM.Repositories;
using ModernWPF_MVVM.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernWPF_MVVM.ViewModels
{
    internal class AddNewPersonViewModel : ViewModel
    {
        /// 
        /// Свойства
        /// 

        #region LabelTextError
        private string _labelTextError;

        public string LabelTextError
        {
            get { return _labelTextError; }
            set { Set(ref _labelTextError, value); }
        }
        #endregion

        #region NameValue
        private string _nameValue;

        public string NameValue
        {
            get { return _nameValue; }
            set { Set(ref _nameValue, value); }
        }
        #endregion

        #region AdressValue
        private string _adressValue;

        public string AdressValue
        {
            get { return _adressValue; }
            set { Set(ref _adressValue, value); }
        }
        #endregion

        #region NumberValue
        private string _numberValue;

        public string NumberValue
        {
            get { return _numberValue; }
            set { Set(ref _numberValue, value); }
        }
        #endregion

        #region DescriptionValue
        private string _descriptionValue;

        public string DescriptionValue
        {
            get { return _descriptionValue; }
            set { Set(ref _descriptionValue, value); }
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

        #region AddPersonCommand
        public ICommand AddPersonCommand { get; }

        private void OnAddPersonCommandExecute(object p)
        {
            if (_nameValue != null && _adressValue != null && _numberValue != null && _descriptionValue != null)
            {
                if (!(_nameValue == "" && _adressValue == "" && _numberValue == "" && _descriptionValue == ""))
                {
                    var personNew = new Person
                    {
                        Name = _nameValue,
                        Adress = _adressValue,
                        Number = _numberValue,
                        Description = _descriptionValue,
                    };

                    // новая запись в бд
                    UserRepository userRepository = new UserRepository();
                    userRepository.AddUser(personNew);

                    //вызываем событие добавление person
                    OnPersonAdded(personNew);

                    MessageBoxCastom.SuccessWithColoredName("Пользователь ", personNew.Name.Trim(), Brushes.Green, " добавлен!");

                    //закрытие окна
                    Window window = p as Window;
                    window.Close();
                }
                else
                    LabelTextError = "Заполните поля*";
            }
            else
                LabelTextError = "Заполните поля*";
        }

        private bool CanAddPersonCommandExecute(object p) => true;
        #endregion

        /// 
        /// Ивенты
        /// 

        #region PersonAdded
        public event EventHandler<Person> PersonAdded;

        private void OnPersonAdded(Person person)
        {
            PersonAdded?.Invoke(this, person);
        }
        #endregion

        public AddNewPersonViewModel()
        {
            OkButtonCommand = new LambdaCommand(OnExecuteOkButtonCommand, CanExecuteOkButtonCommand);
            AddPersonCommand = new LambdaCommand(OnAddPersonCommandExecute, CanAddPersonCommandExecute);
        }

    }
}

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
    internal class EditPersonViewModel : ViewModel
    {
        /// 
        /// Свойства
        /// 

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

        #region PersonToEdit
        private Person _personToEdit;

        public Person PersonToEdit
        {
            get { return _personToEdit; }
            set
            {
                _personToEdit = value;
                NameValue = _personToEdit.Name;
                AdressValue = _personToEdit.Adress;
                NumberValue = _personToEdit.Number;
            }
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

        #region EditPersonCommand
        public ICommand EditPersonCommand { get; }

        private void OnEditPersonCommandExecute(object p)
        {
            // Примените изменения к редактируемому пользователю
            PersonToEdit.Name = NameValue;
            PersonToEdit.Adress = AdressValue;
            PersonToEdit.Number = NumberValue;

            // Обновление данных в базе данных
            UserRepository userRepository = new UserRepository();
            userRepository.UpdateUser(PersonToEdit);

            // Вызов события, чтобы оповестить о редактировании пользователя
            OnPersonEdited(PersonToEdit);

            MessageBoxCastom.SuccessWithColoredName("Пользователь ", PersonToEdit.Name.Trim(), Brushes.Green, " отредактирован!");

            //закрытие окна
            Window window = p as Window;
            window.Close();
        }

        private bool CanEditPersonCommandExecute(object p) => true;
        #endregion

        ///
        /// events
        /// 

        #region PersonEdited
        public event EventHandler<Person> PersonEdited;

        private void OnPersonEdited(Person person)
        {
            PersonEdited?.Invoke(this, person);
        }
        #endregion

        public EditPersonViewModel()
        {
            OkButtonCommand = new LambdaCommand(OnExecuteOkButtonCommand, CanExecuteOkButtonCommand);
            EditPersonCommand = new LambdaCommand(OnEditPersonCommandExecute, CanEditPersonCommandExecute);
        }

    }
}

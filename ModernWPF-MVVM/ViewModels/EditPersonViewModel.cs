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

        #region LabelTextError
        private string _labelTextError;

        public string LabelTextError
        {
            get { return _labelTextError; }
            set { Set(ref _labelTextError, value); }
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
                DescriptionValue = _personToEdit.Description;
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
            if (_nameValue != null && _adressValue != null && _numberValue != null && _descriptionValue != null)
            {
                if (!(_nameValue == "" && _adressValue == "" && _numberValue == "" && _descriptionValue == ""))
                {

                    // новые данные person
                    PersonToEdit.Name = NameValue;
                    PersonToEdit.Adress = AdressValue;
                    PersonToEdit.Number = NumberValue;
                    PersonToEdit.Description = DescriptionValue;

                    // обновление данных в бд
                    UserRepository userRepository = new UserRepository();
                    userRepository.UpdateUser(PersonToEdit);

                    // вызов события чтобы оповестить о редактировании person
                    OnPersonEdited(PersonToEdit);

                    MessageBoxCastom.SuccessWithColoredName("Пользователь ", PersonToEdit.Name.Trim(), Brushes.Green, " отредактирован!");

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

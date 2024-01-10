using System.Collections.ObjectModel;
using System.Windows;
using WPFTaskPerson.Command;
using WPFTaskPerson.Modals;
using WPFTaskPerson.Service;

namespace WPFTaskPerson.ViewModals
{

    public class PersonViewModal:INotifyPropertyHandler
    {
        #region Constructor
        PersonService personService;
        public PersonViewModal() 
        {
            personService = new PersonService();
            CurrentPerson = new Person();
            LoadPersonData();
            _saveCommand = new RelayCommand(Save);
            updateCommand=new RelayCommand(Update);
            DeleteCommand = new RelayCommand(Delete);
        }
        #endregion
        #region Get/Load Information
        private ObservableCollection<Person>? personList;
        public ObservableCollection<Person>? PersonList
        {
            get { return personList; }
            set { personList = value; NotifyPropertyChanged(nameof(PersonList)); }
        }
        private void LoadPersonData()
        {
            PersonList =new ObservableCollection<Person>(personService.GetAllPerson());
        }
        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; NotifyPropertyChanged(nameof(Message)); } 
        }
        #endregion
        #region Add
        private Person _currentPerson;
        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { _currentPerson = value; NotifyPropertyChanged(nameof(CurrentPerson)); }
        }
        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand; }
           
        }
        public void Save()
        {
            try
            {
                var isSave = personService.AddNewPerson(CurrentPerson);
                LoadPersonData();
                if(isSave)
                {
                    Message = "Person Information Added";
                }
                else
                {
                    Message = "Add Operation Fail";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion
        #region Update
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
            set { updateCommand = value;NotifyPropertyChanged(nameof(UpdateCommand)); }
        }

        public void Update()
        {
            try
            {
                bool IsUpdated = personService.UpdatePerson(CurrentPerson);
                LoadPersonData();
                if (IsUpdated)
                {
                    Message = "Person Information Updated";
                }
                else
                {
                    Message = "Updated Operation Fail";
                }
            }
            catch (Exception ex)
            {
                Message=ex.Message;
            }
        }
        #endregion
        #region Delete
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }
        public void Delete()
        {
            var result=MessageBox.Show("are you Sure To delete this Record","Delete",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                bool isDelete = personService.DeletePerson(CurrentPerson.Id);
                LoadPersonData();
                if (isDelete)
                {
                    Message = "Person Information Deleted";
                }
                else
                {
                    Message = "Person Information Deleted";
                }
            }
        }
        #endregion
    }
}

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WPFTaskPerson.ViewModals;

namespace WPFTaskPerson.Modals
{
    public class Person : INotifyPropertyHandler, INotifyDataErrorInfo
    {
        #region Private Variables
        private int id;
        private string? firstName;
        private string? lastName;
        private DateTime? dob;
        private string? email;
        private string? phone;
        private string? gender;
        private string? language;
        #endregion

        #region Public Properties
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged(nameof(Id));
            }
        }

        [Required(ErrorMessage = "FirstName Is Required")]
        public string? FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value; NotifyPropertyChanged(nameof(FirstName));
                Validate(nameof(FirstName), value);
            }
        }

        [Required(ErrorMessage = "LastName Is Required")]
        public string? LastName
        {
            get { return lastName; }
            set
            {
                lastName = value; NotifyPropertyChanged(nameof(LastName));
                Validate(nameof(LastName), value);
            }
        }

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        [AgeValidation(18, ErrorMessage = "Age must be greater than 18.")]
        public DateTime? DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                NotifyPropertyChanged(nameof(DOB));
                Validate(nameof(DOB), value);
            }
        }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email should be Email Address")]
        public string? Email
        {
            get { return email; }
            set
            {
                email = value; NotifyPropertyChanged(nameof(Email));
                Validate(nameof(Email), value);
            }
        }

        [Required(ErrorMessage = "Phone Is Required")]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Invalid phone number format. Use (123) 456-7890.")]
        public string? Phone
        {
            get { return phone; }
            set
            {
                phone = value; NotifyPropertyChanged(nameof(Phone));
                Validate(nameof(Phone), value);
            }
        }

        [Required(ErrorMessage = "Gender Is Required")]
        public string? Gender
        {
            get { return gender; }
            set
            {
                gender = value; NotifyPropertyChanged(nameof(Gender));
                Validate(nameof(Gender), value);
            }
        }

        [Required(ErrorMessage = "Language Is Required")]
        public string? Language
        {
            get { return language; }
            set
            {
                language = value; NotifyPropertyChanged(nameof(Language));
                Validate(nameof(Language), value);
            }
        } 
        #endregion

        Dictionary<string, List<string>> Error = new Dictionary<string, List<string>>();

        public bool HasErrors => Error.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (Error.ContainsKey(propertyName))
            {
                return Error[propertyName];
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// Validates property and adds an error to the list
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public void Validate(string propertyName, object propertyValue)
        {
            var result = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this)
            {
                MemberName = propertyName
            }, result);

            if (result.Any())
            {
                Error.Add(propertyName, result.Select(x => x.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Error.Remove(propertyName);
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WPFTaskPerson.Modals
{
    public class AgeValidationAttribute: ValidationAttribute
    {
        private readonly int minimumAge;

        public AgeValidationAttribute(int minimumAge)
        {
            this.minimumAge = minimumAge;
            ErrorMessage = $"Age must be greater than {minimumAge}.";
        }

        /// <summary>
        /// Validates age based on the selected DOB
        /// </summary>
        /// <param name="value">DOB</param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dob)
            {
                if (CalculateAge(dob) < minimumAge)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Calculates age (number of years) from DOB
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        private int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;
            if (birthDate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}

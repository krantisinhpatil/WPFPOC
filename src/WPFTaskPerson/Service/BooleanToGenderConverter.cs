using System.Globalization;
using System.Windows.Data;

namespace WPFTaskPerson.Service
{
    public class BooleanToGenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return "Male";
            }
            else
            {
                return "Female";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string result = value.ToString();
            if (result=="True")
            {
                return "Male";
            }

            return "Female"; 
        }
    }
}

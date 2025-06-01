using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ConsumeApi.Converters
{
    public class TruncateTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text && int.TryParse(parameter?.ToString(), out int maxLength))
            {
                if (text.Length > maxLength)
                {
                    return text.Substring(0, maxLength) + "...";
                }
                return text;
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
       
    }
}

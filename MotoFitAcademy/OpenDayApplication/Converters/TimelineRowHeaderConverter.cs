using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace OpenDayApplication.Converters
{
    public class TimelineRowHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return GetRowHeader((value as DataGridRow).GetIndex());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }

        private string GetRowHeader(int index)
        {
            return string.Format("{0}:00", index + 8);
        }
    }
}

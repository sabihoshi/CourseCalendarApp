using System.Globalization;
using System.Windows.Data;

namespace CourseCalendarApp.ModernWPF;

public class GroupKeyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        => string.IsNullOrEmpty(value?.ToString()) ? string.Empty : value.ToString()![..1].ToUpper();

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 04/22/2014
 * Время: 14:52
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace DrawLibrary.Brushes
{
	/// <summary>
	/// Description of BrushTypeConverter.
	/// </summary>
	[ValueConversion(typeof(BrushType), typeof(bool))]
	public class BrushTypeConverter: IValueConverter
	{
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            string name = Enum.GetName(typeof(BrushType), value);

            return ( name == (string)parameter );
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return new NotSupportedException(this.GetType().Name + " конвертирование назад не поддерживается");
        }
	}
}

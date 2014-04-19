/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.04.2014
 * Время: 14:10
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Globalization;
using System.Windows.Data;

namespace DrawLibrary.Tools
{
	/// <summary>
	/// Конвертирует тип инструмента в bool
	/// </summary>
	[ValueConversion(typeof(ToolType), typeof(bool))]
	public class ToolTypeConverter: IValueConverter
	{
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            string name = Enum.GetName(typeof(ToolType), value);

            return ( name == (string)parameter );
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return new NotSupportedException(this.GetType().Name + " конвертирование назад не поддерживается");
        }
	}
}

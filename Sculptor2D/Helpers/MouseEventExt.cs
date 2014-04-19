/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 17.04.2014
 * Время: 14:11
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Helpers
{
	/// <summary>
	/// Расширение для мышиного события
	/// </summary>
	internal static class MouseEventExt
	{
		public static bool AllButtonReleases(this MouseEventArgs e)
		{//все кнопки отпущены
        	return 	e.LeftButton == MouseButtonState.Released 
        			&& e.RightButton == MouseButtonState.Released
        			&& e.MiddleButton == MouseButtonState.Released;
		}
		
		public static bool LeftButtonPressed(this MouseEventArgs e)
		{
			return e.LeftButton == MouseButtonState.Pressed;
		}

		public static bool RightButtonPressed(this MouseEventArgs e)
		{
			return e.RightButton == MouseButtonState.Pressed;
		}

		public static bool MiddleButtonPressed(this MouseEventArgs e)
		{
			return e.MiddleButton == MouseButtonState.Pressed;
		}
		
		public static bool AnyButtonPressed(this MouseEventArgs e)
		{//все кнопки отпущены
        	return 	e.LeftButton == MouseButtonState.Pressed 
        			|| e.RightButton == MouseButtonState.Pressed
        			|| e.MiddleButton == MouseButtonState.Pressed;
		}
	}
}

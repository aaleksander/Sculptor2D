/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 04/16/2014
 * Время: 13:45
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DrawLibrary.Graphics;

namespace DrawLibrary.Tools
{
	/// <summary>
	/// Description of ToolLine.
	/// </summary>
	public class ToolLine: ToolMultiPoint<GraphicsLine>
	{
		public ToolLine(): base()
		{
		}

	}
}

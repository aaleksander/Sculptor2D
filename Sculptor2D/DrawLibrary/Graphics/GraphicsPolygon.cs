/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 17.04.2014
 * Время: 15:06
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using System.Windows.Media;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// Description of GraphicsPolygon.
	/// </summary>
	public class GraphicsPolygon: GraphicsMultiPoint
	{
		public GraphicsPolygon()
		{
			_isClosed = true;
			
			//TODO: сделать в виде свойства, чтобы можно было сразу перерисовывать
			_brush = new SolidColorBrush(Colors.Green);
			
			//TODO:Свойство "Обводка" - Pen
		}
	}
}

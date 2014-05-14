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
using DrawLibrary.Serialize;

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
			
			_brush = new SolidColorBrush(Colors.LightGreen );
			
			//TODO: Свойство "Обводка" - Pen
		}
		
		public GraphicsPolygon(DrawingCanvas aCanvas, SerializePolygon aObj):base(aCanvas, aObj)
		{}

		/// <summary>
		/// делает полную копию объекта
		/// </summary>
		/// <returns></returns>
		public override GraphicsBase Clone()
		{
			GraphicsPolygon res = new GraphicsPolygon();
			CloneAttributes(res);
			return res;
		}

        public override SerializeBase GetSerializebleObject()
        {
        	return new SerializePolygon(this);
        }
	}
}

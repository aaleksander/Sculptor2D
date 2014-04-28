/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 28.04.2014
 * Время: 15:48
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using System.Windows.Media;

namespace DrawLibrary.Graphics
{
	public enum DrawingCursorType
	{
		None,
		Brush, 	//кисть
		
		
		max
	}
	
	/// <summary>
	/// Description of GraphicsCursor.
	/// </summary>
	public class GraphicsCursor:GraphicsBase
	{
		private double _size;
		private DrawingCursorType _type;
		public GraphicsCursor(DrawingCanvas aCanvas, DrawingCursorType aType, double aSize=1)
		{
			_size = aSize;
			_type = aType;

			RefreshDrawing();
		}

		public override void Draw(DrawingContext aContext)
		{
			Color col = new Color();
			col.A = 50;
			col.G = 255;
			
			SolidColorBrush br = new SolidColorBrush(col);
			Pen pn = new Pen(new SolidColorBrush(Colors.Black), 0.1);
			
			aContext.DrawEllipse(br, pn, new Point(0, 0), _size, _size);
			aContext.DrawLine(pn, new Point(0, -_size - _size*0.1), new Point(0, _size + _size*0.1));
			aContext.DrawLine(pn, new Point(-_size - _size*0.1, 0), new Point(_size + _size*0.1, 0));
		}	
		
		public DrawingCursorType Type{
			get{
				return _type;
			}
		}
		
		public double Size{
			set{
				_size = value;
				RefreshDrawing();
			}
			get{
				return _size;
			}			
		}
	}
}

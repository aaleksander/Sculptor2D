/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 29.04.2014
 * Время: 14:53
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using System.Windows.Media;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// изображение вершины
	/// </summary>
	public class GraphicsServicePoint: GraphicsService
	{
		private int _pointHashCode;
		private Point _point;
		private static Brush 	_br;// = new SolidColorBrush(Colors.Black);
		private static Brush 	_brHit;// = new SolidColorBrush(Colors.Red);
		private static Pen 		_pn = new Pen(new SolidColorBrush(Colors.White), 1);
		
		public GraphicsServicePoint(GraphicsBase aObj, Point aPoint, int aHash): base(aObj)
		{
			_point = aPoint;
			_pointHashCode = aHash;

			Color col = Colors.Black;
			col.A = 100;
			_br = new SolidColorBrush(col);

			col = Colors.Red;
			col.A = 100;
			_brHit = new SolidColorBrush(col);

			RefreshDrawing();
		}
		
		public override void Draw(DrawingContext aContext)
		{
			if( IsHit )
				aContext.DrawEllipse(_brHit, _pn, _point, 4, 4);
			else
				aContext.DrawEllipse(_br, _pn, _point, 4, 4);
		}
		
		public override bool Contains(Point point)
		{
			return Geometry.dist(point, _point) < 4;
		}
		
		public GraphicsMultiPoint Move(Point aPoint)
		{
//			var obj = (GraphicsMultiPoint) OwnerCanvas.GetObjectBy(x => IsYourOwner(x)); //ищем, чей это объект
//			if( obj == null )
//				return;
			//FIXME: почему IsHist=true - не работает?
			IsHit = true;
			Point p;
			GraphicsMultiPoint obj = (GraphicsMultiPoint)Owner;
			for(int i=0; i<obj.Points.Count; i++)
			{
				p = obj.Points[i];
				if( i == _pointHashCode ) //нашли нужную точку
				{
					obj.Points[i] = new Point(aPoint.X, aPoint.Y);
					break;
				}
			}

			obj.RefreshDrawing();

			return obj;
		}
	}
}

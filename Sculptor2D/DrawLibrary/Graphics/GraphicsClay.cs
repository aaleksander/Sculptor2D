/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.04.2014
 * Время: 12:58
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using DrawLibrary.Brushes;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// Description of GraphicsClay.
	/// </summary>
	public class GraphicsClay: GraphicsMultiPoint
	{
		public GraphicsClay(GraphicsMultiPoint aGr):base()
		{
			double maxLen = 20; //максимально возможный отрезок
			Points.Add(aGr.Points[0]);
			for(int i=1; i<aGr.Count; i++)
			{
				AddPointToClay(aGr.Points[i], maxLen);
			}
			
			_isClosed = aGr.IsClosed;

			if( IsClosed )
			{
				AddPointToClay(Points[0], maxLen);
			}
			
			RefreshDrawing();
		}
		
		/// <summary>
		/// получает на вход точку и создает до нее несколько отрезков
		/// </summary>
		/// <param name="aPoint">новая точка</param>
		/// <param name="maxLen">максимальная длина отрезка</param>
		private void AddPointToClay(Point aPoint, double maxLen)
		{
			if( Geometry.dist(LastPoint, aPoint) <= maxLen )
			{ //добавляемый отрезок - коротковат
				AddPoint(aPoint);
				return;
			}
			//делим отрезок пополам и вызываем рекурсивно
			Point p1 = new Point(
				(aPoint.X + LastPoint.X) / 2,
				(aPoint.Y + LastPoint.Y) / 2);
			AddPointToClay(p1, maxLen);
			AddPointToClay(aPoint, maxLen);
		}
		
		/// <summary>
		/// На объект действует какая-то кисть
		/// </summary>
		/// <param name="aBrush"></param>
		public void Brushed(BrushBase aBrush)
		{
			Point vector, p;
			double dist;
			for(int i=0; i<Points.Count; i++)
			{
				p = Points[i];
				vector = Geometry.GetVector(aBrush.Point, p);
				dist = Geometry.dist(aBrush.Point, p);
				Points[i] = new Point(p.X + vector.X/dist, p.Y + vector.Y/dist);
				//Points[i].X += vector.X;
				//Points[i].Y += vector.Y;
			}
			
			RefreshDrawing();
		}
	}
}

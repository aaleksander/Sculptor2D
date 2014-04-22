/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.04.2014
 * Время: 12:58
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using DrawLibrary.Brushes;
using Sculptor2D.Helpers;
using System.Linq;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// Description of GraphicsClay.
	/// </summary>
	public class GraphicsClay: GraphicsMultiPoint
	{
		public GraphicsClay(GraphicsMultiPoint aGr):base()
		{
			//копируем все вершины
			foreach(var p in aGr.Points)
			{
				AddPoint(p.X, p.Y);
			}
			
			IsClosed = aGr.IsClosed;
			
			UpdateClay();
		}

		/// <summary>
		/// разбить ребра, чтобы вершины не сильно разбегались друг от друга
		/// </summary>
		public void UpdateClay()
		{
			double maxLen = 10; //максимально возможная длина отрезка
			double minLen = 3;  //минимально возможная длина отрезка
			
			Collection<Point> tmp = new Collection<Point>();

			foreach(var p in Points)
			{
				tmp.Add(new Point(p.X, p.Y));
			}
//			Points.ToList().ForEach(x => tmp.Add(new Point(x.X, x.Y)));
			
			Points.Clear();
			
			Points.Add(tmp[0]);
			for(int i=1; i<tmp.Count; i++)
			{
				Points.AddClay(tmp[i], maxLen);
			}

			if( IsClosed )
			{
				Points.AddClay(tmp[0], maxLen);
				Points.RemoveAt(Points.Count - 1); //удалим последнюю точку, она не нужна
			}
			
			//TODO: удалить вершины, которые слишком близко (взять две вершины, и если они очень близко - заменить на одну посередине.
			bool f = true;
			while (f == true)
			{
				f = false;
				for(int i=0; i<Points.Count - 1; i++)
				{
					Point p1 = Points[i];
					Point p2 = Points[i + 1];
					if( Geometry.dist(p1, p2) < minLen )
					{
						f = true;
						Point p = new Point((p1.X + p2.X)/2, (p1.Y + p2.Y)/2);
						Points.RemoveAt(i);
						Points.RemoveAt(i); //не плюс один потому что мы уже удалили и i+1 сместился на i
						Points.Insert(i, p);
					}
						
				}
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
				vector = Geometry.GetVector(aBrush.LastPoint, p);
				vector.X *= 3;
				vector.Y *= 3;
				dist = Geometry.dist(aBrush.LastPoint, p);
				if( dist < 50 )
					Points[i] = new Point(p.X + vector.X/dist, p.Y + vector.Y/dist);
				//Points[i].X += vector.X;
				//Points[i].Y += vector.Y;
			}			
			
			RefreshDrawing();
		}
	}
}

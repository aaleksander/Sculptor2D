/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 04/22/2014
 * Время: 16:00
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DrawLibrary.Graphics;

namespace DrawLibrary.Brushes
{
	/// <summary>
	/// Кисть разглаживает линии
	/// </summary>
	public class BrushSmoother: BrushBase
	{
		private double eps = 0.001;
		public BrushSmoother(): base()
		{
			_type = BrushType.Smoother;
			Power = 50;
		}
		
		
		//TODO: разобраться, почему при большой мощности, линия начинает плясать.
		public override bool Modify(GraphicsClay aObj)
		{//возвращает истину, если кисть изменила объект (смогла дотянуться до его вершин и сдвинуть их)			
			bool res = false;
			double d;
			Point last = LastPoint, p, p1, p2;
			Point? smoothP;
			Collection<Point> tmp = new Collection<Point>(); //сюда будет складывать выровненные вершины
			foreach(var ppp in aObj.Points)
				tmp.Add(new Point(ppp.X, ppp.Y));
			for(int i=1; i<aObj.Count - 1; i++)
			{
				p = tmp[i];
				d = Geometry.dist(last, p);
				if( d < Size ) //Точка попала в область действия кисти
				{
					p1 = tmp[i - 1];
					p2 = tmp[i + 1];
					smoothP = Smooth(p, p1, p2, d);
					if( smoothP.HasValue ) //эта точка не лежит на отрезке
					{
						tmp[i] = smoothP.Value;
						res = true;
					}
				}
			}

			if( aObj.IsClosed )
			{//разбираемся с последней и нулевой точкой
				p1 = tmp.Last();
				p2 = tmp[1];
				p = tmp[0];
				d = Geometry.dist(last, p);//FIXME: вставить сюда контроль дистанции
				if( d < Size )
				{
					smoothP = Smooth(p, p1, p2, d);
					if( smoothP.HasValue ) //эта точка не лежит на отрезке
					{
						tmp[0] = smoothP.Value;
						res = true;
					}				
				}
				
				p = tmp.Last();
				p2 = tmp[0];
				p = tmp[aObj.Count - 2];
				d = Geometry.dist(last, p);
				if( d < Size )
				{
					smoothP = Smooth(p, p1, p2, d);
					if( smoothP.HasValue ) //эта точка не лежит на отрезке
					{
						tmp[tmp.Count - 1] = smoothP.Value;
						res = true;
					}
				}
			}

			if( res )
			{//копируем обратно
				aObj.Points.Clear();
				foreach(var ppp in tmp)
					aObj.Points.Add(new Point(ppp.X, ppp.Y));
			}
			return res;
		}

		private Point? Smooth(Point p, Point p1, Point p2, double dist)
		{
			Point? res = null;
			//Point proj = Geometry.GetProjection(p,  p1, p2);
			Point proj = new Point((p1.X + p2.X)/2, (p1.Y + p2.Y)/2); //просто серединка основания
			if( Geometry.dist(proj, p) > eps ) //эта точка не лежит на отрезке
			{
				Point vec = Geometry.GetVector(p, proj, false);
				
				var koef = Power*dist/Size/100;
				res = new Point(p.X + vec.X*koef, p.Y + vec.Y*koef);
				//res = new Point(p.X + vec.X/(Power/dist), p.Y + vec.Y/(Power/dist));
				//res = new Point(p.X + vec.X/dist, p.Y + vec.Y/dist);

				//res = new Point(p.X + vec.X/(1/Power), p.Y + vec.Y*(1/Power));
				//res = new Point(p.X + vec.X/(Power/100), p.Y + vec.Y/(Power/100)); //кушеряшка, программа тут же вешается
				//res = new Point(p.X + vec.X*(Power/50), p.Y + vec.Y*(Power/50)); //эта штука дает интересный эффект равного края
			}

			return res;
		}
	}
}

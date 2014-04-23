/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.04.2014
 * Время: 16:48
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;

namespace DrawLibrary
{
	/// <summary>
	/// всякие геометрические функции
	/// </summary>
	public static class Geometry
	{
		/// <summary>
		/// возвращает дистанцию между двумя точками
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static double dist(Point a, Point b)
		{
			double t1 = (a.X - b.X);
			double t2 = (a.Y - b.Y);
			return Math.Sqrt(t1*t1 + t2*t2);
		}

		/// <summary>
		/// возвращает единичный вектор из точки а в точку б
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Point GetVector(Point a, Point b, bool aNormalize=true)
		{
			Point res = new Point(b.X - a.X, b.Y - a.Y);
			if( aNormalize )
			{
				double len = Math.Sqrt(res.X*res.X + res.Y*res.Y);
				res.X /= len;
				res.Y /= len;
			}
			
			return res;
		}
		
		/// <summary>
		/// возвращает точку-проекцию точки p на отрезок p1-p2
		/// </summary>
		/// <param name="p"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static Point GetProjection(Point p, Point p1, Point p2)
		{
		    double fDenominator = (p2.X - p1.X)*(p2.X - p1.X) + (p2.Y - p1.Y)*(p2.Y - p1.Y);
		    if (fDenominator == 0) // p1 and p2 are the same
		        return p1;
		
		    double t = (p.X*(p2.X - p1.X) - (p2.X - p1.X)*p1.X + p.Y*(p2.Y - p1.Y) - (p2.Y - p1.Y)*p1.Y) / fDenominator;
		
		    return new Point(p1.X + (p2.X - p1.X)*t, p1.Y + (p2.Y - p1.Y)*t);
		}		
	}
}

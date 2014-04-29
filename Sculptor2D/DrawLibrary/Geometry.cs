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
		static double pi = 3.14159265358979323;
		
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

		/// <summary>
		/// возвращает угол между двумя отрезками
		/// </summary>
		/// <param name="p11"></param>
		/// <param name="p12"></param>
		/// <param name="p21"></param>
		/// <param name="p22"></param>
		/// <returns></returns>		
		public static double GetAngle(Point p11, Point p12, Point p21, Point p22)
		{			
			var l1 = Math.Sqrt((p11.X - p12.X)*(p11.X - p12.X) + (p11.Y - p12.Y)*(p11.Y - p12.Y));
    		var l2 = Math.Sqrt((p21.X - p22.X)*(p21.X - p22.X) + (p21.Y - p22.Y)*(p21.Y - p22.Y));

    		var v1 = new Point(0, 0);
    		var v2 = new Point(0, 0);
    		v1.X = (p12.X/l1 - p11.X/l1);
    		v1.Y = (p12.Y/l1 - p11.Y/l1);
    		v2.X = (p22.X/l2 - p21.X/l2);
    		v2.Y = (p22.Y/l2 - p21.Y/l2);

    			
    		double res = Math.Atan2(v1.X*v2.Y - v2.X*v1.Y, v1.X*v2.X + v1.Y*v2.Y);

    		if (res < 0)
        		res = pi + (pi - Math.Abs(res));

    		//переводим в градусы    		
    		return res*180/pi;
		}
		
	}
}

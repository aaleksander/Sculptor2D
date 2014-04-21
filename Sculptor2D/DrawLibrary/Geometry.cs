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
		public static Point GetVector(Point a, Point b)
		{
			Point res = new Point(b.X - a.X, b.Y - a.Y);
			double len = Math.Sqrt(res.X*res.X + res.Y*res.Y);
			res.X /= len;
			res.Y /= len;
			
			return res;
		}
	}
}

/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 22.04.2014
 * Время: 13:08
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using DrawLibrary;

namespace Sculptor2D.Helpers
{
	/// <summary>
	/// расширения для коллекций
	/// </summary>
	internal static class CollectionPoint
	{
		/// <summary>
		/// добавляет к массиву новую точку, если длина отрезка больше aMaxLen, то он дробит его на несколько 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="aMaxLen"></param>
		/// <returns></returns>
		public static void AddClay(this Collection<Point> a, Point b, double aMaxLen)
		{
			Point last = a.Last();
			var dist = Geometry.dist(last, b);
			if( dist < aMaxLen)
			{
				a.Add(b);
				return;
			}

			Point p = new Point();
			p.X = (b.X + last.X)/2;
			p.Y = (b.Y + last.Y)/2;

			a.AddClay(p, aMaxLen);
			a.AddClay(b, aMaxLen);
		}

		/// <summary>
		/// возвращает последний элемент коллекции
		/// </summary>
		/// <param name="l"></param>
		/// <returns></returns>
		public static Point Last(this Collection<Point> l)
		{
			return l[l.Count - 1];
		}
	}
}

/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 24.04.2014
 * Время: 16:27
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Linq;
using System.Windows;

namespace DrawLibrary.Brushes
{
	/// <summary>
	/// Кисть - щиплка
	/// </summary>
	public class BrushPincher: BrushBase
	{
		public BrushPincher():base()
		{
			_type = BrushType.Pincher;
			Power = 30;
			Size = 10;
		}

		public override bool Modify(DrawLibrary.Graphics.GraphicsClay aObj)
		{
			bool res = false;
			double d;
			Point first = Path.First();
			Point last = LastPoint, p, vec;
			Point preLast = Path[Path.Count - 2];			
			for(int i=0; i<aObj.Count; i++)
			{
				p = aObj.Points[i];
				d = Geometry.dist(last, p);

				if( d < Size ) //Точка попала в зону кисти В НАЧАЛЕ перемещения
				{
					vec = Geometry.GetVector(preLast, last, false); //вектор перемещения кисти
					var k = (Size-d)/Size;
					aObj.Points[i] = new Point(p.X + vec.X*k, p.Y + vec.Y*k);
					res = true;
				}
			}
			return res;
		}	
	}
}

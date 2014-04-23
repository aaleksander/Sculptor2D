/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 23.04.2014
 * Время: 17:02
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Linq;
using System.Windows;

namespace DrawLibrary.Brushes
{
	/// <summary>
	/// Кисть для перемещения вершин
	/// </summary>
	public class BrushMover: BrushBase
	{
		public BrushMover():base()
		{
			_type = BrushType.Mover;
			Power = 30;
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
					aObj.Points[i] = new Point(p.X + vec.X, p.Y + vec.Y);
					res = true;
				}
			}

			//if( res )
			//	aObj.RefreshDrawing();
		
			return res;
		}		
	}
}

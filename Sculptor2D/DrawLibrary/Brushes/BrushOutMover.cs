/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 04/22/2014
 * Время: 15:30
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Configuration;
using System.Windows;

namespace DrawLibrary.Brushes
{
	/// <summary>
	/// Кисть - отталкивает вершины
	/// </summary>
	public class BrushOutMover: BrushBase
	{
		public BrushOutMover(): base()
		{
			_type = BrushType.OutMover;
			Power = 50;
		}		
		
		public override bool Modify(DrawLibrary.Graphics.GraphicsClay aObj)
		{
			bool res = false;
			double d;
			Point last = LastPoint, p, vec;
			for(int i=0; i<aObj.Count; i++)
			{
				p = aObj.Points[i];
				d = Geometry.dist(last, p);
				if( d < Size ) //Точка попала в область действия кисти
				{
					vec = Geometry.GetVector(last, p);				
					aObj.Points[i] = new Point(p.X + vec.X/d*(Power/10), p.Y + vec.Y/d*(Power/10));
					res = true;
				}
			}

			//if( res )
			//	aObj.RefreshDrawing();

			return res;
		}
	}
}

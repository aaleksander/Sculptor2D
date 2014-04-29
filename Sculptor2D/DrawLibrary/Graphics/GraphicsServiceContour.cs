/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 29.04.2014
 * Время: 13:31
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// контур объекта, который будет просвечивать через остальные
	/// </summary>
	public class GraphicsServiceContour: GraphicsService
	{
		private Collection<Point> _points = new Collection<Point>();
		private SolidColorBrush _brush = new SolidColorBrush(Colors.Red);
		private Pen _pen = new Pen(new SolidColorBrush(Colors.Red), 2);
		
		public GraphicsServiceContour(GraphicsBase aObj, bool aHot): base(aObj)
		{
			foreach(var p in ((GraphicsMultiPoint) aObj).Points)
			{
				_points.Add(new Point(p.X, p.Y));
			}
			
			if( !aHot )
				_pen = new Pen(new SolidColorBrush(Colors.Black), 0.5);
			
			RefreshDrawing();
		}
		
		public override void Draw(DrawingContext aContext)
		{
			if( _points.Count < 2 )
				return;
			//обнулим матрицу трансформации
			if( Transform != null )
			{
				for(int i=0; i<_points.Count; i++)
				{
					_points[i] = Transform.Transform(_points[i]);
				}
				Transform = null;
			}

            StreamGeometry geometry = new StreamGeometry();
            geometry.FillRule = FillRule.EvenOdd;			
			using (StreamGeometryContext ctx = geometry.Open())
            {
				Point p1 = _points[0];
				ctx.BeginFigure(p1, false /* is filled */, ((GraphicsMultiPoint)Owner).IsClosed /* is closed */);
				ctx.PolyLineTo(_points, true, true);
            }

			aContext.DrawGeometry(null, _pen, geometry);
		}
	}
}

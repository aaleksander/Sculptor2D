/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 17.04.2014
 * Время: 16:50
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
	/// графический объект из множества точек
	/// </summary>
	public class GraphicsMultiPoint: GraphicsBase
	{
		public GraphicsMultiPoint(): base()
		{
			_points = new Collection<Point>();
		}

		private Collection<Point> _points;
		protected bool _isClosed = false;
		protected Brush _brush = null;
		
		public bool IsClosed{
			set{
				_isClosed = value;
				RefreshDrawing();
			}
			get{
				return _isClosed;
			}
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
                ctx.BeginFigure(p1, true /* is filled */, _isClosed /* is closed */);                
				ctx.PolyLineTo(_points, true, true);
            }

			Color col = (IsHit)? Colors.Red: Colors.Blue;
			//col.A = 100; //прозрачность
			aContext.DrawGeometry(_brush, new Pen(new SolidColorBrush(col), 5), geometry);
        	
            if ( IsSelected )
            {
                DrawTracker(aContext);
            }           
		}		

		/// <summary>
		/// преобразует объект в состояние "Глина". обратное преобразование невозможно
		/// </summary>
		/// <returns></returns>
		//TODO: закончить
		public virtual GraphicsClay ToClay()
		{			
			return new GraphicsClay(this);
		}

		public void DeleteLastPoint()
		{
			_points.RemoveAt(_points.Count - 1);
			RefreshDrawing();
		}


		
		public int Count
		{
			get{
				return _points.Count;
			}
		}
		
		public Collection<Point> Points{
			get{
				return _points;
			}
		}

		public override void MoveLastHandleTo(Point aP)
		{
			_points[_points.Count - 1] = aP;
			RefreshDrawing();
		}
		
		protected Point LastPoint
		{
			get{
				if( _points.Count == 0 )
					throw new IndexOutOfRangeException("Нет точек");
				return _points[_points.Count - 1];
			}
		}
		
        /// <summary>
        /// попадает ли точка в этот объект
        /// </summary>
        public override bool Contains(Point point)
        {
			StreamGeometry g = new StreamGeometry();
			using( var c = g.Open())
			{
				c.BeginFigure(_points[0], true, _isClosed);
				c.PolyLineTo(_points, true, true);
			}
			g.Transform = Transform;

			if( _isClosed == false )
				return g.StrokeContains(new Pen(System.Windows.Media.Brushes.Black, 10), point);
			else
				return g.FillContains(point, 0.001, ToleranceType.Relative) ||
					   g.StrokeContains(new Pen(System.Windows.Media.Brushes.Black, 10), point);
        }		
        
        public void AddPoint(double x, double y)
        {
        	_points.Add(new Point(x, y));
        }
        
		public int AddPoint(Point aP)
		{
			_points.Add(aP);
			
			return _points.Count;
		}        
        
		public override void DrawTracker(DrawingContext aContext)
		{
			var br = new SolidColorBrush(Colors.Black);
			var pn = new Pen(new SolidColorBrush(Colors.White), 1);
			foreach(var p in _points)
			{
				aContext.DrawEllipse(br, pn, new Point(p.X, p.Y), 5, 5);
				//aContext.DrawRectangle(br, null, new Rect(p.X - 5, p.Y - 5, 10, 10));
				//aContext.DrawRectangle(null, pn, new Rect(p.X - 4, p.Y - 4, 8, 8));
				//aContext.DrawRectangle(br, null, new Rect(p.X - 3, p.Y - 3, 6, 6));
			}
        }
		
	}
}

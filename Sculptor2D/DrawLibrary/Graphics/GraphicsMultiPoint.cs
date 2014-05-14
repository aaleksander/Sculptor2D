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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using DrawLibrary.Serialize;
using Sculptor2D.Helpers;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// графический объект из множества точек
	/// </summary>
	public class GraphicsMultiPoint: GraphicsBase
	{
		public GraphicsMultiPoint()
		{}
		
		public GraphicsMultiPoint(DrawingCanvas aCanvas): base(aCanvas)
		{}

		public GraphicsMultiPoint(DrawingCanvas aCanvas, SerializeMultiPoint aObj):base(aCanvas, aObj)
		{
			IsClosed = aObj.IsClosed;
			
			_points = new Collection<Point>();
			foreach(var p in aObj.Points)
				_points.Add(new Point(p.X, p.Y));
		}
		
		private Collection<Point> _points = new Collection<Point>();
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

		public Brush Brush{
			set{
				_brush = value;
				//TODO: Сделать в graphicsBase эту штуку OnPropertyChanged("Brush");
			}
			get{
				return _brush;
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

			Color col = Colors.Blue;
			aContext.DrawGeometry(_brush, new Pen(new SolidColorBrush(col), 2), geometry);

			if( IsHit ) //надо подсветить поверх всех объектов
			{
				OwnerCanvas.GraphicsList.Add(new GraphicsServiceContour(this, true));
			}

			if ( IsSelected )
            {
				//нарисовать еще один контур, только потоньше (пунктиром?) и другим цветом
				OwnerCanvas.GraphicsList.Add(new GraphicsServiceContour(this, false));
				switch (Mode)
				{
				case GraphicsMode.Selected:	DrawTracker(aContext); break;
				case GraphicsMode.Points:	DrawPoints(); break;
				}
            }
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
			set{
				_points = value;
			}
			get{
				return _points;
			}
		}

		public override Point GetCenter()
		{
			var res = new Point(0, 0);
			foreach(var p in _points)
			{
				res.X += p.X;
				res.Y += p.Y;
			}
			res.X /=Count;
			res.Y /=Count;
			return res;
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

		/// <summary>
		/// возвращает прямоугольник, очерчивающий этот объект (опирается на точки)
		/// </summary>
		/// <returns></returns>
		public Rect GetRect()
		{
			double left=0, right=0, top=0, bottom=0;
			Point p;
			//ищем крайние точки
			for(int i=0; i<_points.Count; i++)
			{
				p = _points[i];
				if( i == 0 )
				{
					left = p.X;
					right = p.X;
					top = p.Y;
					bottom = p.Y;
					continue;
				}
				if( left > p.X ) left = p.X;
				if( right < p.X ) right = p.X;
				if( top > p.Y )	top = p.Y;
				if( bottom < p.Y ) bottom = p.Y;
			}
			
			return new Rect(left, top, right-left, bottom-top);
		}

		/// <summary>
		/// рисует маркеры, за которые можно наскать объект
		/// </summary>
		/// <param name="aContext"></param>
		public override void DrawTracker(DrawingContext aContext)
		{			
			//var r = GetRect();
			
			//TODO: создать сервисный объект - общий прямоугольник

			//DrawOneTracker(aContext, r.Left - 8, r.Top - 8);
			//DrawOneTracker(aContext, r.Left - 8, r.Bottom + 8);
			//DrawOneTracker(aContext, r.Right + 8, r.Top - 8);
			//DrawOneTracker(aContext, r.Right + 8, r.Bottom + 8);
        }

		public override void DrawPoints()
		{
			for(int i=0; i<Points.Count; i++)
			{
				OwnerCanvas.GraphicsList.Add(new GraphicsServicePoint(this, Points[i], i));
				//aContext.DrawEllipse(br, pn, new Point(p.X, p.Y), 4, 4);
			}
        }		

		/// <summary>
		/// рисует один прямоугольник
		/// </summary>
		/// <param name="aContext"></param>
		private void DrawOneTracker(DrawingContext aContext, double aX, double aY)
		{
			double size = 8;
			//Рисуем углы
			var br1 = new SolidColorBrush(Colors.Black);
			var p1 = new Pen(br1, 1);

    		double halfPenWidth = p1.Thickness / 2;

    		// Create a guidelines set
    		GuidelineSet guidelines = new GuidelineSet();

    		guidelines.GuidelinesX.Add(aX + halfPenWidth);
    		guidelines.GuidelinesX.Add(aX + size + halfPenWidth);
    		guidelines.GuidelinesY.Add(aY + halfPenWidth);
    		guidelines.GuidelinesY.Add(aY + size + halfPenWidth);

    		aContext.PushGuidelineSet(guidelines);
    		aContext.DrawRectangle(null, p1, new Rect(aX, aY, size, size));
    		aContext.Pop();
		}
		
		/// <summary>
		/// возвращает координаты в виде строки SVG
		/// </summary>
		/// <returns></returns>
		public string ToSVG()
		{
			StringBuilder res = new StringBuilder();
			bool first = true;
			foreach(var p in Points)
			{
				if( !first )
				{
					res.Append(string.Format(", {0} {1} ", p.X, p.Y));
				}
				else
				{
					res.Append(string.Format("{0} {1} ", p.X, p.Y));
					first = false;
				}
					
			}
			
			if( IsClosed )
				res.Append(" z ");
			
			return "M " + res.ToString();
		}

		protected void CloneAttributes(GraphicsMultiPoint obj)
		{
			obj.Brush = this.Brush;
			Points.ToList().ForEach(x => obj.AddPoint(x.X, x.Y));  //клонируем все вершины
			obj.Id = Id;
			obj.IsClosed = IsClosed;
			obj.OwnerCanvas = OwnerCanvas;
			//obj.IsSelected = IsSelected;
			//obj.Mode = Mode;
		}

        public SerializeBase GetSerializebleObject<T>()
        {
        	return new SerializeMultiPoint(this);
        }
	}
}

/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.04.2014
 * Время: 16:06
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
//using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// состояния, в которых может пребывать объект
	/// </summary>
    public enum GraphicsMode
    {
    	None, 		//не выделен, просто рисуется
    	Selected, 	//выделен для перемещения, масштабирования, вращения
    	Points,		//показывает все свои точки
    	
    	Max
    }

	/// <summary>
	/// Description of GraphicsBase.
	/// </summary>
	public class GraphicsBase: DrawingVisual
	{
		public GraphicsBase()
		{
			Id = this.GetHashCode();
		}

		public int Id{set;get;}
		
		public GraphicsMode Mode{set; get;}

        public void RefreshDrawing()
        {
            DrawingContext dc = this.RenderOpen();
            
            Draw(dc);

            dc.Close();
        }	

        public virtual void Draw(DrawingContext drawingContext){}

        public bool IsSelected
        {
            get 
            { 
                return Mode == GraphicsMode.Selected; 
            }
            set 
            {
           		Mode = value? GraphicsMode.Selected: GraphicsMode.None;

                RefreshDrawing();
            }
        }	
  
        public bool IsHit
        {
            get 
            { 
                return _isHit; 
            }
            set
            {
                _isHit = value;

                RefreshDrawing();
            }
        }	
		private bool _isHit;  

		/// <summary>
        /// Move handle to new point (resizing)
        /// </summary>
        public virtual void MoveHandleTo(Point point, int handleNumber){}

        public virtual void MoveLastHandleTo(Point point){}        

        /// <summary>
        /// попадает ли точка в объект
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool Contains(Point point){return false;}

        public virtual void DrawTracker(DrawingContext aCanvas){}

        public virtual GraphicsBase Clone(){return null;}
	}
}

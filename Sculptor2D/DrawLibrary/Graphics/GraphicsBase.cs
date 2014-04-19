/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.04.2014
 * Время: 16:06
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
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

//TODO: !!! реализовать машину состояний, чтобы Tool не мучался.
//т.е. Tool просто посылает OverMouse, или Down, или Up определенному объекту и все
//Canvas может принимать несколько событий: Move, Down, Up, Drag и передавать их объектам

	/// <summary>
	/// Description of GraphicsBase.
	/// </summary>
	public class GraphicsBase: DrawingVisual
	{
		public GraphicsBase()
		{
		}

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
                return _isSelected; 
            }
            set 
            {
                _isSelected = value;

                RefreshDrawing();
            }
        }	
		private bool _isSelected;      

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
		/// преобразует объект в состояние "Глина". обратное преобразование невозможно
		/// </summary>
		/// <returns></returns>
		//TODO: закончить
		public GraphicsClay toClay()
		{
			_isClay = true;
			
			return new GraphicsClay(this);
		}
		private bool _isClay;

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
	}
}

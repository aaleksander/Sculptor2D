﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.04.2014
 * Время: 16:06
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using DrawLibrary.Misc;
using DrawLibrary.Serialize;

//using System.Drawing;



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
	public class GraphicsBase: DrawingVisual, INotifyPropertyChanged
	{
		
		public event PropertyChangedEventHandler PropertyChanged;

		public void onPropertyChanged(string aProp){			
			_onPropertyChanged(new PropertyChangedEventArgs(aProp));						
		}		

		protected void _onPropertyChanged(PropertyChangedEventArgs e)
		{
			if( PropertyChanged != null )
				PropertyChanged(this, e);
		}		
		
		public GraphicsBase()
		{
			Id = GetHashCode();
		}

		public GraphicsBase(DrawingCanvas aCanvas)
		{
			Id = this.GetHashCode();
			OwnerCanvas = aCanvas;
		}

		public GraphicsBase(SerializeBase aObj)
		{
			Id = aObj.Id;
			OwnerCanvas = aObj.ownerCanvas;
			IsSelected = aObj.isSelected;
			Mode = aObj.mode;
			//TODO: прозрачность
		}

/*		public byte Alpha{
			set{
				SetAlpha(value);
				
				onPropertyChanged("Alpha");
			}
			get{
				return _alpha;
			}
		}
		private byte _alpha;*/
		
		public int Id{set;get;}

		public GraphicsMode Mode{
			set{
				_mode = value;
				RefreshDrawing();
			}
			get{
				return _mode;
			}
		}
		private GraphicsMode _mode;
		public DrawingCanvas OwnerCanvas{set;get;}

		/// <summary>
		/// Слой, которому принадлежит объект
		/// </summary>
		public Layer Layer{set;get;}

        public void RefreshDrawing()
        {
            DrawingContext dc = this.RenderOpen();

            if( OwnerCanvas != null )
            {
            	OwnerCanvas.RemoveService(this);
            }
            
            Draw(dc);

            dc.Close();
        }

        public virtual void Draw(DrawingContext drawingContext){}

        public bool IsSelected
        {
            get 
            { 
                return Mode == GraphicsMode.Selected || Mode == GraphicsMode.Points; 
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

        public virtual Point GetCenter(){return new Point(0, 0);}

        /// <summary>
        /// попадает ли точка в объект
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool Contains(Point point){return false;}

        public virtual void DrawTracker(DrawingContext aCanvas){}
        public virtual void DrawPoints(){}

//        protected virtual void SetAlpha(byte a)
//        {
//        	_alpha = a;
//        	RefreshDrawing();
//        }
        
//        public virtual GraphicsBase Clone(){return null;}

        public virtual SerializeBase GetSerializebleObject()
        {
        	SerializeBase res = new SerializeBase(this);

        	return res;
        }
	}
}

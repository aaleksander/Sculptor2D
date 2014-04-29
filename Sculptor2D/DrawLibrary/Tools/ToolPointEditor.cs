/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 29.04.2014
 * Время: 13:05
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DrawLibrary.Graphics;

namespace DrawLibrary.Tools
{
	/// <summary>
	/// редактор объектов поточечно
	/// </summary>
	public class ToolPointEditor:ToolPointer
	{
		public override void OnUp(DrawingCanvas aCanvas, Point aPoint)
		{
			var o = GetHitObject(aCanvas, aPoint);
			if( o == null && IsDragging == false ) //просто клик по пустому месту
			{
				_dragObject = null;
				aCanvas.UnselectAll();
			}

			if( IsDragging == false && _dragObject != null ) //просто клик по объекту
			{
				if( !(_dragObject is GraphicsClay ) )
				{
					aCanvas.UnselectAll(); //TODO: добавить проверку shifta (делать анселект, только если шифт не нажат)				
					aCanvas.SelectObject(_dragObject, GraphicsMode.Points );
				}
			}

			aCanvas.ReleaseMouseCapture();
		}
		
		public override void Init(DrawingCanvas drawingCanvas)
		{
			if( drawingCanvas.SelectedObject != null && !(drawingCanvas.SelectedObject is GraphicsClay))
			{
				drawingCanvas.SelectedObject.Mode = GraphicsMode.Points;
			}
		}

        public override void OnDown(DrawingCanvas aCanvas, Point aPoint)//MouseButtonEventArgs e)
        {        	
			_startDragging = aPoint;

        	var o = GetHitObject(aCanvas, aPoint);

        	if( Mouse.LeftButton == MouseButtonState.Pressed )
        	{
        		_dragObject = o;
        	}
        }
        
        public override void OnMove(DrawingCanvas aCanvas, Point aPoint, bool aPressed ) 
        {
        	if( Geometry.dist(_startDragging, aPoint) > 5 )
        		IsDragging = aPressed;

        	if( !aPressed )
        		SimpleMove(aCanvas, aPoint);
      	
        	if( aPressed )
        	{
				if( _dragObject != null && _dragObject is GraphicsServicePoint ) //тащим точку
				{
					((GraphicsServicePoint)_dragObject).Move(aPoint);
					/*
					_dragObject.Transform = new TranslateTransform(aPoint.X - _startDragging.X, aPoint.Y - _startDragging.Y);
		            //обновим матрицы у сервисных объектов
		            foreach(var o in aCanvas.GraphicsList)
		            {
		            	if( o is GraphicsService && ((GraphicsService)o).IsYourOwner(_dragObject))
		            	{
		            		
		            		//((GraphicsBase)o).Transform = _dragObject.Transform;
		            		//((GraphicsBase)o).RefreshDrawing();
		            	}
		            }*/
				}
        	}
        }
	}
}

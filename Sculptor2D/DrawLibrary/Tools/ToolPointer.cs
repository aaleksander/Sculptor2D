/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 17.04.2014
 * Время: 12:25
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DrawLibrary.Graphics;
using Helpers;

//TODO:редактор точек
//TODO:масштабирование, вращение фигур
//перемещение вершины
namespace DrawLibrary.Tools
{
	/// <summary>
	/// стандартный указатель
	/// </summary>
	public class ToolPointer: ToolBase
	{
        public override void OnMove(DrawingCanvas aCanvas, Point aPoint, bool aPressed )
        {
        	if( Geometry.dist(_startDragging, aPoint) > 5 )
        		IsDragging = aPressed;

			if ( !aPressed ) //ничего не тащим, просто двигаемся
			{
				//почистить у всех IsHit
				foreach(var o in aCanvas.GraphicsList)
					((GraphicsBase)o).IsHit = false;
				
				_dragObject = null;
				for(int i=aCanvas.Count - 1; i>=0; i-- ) //идем сверху-вниз
				{ //просто подсвечиваем
					GraphicsBase o = aCanvas[i];
					if( o != null )
					{
				   		o.IsHit = o.Contains(aPoint);
				   		if( o.IsHit )
				   			break; //Горячим может быть только один объект
					}
				}
			}

			if( aPressed )
			{//что-то тащим
				if( Keyboard.IsKeyDown(Key.LeftCtrl) ) //поворачиваем
				{
					if( aCanvas.SelectedObject != null )
					{
						var c = aCanvas.SelectedObject.GetCenter();
						var a  = Geometry.GetAngle(_startDragging, c, aPoint, c);
						aCanvas.SelectedObject.Transform = new RotateTransform(a, c.X, c.Y);
						aCanvas.SelectedObject.RefreshDrawing();
						_startDragging = aPoint;
					}
				}
				else
				{
					if( _dragObject != null ) //тащим
						_dragObject.Transform = new TranslateTransform(aPoint.X - _startDragging.X, aPoint.Y - _startDragging.Y);
				}
			}
        }

        public override void OnDown(DrawingCanvas aCanvas, Point aPoint)//MouseButtonEventArgs e)
        {        	
			_startDragging = aPoint;

        	var o = GetHitObject(aCanvas, aPoint);

        	if( Mouse.LeftButton == MouseButtonState.Pressed ) //e.LeftButtonPressed() ) //нажали над каким-то  объектом
        	{
        		_dragObject = o;
        	}
        }

		public override void OnUp(DrawingCanvas aCanvas, Point aPoint)
		{
			var o = GetHitObject(aCanvas, aPoint);
			if( o == null && IsDragging == false) //просто клик по пустому месту
			{
				_dragObject = null;
				aCanvas.UnselectAll();
			}

			if( IsDragging == false && _dragObject != null ) //просто клик по объекту
			{
				aCanvas.UnselectAll(); //TODO: добавить проверку shifta (делать анселект, только если шифт не нажат)				
				aCanvas.SelectObject(_dragObject, GraphicsMode.Selected);
			}

			if( IsDragging == true )
			{//чето таскалли
				_dragObject = null; //отпустили перетаскиваемый объект
			}

			aCanvas.ReleaseMouseCapture();
		}

        private GraphicsBase GetHitObject(DrawingCanvas aCanvas, Point aPoint)
        {        	
			for(int i = aCanvas.Count - 1; i >= 0; i-- ) //объекты сверху должны "попасть" под мышку первыми
			{
				GraphicsBase o = aCanvas[i];
				if( o != null )
				{
					if( o.Contains(aPoint) )
					{
						return o;
					}
				}
			}
			return null;
        }

        private GraphicsBase _dragObject;        
        private Point _startDragging;
        
        
		public override void SetCursor(DrawingCanvas drawingCanvas)
		{
			drawingCanvas.SetCursor(DrawingCursorType.None);
			drawingCanvas.Cursor = Cursors.Arrow;
		}
	}
}

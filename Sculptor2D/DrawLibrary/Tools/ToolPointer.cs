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

//TODO: редактор точек
//TODO: масштабирование, вращение фигур
//перемещение вершины
namespace DrawLibrary.Tools
{
	/// <summary>
	/// стандартный указатель
	/// </summary>
	public class ToolPointer: ToolBase
	{
        public override void OnMouseMove(DrawingCanvas aCanvas, MouseEventArgs e)
        {
        	IsDragging = e.AnyButtonPressed();
			Point point = e.GetPosition(aCanvas);

			if ( e.AllButtonReleases() ) //ничего не тащим, просто двигаемся
			{
				_dragObject = null;
				for(int i=0; i<aCanvas.Count; i++ )
				{ //просто подсвечиваем
					var o = aCanvas[i];
				   	o.IsHit = o.Contains(point);
				}
			}

			if( e.LeftButtonPressed() && _dragObject != null )
			{//что-то тащим
				_dragObject.Transform = new TranslateTransform(point.X - _startDragging.X, point.Y - _startDragging.Y);
			}
        }

        public override void OnMouseDown(DrawingCanvas aCanvas, MouseButtonEventArgs e)
        {
        	Point point = e.GetPosition(aCanvas);
			_startDragging = point;

        	var o = GetHitObject(aCanvas, point);

        	if( e.LeftButtonPressed() ) //нажали над каким-то  объектом
        	{
        		_dragObject = o;
        	}
        }

		public override void OnMouseUp(DrawingCanvas aCanvas, MouseButtonEventArgs e)
		{
			Point point = e.GetPosition(aCanvas);
			var o = GetHitObject(aCanvas, point);
			if( o == null && IsDragging == false) //просто клик по пустому месту
			{
				_dragObject = null;
				aCanvas.UnselectAll();
			}

			if( IsDragging == false && _dragObject != null ) //просто клик по объекту
			{
				aCanvas.UnselectAll(); //TODO: добавить проверку shifta (делать анселект, только если шифт не нажат)
				_dragObject.IsSelected = true;
			}

			if( IsDragging == true )
			{//чето таскалли
				_dragObject = null; //отпустили перетаскиваемый объект
			}
			/*if( _dragObject != null )
			{
				_dragObject.RefreshDrawing();
			}*/

			aCanvas.ReleaseMouseCapture();
		}

        private GraphicsBase GetHitObject(DrawingCanvas aCanvas, Point aPoint)
        {        	
			for(int i = aCanvas.Count - 1; i >= 0; i-- ) //объекты сверху должны "попасть" под мышку первыми
			{
				var o = aCanvas[i];
				if( o.Contains(aPoint) )
					return o;
			}
			return null;
        }

        private GraphicsBase _dragObject;        
        private Point _startDragging;
	}
}

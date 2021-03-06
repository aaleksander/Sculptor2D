﻿/*
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

//TODO: масштабирование фигур и история для этого
//TODO: групповое выделение
namespace DrawLibrary.Tools
{
	/// <summary>
	/// стандартный указатель
	/// </summary>
	public class ToolPointer: ToolBase
	{
		/// <summary>
		/// простое перемещение указателя
		/// </summary>
		/// <param name="aCanvas"></param>
		/// <param name="aPoint"></param>
		protected void SimpleMove(DrawingCanvas aCanvas, Point aPoint)
		{
			//почистить у всех IsHit
			//foreach(var o in aCanvas.GraphicsList)
			for(int i=aCanvas.GraphicsList.Count - 1; i >= 0; i--)
			{
				((GraphicsBase)aCanvas.GraphicsList[i]).IsHit = false;
			}

			_dragObject = null;
			for(int i=aCanvas.Count - 1; i>=0; i-- ) //идем сверху-вниз
			{ //просто подсвечиваем
				GraphicsBase o = aCanvas[i];
		   		o.IsHit = o.Contains(aPoint);
		   		if( o.IsHit )
		   		{
		   			aCanvas.UpdateServiceObjects();
		   			break; //Горячим может быть только один объект
		   		}
			}
		}
		
        public override void OnMove(DrawingCanvas aCanvas, Point aPoint, bool aPressed )
        {
        	if( Geometry.dist(_startDragging, aPoint) > 5 )
        		IsDragging = aPressed;

			if ( !aPressed ) //ничего не тащим, просто двигаемся
				SimpleMove(aCanvas, aPoint);

			if( aPressed )
			{//что-то тащим
				if( Keyboard.IsKeyDown(Key.LeftCtrl) ) //поворачиваем
				{
					if( aCanvas.SelectedObject != null )
					{
						var c = aCanvas.SelectedObject.GetCenter();
						var a  = Geometry.GetAngle(_startDragging, c, aPoint, c);
						aCanvas.SelectedObject.Transform = new RotateTransform(a, c.X, c.Y);
						AddObjectToModified(aCanvas.SelectedObject);
			            //обновим матрицы у сервисных объектов
			            foreach(var o in aCanvas.GraphicsList)
			            {
			            	if( o is GraphicsService && ((GraphicsService)o).IsYourOwner(aCanvas.SelectedObject))
			            	{
			            		((GraphicsBase)o).Transform = aCanvas.SelectedObject.Transform;
			            		((GraphicsBase)o).RefreshDrawing();			            		
			            	}
			            }

			            aCanvas.SelectedObject.RefreshDrawing();
						
						_startDragging = aPoint;
					}
				}
				else
				{
					if( _dragObject != null ) //тащим
					{
						_dragObject.Transform = new TranslateTransform(aPoint.X - _startDragging.X, aPoint.Y - _startDragging.Y);
						AddObjectToModified(_dragObject );
			            //обновим матрицы у сервисных объектов
			            foreach(var o in aCanvas.GraphicsList)
			            {
			            	if( o is GraphicsService && ((GraphicsService)o).IsYourOwner(_dragObject))
			            	{
			            		((GraphicsBase)o).Transform = _dragObject.Transform;			            		
			            		//((GraphicsBase)o).RefreshDrawing();
			            	}
			            }						
					}
				}
			}
        }

        public override void OnDown(DrawingCanvas aCanvas, Point aPoint)//MouseButtonEventArgs e)
        {        	
        	InitObjectsForHistory(aCanvas, x => x is GraphicsBase);
        	
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
			if( o == null && IsDragging == false && !Keyboard.IsKeyDown(Key.LeftCtrl) ) //просто клик по пустому месту
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
			AddModifiedToHistory(aCanvas);

			aCanvas.ReleaseMouseCapture();
		}

        protected GraphicsBase GetHitObject(DrawingCanvas aCanvas, Point aPoint)
        {
			for(int i = aCanvas.Count - 1; i >= 0; i-- ) //объекты сверху должны "попасть" под мышку первыми
			{
				GraphicsBase o = aCanvas[i];
				if( o != null )
				{
					if( o is GraphicsService ) //у сервисных объектов нет слоев, они всегда сверху
					{
						if( o.Contains(aPoint) )
						{
								return o;
						}
					}
					else
					{
						if( o.Layer.IsVisible && o.Contains(aPoint))
							return o;
					}
						
				}
			}
			return null;
        }

        protected GraphicsBase _dragObject;        
        protected Point _startDragging;


		public override void SetCursor(DrawingCanvas drawingCanvas)
		{
			drawingCanvas.SetCursor(DrawingCursorType.None);
			drawingCanvas.Cursor = Cursors.Arrow;
		}
		
		public override void Init(DrawingCanvas drawingCanvas)
		{
			if( drawingCanvas.SelectedObject != null )
			{
				drawingCanvas.SelectedObject.Mode = GraphicsMode.Selected;
			}
		}
		
	}
}

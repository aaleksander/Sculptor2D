/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 17.04.2014
 * Время: 16:48
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using System.Windows.Input;
using DrawLibrary.Graphics;
using DrawLibrary.Undo;

namespace DrawLibrary.Tools
{
	/// <summary>
	/// базовый класс для всякий многоточечных объектов (линии и полигоны)
	/// </summary>
	public class ToolMultiPoint<T>: ToolBase
		where T: GraphicsMultiPoint, new()
	{
		public ToolMultiPoint(): base()
		{
		}

		protected Func<GraphicsBase> addFunc = null;
		
		private T _obj = null; //рисуемый в данный момент объект

		/// <summary>
		/// нажатие како-нибудь кнопки
		/// </summary>
		/// <param name="aCanvas"></param>
		/// <param name="aKey"></param>
		public override void KeyDown(DrawingCanvas aCanvas, Key aKey)
		{
        	if( aKey == Key.Escape )
            { //закончили рисование
            	if( _obj != null )
            	{
            		_obj.DeleteLastPoint(); //удаляем последнюю которая тянулась за мышкой            	
            		_obj.IsSelected = false;
            		_obj.OwnerCanvas = aCanvas;
            		_obj.RefreshDrawing();
            		aCanvas.AddActionToHistory(new ActionAdd(_obj));
            		_obj = null;
            		aCanvas.ReleaseMouseCapture();            		
            		return; 
            	}
            }
		}

		public override void OnDown(DrawingCanvas drawingCanvas, Point aPoint)//, MouseButtonEventArgs e)
        {
            if( _obj == null )
            { //начинаем рисовать новый объект
            	_obj = new T();
            	_obj.AddPoint(aPoint); //это начало линии

            	AddNewObject(drawingCanvas, _obj); //добавляем новый объект
            	//drawingCanvas.AddObject(_obj);
            	_obj.IsSelected = false;
            	drawingCanvas.CaptureMouse();
            }

            //новая точка, она теперь будет тянуться за мышкой
           	_obj.AddPoint(aPoint);
        }

        public override void OnMove(DrawingCanvas drawingCanvas, Point aPoint, bool aPressed)
        {
        	if( _obj == null ) //пока нечего рисовать
        		return;

        	if( _obj.Count == 0 )
        		return;

        	//var p = e.GetPosition(drawingCanvas);
        	drawingCanvas[drawingCanvas.Count - 1].MoveLastHandleTo(aPoint);
        }


		public override void SetCursor(DrawingCanvas drawingCanvas)
		{
			drawingCanvas.SetCursor(DrawingCursorType.None);
			drawingCanvas.Cursor = Cursors.Cross;
		}
        
	}
}

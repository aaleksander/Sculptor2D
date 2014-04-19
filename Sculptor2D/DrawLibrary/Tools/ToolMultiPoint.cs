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

        public override void OnMouseDown(DrawingCanvas drawingCanvas, MouseButtonEventArgs e)
        {
            if( e.RightButton == MouseButtonState.Pressed )
            { //закончили рисование
            	if( _obj != null )
            	{
            		_obj.DeleteLastPoint(); //удаляем последнюю которая тянулась за мышкой            	
            		_obj.IsSelected = false;
            		_obj = null;
            		drawingCanvas.ReleaseMouseCapture();            		
            		return; 
            	}
            }

            if( e.LeftButton == MouseButtonState.Pressed )
            {
				Point p = e.GetPosition(drawingCanvas);            
	            if( _obj == null )
	            { //начинаем рисовать новый объект
	            	_obj = new T();
	            	_obj.AddPoint(p); //это начало линии
	            
	            	AddNewObject(drawingCanvas, _obj); //добавляем новый объект
	            	_obj.IsSelected = false;
	            }

	            //новая точка, она теперь будет тянуться за мышкой
	           	_obj.AddPoint(p);
            }
           	//drawingCanvas.CaptureMouse();
        }

        public override void OnMouseMove(DrawingCanvas drawingCanvas, MouseEventArgs e)
        {
        	if( _obj == null ) //пока нечего рисовать
        		return;

        	if( _obj.Count == 0 )
        		return;

        	var p = e.GetPosition(drawingCanvas);
        	drawingCanvas[drawingCanvas.Count - 1].MoveLastHandleTo(p);
        }		
		
	}
}

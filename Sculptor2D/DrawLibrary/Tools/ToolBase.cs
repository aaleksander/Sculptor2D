/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 04/16/2014
 * Время: 13:42
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
	/// доступные инструменты рисования
	/// </summary>
    public enum ToolType
    {
        None,    //ничего
        Pointer, //указатель        
        Editor,	 //редактор точек
//        Rectangle,
//        Ellipse,
        Line,    //линия
        Polygone,
//        PolyLine,
//        Text,
        

		Max //это, чтобы знать, где конец перечисления
    };		
	
	/// <summary>
	/// Базовый класс для всех рисовальных инструментов
	/// </summary>
	public class ToolBase
	{
        /// <summary>
        /// Курсор инструмента
        /// </summary>
        protected Cursor ToolCursor{
            get{
                return toolCursor;
            }
            set{
                toolCursor = value;
            }
        }
        private Cursor toolCursor;

        public virtual  void OnMouseClick(DrawingCanvas drawingCanvas, MouseButtonEventArgs e){}

		public virtual void OnMouseDown(DrawingCanvas drawingCanvas, MouseButtonEventArgs e){}

		public virtual void OnMouseMove(DrawingCanvas drawingCanvas, MouseEventArgs e){}

		public virtual void OnMouseUp(DrawingCanvas drawingCanvas, MouseButtonEventArgs e){}

		public virtual void SetCursor(DrawingCanvas drawingCanvas){}
		
		/// <summary>
		/// добавляем новый объект на канву
		/// </summary>
		/// <param name="drawingCanvas"></param>
		/// <param name="o"></param>
        protected static void AddNewObject(DrawingCanvas aCanvas, GraphicsBase o)
        {
            //HelperFunctions.UnselectAll(drawingCanvas);

            o.IsSelected = true;
            //o.Clip = new RectangleGeometry(new Rect(0, 0, drawingCanvas.ActualWidth, drawingCanvas.ActualHeight));

            aCanvas.GraphicsList.Add(o);
            //drawingCanvas.CaptureMouse();
            aCanvas.ReleaseMouseCapture();
        }

		//перетаскивание        
        public bool IsDragging{set;get;}
	}
}

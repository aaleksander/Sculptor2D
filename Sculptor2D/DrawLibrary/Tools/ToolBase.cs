/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 04/16/2014
 * Время: 13:42
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DrawLibrary.Graphics;
using DrawLibrary.Undo;

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
        Line,    //линия
        Polygone,

        Brush,
        
		Max //это, чтобы знать, где конец перечисления
    };		
	
	/// <summary>
	/// Базовый класс для всех рисовальных инструментов
	/// </summary>
	public class ToolBase
	{
		
		#region для undo/redo
		protected Collection<GraphicsBase> _objects;  //сюда поместятся все объекты, которые можно изменить (для последующего undo)
		protected Collection<int> _modifiedIDs = new Collection<int>(); //список идешников объектов, у которых что-то поменялось

		/// <summary>
		/// инициализируем объекты для последующих откатов
		/// </summary>
		/// <param name="aCanvas"></param>
		protected void InitObjectsForHistory(DrawingCanvas aCanvas)
		{
			_objects = aCanvas.GetPotentObjects();
			_modifiedIDs.Clear();			
		}

		/// <summary>
		/// какой-то объект в ходе действия поменялся, надо его запомнить
		/// </summary>
		/// <param name="aObject"></param>
		protected void AddObjectToModified(GraphicsBase aObject)
		{
			if( !_modifiedIDs.Contains(aObject.Id) )
				_modifiedIDs.Add(aObject.Id);
		}

		protected void AddModifiedToHistory(DrawingCanvas aCanvas)
		{
			aCanvas.AddActionToHistory(new ActionModify(_objects, _modifiedIDs));
		}
		#endregion для undo/redo
			

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

        //public virtual  void OnMouseClick(DrawingCanvas drawingCanvas, MouseButtonEventArgs e){}

        public virtual void OnDown(DrawingCanvas drawingCanvas, Point aPoint){}//, MouseButtonEventArgs e){}

		public virtual void OnMove(DrawingCanvas drawingCanvas, Point aPoint, bool aPressed){}

		public virtual void OnUp(DrawingCanvas drawingCanvas, Point aPoint){}

		public virtual void KeyDown(DrawingCanvas drawingCanvas, Key aKey){}
		
		public virtual void SetCursor(DrawingCanvas drawingCanvas){}
		
		public virtual void Init(DrawingCanvas drawingCanvas){}
		
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
            //aCanvas.ReleaseMouseCapture();
        }

		//перетаскивание        
        public bool IsDragging{set;get;}
	}
}

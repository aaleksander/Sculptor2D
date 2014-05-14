/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 25.04.2014
 * Время: 12:53
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using DrawLibrary.Graphics;

namespace DrawLibrary.Undo
{
	/// <summary>
	/// действие - добавить новый объект
	/// </summary>
	public class ActionAdd: ActionBase
	{
        GraphicsBase _newObjectClone;

        // Create this command with DrawObject instance added to the list
        public ActionAdd(GraphicsBase newObject): base()
        {
            // Keep copy of added object
            _newObjectClone = newObject.Clone();
        }

        /// <summary>
        /// Delete added object
        /// </summary>
        public override void Undo(DrawingCanvas drawingCanvas)
        {
            // Find object to delete by its ID.
            // Don't use objects order in the list.

            GraphicsBase objectToDelete = null;

            // Remove object from the list
            foreach(GraphicsBase b in drawingCanvas.GraphicsList)
            {
                if ( b.Id == _newObjectClone.Id )
                {
                    objectToDelete = b;
                    break;
                }
            }

            if ( objectToDelete != null )
            {
            	drawingCanvas.RemoveService(objectToDelete);
                drawingCanvas.GraphicsList.Remove(objectToDelete); 
                //drawingCanvas.RemoveObject(objectToDelete);
            }
        }

        /// <summary>
        /// Add object again
        /// </summary>
        public override void Redo(DrawingCanvas drawingCanvas)
        {
            //HelperFunctions.UnselectAll(drawingCanvas);

            // Create full object from the clone and add it to list
            //drawingCanvas.GraphicsList.Add(_newObjectClone.CreateGraphics());

            // Object created from the clone doesn't contain clip information,
            // refresh it.
            //drawingCanvas.RefreshClip();
        }
	}
}

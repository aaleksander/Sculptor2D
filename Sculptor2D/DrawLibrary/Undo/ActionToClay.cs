/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 25.04.2014
 * Время: 13:24
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using DrawLibrary.Graphics;
using DrawLibrary.Serialize;

namespace DrawLibrary.Undo
{	
	/// <summary>
	/// действие "преобразовать в глину"
	/// </summary>
	public class ActionToClay: ActionBase
	{
		private SerializeBase _oldObj;
		private int _newId;
			
		public ActionToClay(GraphicsBase _old, int aNewId)
		{
			_oldObj = _old.GetSerializebleObject();
			_newId = aNewId;
		}
		
		/// <summary>
		/// восстанавливаем преждний объект
		/// </summary>
		/// <param name="drawingCanvas"></param>
        public override void Undo(DrawingCanvas aCanvas)
        {
            GraphicsBase objectToReplace = null;

            GraphicsBase b;
            int i;
            for(i=0; i<aCanvas.GraphicsList.Count; i++)
            {
            	b = (GraphicsBase)aCanvas.GraphicsList[i];
                if ( b.Id == _newId )
                {
                    objectToReplace = b;
                    break;
                }
        	}

            if ( objectToReplace != null )
            {
            	aCanvas.ReplaceObject(i, _oldObj.CreateGraphicsObject());
            }
        }

        public override void Redo(DrawingCanvas drawingCanvas)
        {
        	
        }
	        
	}
}

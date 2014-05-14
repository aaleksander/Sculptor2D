/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 25.04.2014
 * Время: 15:13
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Linq;
using DrawLibrary.Graphics;

namespace DrawLibrary.Undo
{
	/// <summary>
	/// 
	/// </summary>
	public class ActionModify: ActionBase
	{
        Collection<GraphicsBase> _objects;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aObjects"></param>
        /// <param name="aIds"></param>
        public ActionModify(Collection<GraphicsBase> aObjects, Collection<int> aIDs): base()
        {
        	_objects = new Collection<GraphicsBase>();
        	GraphicsBase o;
        	//клонируем все объекты, для которых есть id
        	foreach(int id in aIDs)
        	{
        		o = aObjects.First(x => x.Id == id);
        		_objects.Add(o.Clone());
        	}
        }

        /// <summary>
        /// Delete added object
        /// </summary>
        public override void Undo(DrawingCanvas aCanvas)
        {
        	GraphicsBase objectToReplace = null;
        	GraphicsBase b;
        	int i;
        	//восстанавливаем все объекты обратно
        	foreach(var o in _objects)
        	{
        		objectToReplace = null;
        		
            	for(i=0; i<aCanvas.GraphicsList.Count; i++)
            	{
            		b = (GraphicsBase)aCanvas.GraphicsList[i];
	                if ( b.Id == o.Id )
	                {
	                    objectToReplace = o;
	                    break;
	                }
	            }

	            if( objectToReplace != null )
	            {
	            	aCanvas.ReplaceObject(i, objectToReplace);
	            	//objectToReplace.RefreshDrawing();
	            }
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

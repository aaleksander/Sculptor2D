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
using DrawLibrary.Serialize;

namespace DrawLibrary.Undo
{
	/// <summary>
	/// 
	/// </summary>
	public class ActionModify: ActionBase
	{
        Collection<SerializeBase> _objects;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aObjects"></param>
        /// <param name="aIds"></param>
        public ActionModify(Collection<SerializeBase> aObjects, Collection<int> aIDs): base()
        {
        	_objects = new Collection<SerializeBase>();
        	SerializeBase o;
        	//клонируем все объекты, для которых есть id
        	foreach(int id in aIDs)
        	{
        		o = aObjects.First(x => x.Id == id);
        		_objects.Add(o);//.CreateGraphicsObject() );
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
        	foreach(SerializeBase o in _objects)
        	{
        		objectToReplace = null;

            	for(i=0; i<aCanvas.GraphicsList.Count; i++)
            	{
            		b = (GraphicsBase)aCanvas.GraphicsList[i];
	                if ( b.Id == o.Id )
	                {
	                	objectToReplace = o.CreateGraphicsObject();
	                	objectToReplace.OwnerCanvas = aCanvas;
	                    break;
	                }
	            }

	            if( objectToReplace != null )
	            {
	            	aCanvas.RemoveService(objectToReplace);
	            	aCanvas.ReplaceObject(i, objectToReplace);
	            	aCanvas.RefreshServiceObjects(objectToReplace); //обновить сервисные объекты этого объекта
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

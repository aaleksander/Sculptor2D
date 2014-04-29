/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 24.04.2014
 * Время: 12:54
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// всякие сервисные штуки, которые должны быть все время сверху (маркеры вершин и прочее)
	/// </summary>
	public class GraphicsService: GraphicsBase
	{
		public GraphicsService(GraphicsBase aObj): base()
		{
			_owner = aObj;
		}
		
		private GraphicsBase _owner; //владелец этого сервисного объекта
		
		public bool IsYourOwner(GraphicsBase aObj)
		{
			return aObj == _owner;
		}		
		
		public GraphicsBase Owner{
			get{ return _owner; }
		}
	}
}

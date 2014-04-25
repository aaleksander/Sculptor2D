/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.04.2014
 * Время: 17:36
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DrawLibrary.Brushes;
using DrawLibrary.Graphics;
using DrawLibrary.Undo;
using Helpers;

namespace DrawLibrary.Tools
{
	/// <summary>
	/// Кисть для работы с глиной
	/// </summary>
	public class ToolBrush: ToolBase
	{
		private BrushBase _brush = new BrushBase(); //текущая кисть	

		private Collection<GraphicsBase> _objects;  //сюда поместятся все объекты, которые можно изменить (для последующего undo)
		private Collection<int> _modifiedIDs = new Collection<int>(); //список идешников объектов, у которых что-то поменялось
		
		public override void OnMove(DrawingCanvas aCanvas, Point aPoint, bool aPressed)
        {
        	IsDragging = aPressed;//e.AnyButtonPressed();

        	if( IsDragging )
        		_brush.AddPath(aPoint);

        	//TODO: если на канве есть выделенные элементы, то редактировать нолько их
			if( aPressed && _brush.Path.Count > 2)
			{//делаем "мазок"
				GraphicsClay o;
				foreach(var clay in aCanvas.GraphicsList)
				{					
					if( clay is Graphics.GraphicsClay ) //если это "глина"
					{
						o = (GraphicsClay)clay;
						if( _brush.Modify(o) )//то кисть ее модифицирует
						{					
							if( !_modifiedIDs.Contains(o.Id) )
								_modifiedIDs.Add(o.Id);
							((GraphicsClay)clay).UpdateClay();
						}						
					}
				}
			}
        }

		public override void OnDown(DrawingCanvas aCanvas, Point aPoint)//, MouseButtonEventArgs e)
		{
			_objects = aCanvas.GetPotentObjects(); //клонируем себе объектов
			_modifiedIDs.Clear();

			//_brush.AddPath(e.GetPosition(aCanvas)); //самая первая точка
			_brush.AddPath(aPoint); //самая первая точка
		}

		public override void OnUp(DrawingCanvas aCanvas, Point aPoint)
		{
			aCanvas.AddActionToHistory(new ActionModify(_objects, _modifiedIDs));
			_brush.ClearPath();
		}

		public BrushBase Brush
		{
			set{
				_brush = value;
			}
			get{
				return _brush;
			}
		}
	}
}

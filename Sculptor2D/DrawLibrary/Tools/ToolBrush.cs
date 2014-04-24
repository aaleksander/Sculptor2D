/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.04.2014
 * Время: 17:36
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using System.Windows.Input;
using DrawLibrary.Brushes;
using DrawLibrary.Graphics;
using Helpers;

namespace DrawLibrary.Tools
{
	/// <summary>
	/// Кисть для работы с глиной
	/// </summary>
	public class ToolBrush: ToolBase
	{
		private BrushBase _brush = new BrushBase(); //текущая кисть	

		public override void OnMouseMove(DrawingCanvas aCanvas, MouseEventArgs e)
        {
        	IsDragging = e.AnyButtonPressed();

        	if( IsDragging )
        		_brush.AddPath(e.GetPosition(aCanvas));

        	//TODO: если на канве есть выделенные элементы, то редактировать нолько их
			if( e.LeftButtonPressed() )
			{//делаем "мазок"
				IsDragging = true;
				foreach(var clay in aCanvas.GraphicsList)
				{
					if( clay is Graphics.GraphicsClay ) //если это "глина"
					{
						if( _brush.Modify((GraphicsClay)clay) ) //то кисть ее модифицирует
							((GraphicsClay)clay).UpdateClay();
					}
				}
			}
        }

		public override void OnMouseDown(DrawingCanvas aCanvas, MouseButtonEventArgs e)
		{
			_brush.AddPath(e.GetPosition(aCanvas)); //самая первая точка
		}
		
		public override void OnMouseUp(DrawingCanvas aCanvas, MouseButtonEventArgs e)
		{
			if( IsDragging == true ) //чето мазали, надо обновить
			{
				foreach(var clay in aCanvas.GraphicsList)
				{
					if( clay is Graphics.GraphicsClay ) //если это "глина"
					{
						//((GraphicsClay)clay).UpdateClay();
					}
				}
			}
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

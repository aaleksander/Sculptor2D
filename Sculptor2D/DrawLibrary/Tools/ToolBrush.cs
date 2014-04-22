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

        	_brush.AddPath(e.GetPosition(aCanvas));

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

				_brush.ClearPath();
			}
		}
		
		public BrushType Brush
		{
			set{
				switch(value)
				{
						case BrushType.OutMover: _brush = new BrushOutMover(); break;
						case BrushType.Smoother: _brush = new BrushSmoother(); break;
				}
			}
			get{
				return _brush.Type;
			}
		}
	}
}

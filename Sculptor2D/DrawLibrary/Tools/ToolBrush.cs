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

			_brush.Point = e.GetPosition(aCanvas);;
			
			if( e.LeftButtonPressed() )
			{//делаем "мазок"
				foreach(var clay in aCanvas.GraphicsList)
				{
					if( clay is Graphics.GraphicsClay ) //если это "глина"
					{
						((GraphicsClay) clay).Brushed(_brush); //посылаем ей "мазок"
					}
				}
			}
        }
	}
}

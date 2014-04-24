/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.04.2014
 * Время: 13:25
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DrawLibrary;
using DrawLibrary.Graphics;
using DrawLibrary.Tools;

namespace Sculptor2D
{
	
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{		
		public String CanvasEvent{set;get;}
		public Window1()
		{
			InitializeComponent();
			
			//связываем кнопки с холстом
			buttonToolPointer.PreviewMouseDown += new MouseButtonEventHandler(ToolButton_PreviewMouseDown);
			buttonToolEditor.PreviewMouseDown += new MouseButtonEventHandler(ToolButton_PreviewMouseDown);
			buttonToolLine.PreviewMouseDown += new MouseButtonEventHandler(ToolButton_PreviewMouseDown);
			buttonToolPolygone.PreviewMouseDown += new MouseButtonEventHandler(ToolButton_PreviewMouseDown);
			buttonToolBrush.PreviewMouseDown += new MouseButtonEventHandler(ToolButton_PreviewMouseDown);
			
			canvas.CanvasEvent += new DrawingCanvas.CanvasEventHandler(Canvas_CanvasEvent);
			
			//тестовые данные			
/*			var p = new GraphicsPolygon();
			p.AddPoint(500, 100);
			p.AddPoint(300, 300);
			p.AddPoint(150, 250);
			canvas.GraphicsList.Add(p);
			
			p = new GraphicsPolygon();
			p.AddPoint(60, 60);
			p.AddPoint(400, 50);
			p.AddPoint(400, 400);
			p.AddPoint(50, 400);
			canvas.GraphicsList.Add(p); */
		}

	
		void Canvas_CanvasEvent(object sender, CanvasEventArgs e)
		{
			statusBar.Content = e.Text;			
		}
		
        void ToolButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            canvas.Tool = (ToolType)Enum.Parse(typeof(ToolType),
                ((System.Windows.Controls.Primitives.ButtonBase)sender).Tag.ToString());

        	//пляска с Capture нужна, чтобы первый клик приходил, иначе начинает рисовать только со второго клика.
        	canvas.CaptureMouse();
        	canvas.Focus();
        	canvas.ReleaseMouseCapture();

            e.Handled = true;
        }
	}
}
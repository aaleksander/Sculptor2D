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
using DrawLibrary.Brushes;
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
		Point? lastMousePositionOnTarget;
		Point? lastCenterPositionOnTarget;
		
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
			
			slider.ValueChanged += OnSliderValueChanged;
			
			scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;
			scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;			
			
			slider.Value = 5;
			
			

			
			//тестовые данные			
/*			var p = new GraphicsPolygon();
			p.OwnerCanvas = canvas;
			p.AddPoint(500, 100);
			p.AddPoint(300, 300);
			p.AddPoint(150, 250);
			canvas.GraphicsList.Add(p);
		
			p = new GraphicsPolygon();
			p.OwnerCanvas = canvas;
			p.AddPoint(60, 60);
			p.AddPoint(400, 50);
			p.AddPoint(400, 400);
			p.AddPoint(50, 400);
			canvas.GraphicsList.Add(p);*/
		}

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value += 0.7;
            }
            if (e.Delta < 0)
            {
                slider.Value -= 0.7;
            }

            e.Handled = true;
        }
        
        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollViewer.ViewportWidth/2, scrollViewer.ViewportHeight/2);
                        Point centerOfTargetNow = scrollViewer.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth/grid.Width;
                    double multiplicatorY = e.ExtentHeight/grid.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset - dXInTargetPixels*multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset - dYInTargetPixels*multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }        
        
        
        #region Нажатие хитрых клавиш
        private BrushType _prevBrush = BrushType.None;
        protected override void OnKeyDown(KeyEventArgs e)
        {
        	if( e.Key == Key.LeftShift && canvas.Tool == ToolType.Brush && canvas.Brush.Type != BrushType.Smoother )
        	{//нажали shift во время скульптинга
        		_prevBrush = canvas.Brush.Type; //запомнили, что было
        		canvas.SetBrushCommand.Execute("Smoother");
        	}
        }
        
        protected override void OnKeyUp(KeyEventArgs e)
        {
        	if( e.Key == Key.LeftShift && canvas.Tool == ToolType.Brush )
        	{//отпустили шифт во время скульптинга
        		if( _prevBrush != BrushType.None )
        		{
        			canvas.SetBrushCommand.Execute(_prevBrush.ToString());
        		}
        	}
        }

		#endregion
        
        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;
            statusBar.Content = e.NewValue.ToString();

            var centerOfViewport = new Point(scrollViewer.ViewportWidth/2, scrollViewer.ViewportHeight/2);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, grid);
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
        	//canvas.CaptureMouse();
        	canvas.Focus();
        	//canvas.ReleaseMouseCapture();

            e.Handled = true;
        }
	}
}
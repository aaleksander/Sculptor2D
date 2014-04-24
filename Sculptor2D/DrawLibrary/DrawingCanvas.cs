﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 04/16/2014
 * Время: 13:43
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Commands;
using DrawLibrary.Brushes;
using DrawLibrary.Graphics;
using DrawLibrary.Tools;

//FUTURE: Блокирование некоторых вершин (участка), относительно друг-друга. Например, вырезы под сборку
//FUTURE: Слои и интерфейс для них

//TODO: 000 сделать зумминг и скроллинг с помощью мыши
//TODO: сделать изображение кисти (кружок под мышкой)
//FUTURE: загрузка изображений-подложек
namespace DrawLibrary
{
    /// <summary>
    /// событие об изменении статуса хоста
    /// </summary>
    public class CanvasEventArgs
    {
        public CanvasEventArgs(string s) 
        {
    		Text = s; 
    	}
        public String Text {get; private set;}
    }

	/// <summary>
	/// Холст для рисования
	/// </summary>
	public class DrawingCanvas: Canvas, INotifyPropertyChanged
	{
        // Collection contains instances of GraphicsBase-derived classes.
        private VisualCollection _graphicsList;

        //private DrawingVisual _cursor;

		public DrawingCanvas(): base()
		{
			_graphicsList = new VisualCollection(this);

//			_cursor = new DrawingVisual();
//			_cursor.Transform = new TranslateTransform(0, 0);			
//			UpdateCursor();
//			GraphicsList.Add(_cursor);			

            // создадим список инструментов
            _tools = new ToolBase[(int)ToolType.Max];

            _tools[(int)ToolType.Pointer] = new ToolPointer();
            _tools[(int)ToolType.Editor] = new ToolEditor();
            _tools[(int)ToolType.Line] = new ToolLine();
            _tools[(int)ToolType.Polygone] = new ToolPolygone();
            _tools[(int)ToolType.Brush] = new ToolBrush();

            //toolText = new ToolText(this);
            //tools[(int)ToolType.Text] = toolText;   // kept as class member for in-place editing			
            
            
            _brushes = new BrushBase[(int) BrushType.Max];
            //_brushes[(int) BrushType.OutMover] = new BrushOutMover();
            //_brushes[(int) BrushType.Smoother] = new BrushSmoother();
            
            
            //события мыши
            this.FocusVisualStyle = null;
            this.Focus();

            this.Loaded += new RoutedEventHandler(DrawingCanvas_Loaded);
            this.MouseDown += new MouseButtonEventHandler(DrawingCanvas_MouseDown);
            this.MouseMove += new MouseEventHandler(DrawingCanvas_MouseMove);
            this.MouseUp += new MouseButtonEventHandler(DrawingCanvas_MouseUp);
            //this.KeyDown += new KeyEventHandler(DrawingCanvas_KeyDown);
            this.LostMouseCapture += new MouseEventHandler(DrawingCanvas_LostMouseCapture);
		}
		
		private void UpdateCursor()
		{
			//_graphicsList.Remove(_cursor);
			
//			_cursor = new DrawingVisual();
//            DrawingContext dc = _cursor.RenderOpen();
//            dc.DrawEllipse(null, new Pen(new SolidColorBrush(Colors.Black), 1), new Point(0, 0), 10, 10);
//            dc.Close();            
            
            //_graphicsList.Add(_cursor);
		}
		
		public event PropertyChangedEventHandler PropertyChanged;

		public void onPropertyChanged(string aProp){			
			_onPropertyChanged(new PropertyChangedEventArgs(aProp));						
		}		

		protected void _onPropertyChanged(PropertyChangedEventArgs e)
		{
			if( PropertyChanged != null )
				PropertyChanged(this, e);
		}		

		#region события хоста
		public delegate void CanvasEventHandler(object sender, CanvasEventArgs e);
		public event CanvasEventHandler CanvasEvent;

		protected virtual void SendEvent(String aText)
        {
            if (CanvasEvent != null)
                CanvasEvent(this, new CanvasEventArgs(aText));
        }		
		#endregion

        internal int Count
        {
            get
            {
                return _graphicsList.Count;
            }
        }		
        
        internal VisualCollection GraphicsList
        {
            get
            {
                return _graphicsList;
            }
        }       


        internal GraphicsBase this[int index]
        {
            get
            {
            	if ( index >= 0  &&  index < Count && _graphicsList[index] is GraphicsBase)
                {
                    return (GraphicsBase)_graphicsList[index];
                }

                return null;
            }
        }
		
        void DrawingCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focusable = true;      // to handle keyboard messages
            Focus();
        }		

        void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_tools[(int)Tool] == null)
            {//не выбран ни один инструмент
                return;
            }

            this.Focus();

            //if ( e.ChangedButton == MouseButton.Left )
            //{//нажали на левую кнопку
			_tools[(int)Tool].OnMouseDown(this, e);
			
			onPropertyChanged("SelectedObject");
                //UpdateState();
            //}
            //else if (e.ChangedButton == MouseButton.Right)
            //{
                //ShowContextMenu(e);
            //}
        }        
		
        void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_tools[(int)Tool] == null)
            {
                return;
            }

            //if (e.ChangedButton == MouseButton.Left)
            //{
                _tools[(int)Tool].OnMouseUp(this, e);

                //UpdateState();
            //}
        }
        
        void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
        	Point p = e.GetPosition(this);
        	
//        	((TranslateTransform)_cursor.Transform).X = p.X;
//        	((TranslateTransform)_cursor.Transform).Y = p.Y;
        	UpdateCursor();

            if (_tools[(int)Tool] == null)
            {
                return;
            }

            if ( e.MiddleButton == MouseButtonState.Released  &&  e.RightButton == MouseButtonState.Released )
            {
                _tools[(int)Tool].OnMouseMove(this, e);

                //UpdateState();
            }
            else
            {
                //this.Cursor = HelperFunctions.DefaultCursor;
            }
        }        
		
        /// <summary>
        /// Mouse capture is lost
        /// </summary>
        void DrawingCanvas_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if ( this.IsMouseCaptured )
            {
            	
                //CancelCurrentOperation();
                //UpdateState();
            }
        }
        
        
       static DrawingCanvas()
        {
            // инициализация свойств
            PropertyMetadata metaData;

            // Tool
            metaData = new PropertyMetadata(ToolType.Pointer);

            ToolProperty = DependencyProperty.Register(
                "Tool", typeof(ToolType), typeof(DrawingCanvas),
                metaData);
            
            
            // Brush
            /*metaData = new PropertyMetadata(BrushType.None);

            BrushProperty = DependencyProperty.Register(
                "Brush", typeof(BrushType), typeof(DrawingCanvas),
                metaData);     */       
        }

        public static readonly DependencyProperty ToolProperty;
        public static readonly DependencyProperty BrushProperty;

        private ToolBase[] _tools;                   // список инструментов
        private BrushBase[] _brushes;

        #region Tool
        /// <summary>
        /// активный на данный момент инструмент
        /// </summary>
        public ToolType Tool
        {
            get
            {
                return (ToolType)GetValue(ToolProperty);
            }
            set
            {
                if ((int)value >= 0 && (int)value < (int)ToolType.Max)
                {
                    SetValue(ToolProperty, value);                    
                    SendEvent("Я включил " + Tool.ToString());
                    //TODO:включаем нужный курсор
                    //tools[(int)Tool].SetCursor(this);
                }
            }
        }

        #endregion Tool
        
        public BrushBase Brush
        {
            get
            {
            	if( Tool != ToolType.Brush )
            		return null;
            	
            	return ((ToolBrush)_tools[(int)Tool]).Brush;
            }

        }


        #region Visual Children Overrides

        /// <summary>
        /// Get number of children: VisualCollection count.
        /// If in-place editing textbox is active, add 1.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get 
            { 
                int n = _graphicsList.Count; 

//                if ( toolText.TextBox != null )
  //              {
  //                  n++;
  //              }

                return n;
            }
        }

        /// <summary>
        /// Get visual child - one of GraphicsBase objects
        /// or in-place editing textbox, if it is active.
        /// </summary>
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _graphicsList.Count )
            {
//                if (toolText.TextBox != null && index == graphicsList.Count )
//                {
//                    return toolText.TextBox;
//                }

                throw new ArgumentOutOfRangeException("index");
            }

            return _graphicsList[index];
        }
        #endregion Visual Children Overrides        


        public void UnselectAll()
        {
        	foreach(var g in _graphicsList)
        	{
        		if( g is GraphicsBase )
        			((GraphicsBase)g).IsSelected = false;
        	}

        	//FIXME: удалить все сервисные объекты
        }

        public GraphicsBase SelectedObject
        {//FIXME: Что делать, если объектов несколько?
        	get{
        		foreach(var o in _graphicsList)
        		{
        			if( o is GraphicsBase )
        			{
        				var  oo = (GraphicsBase) o;
        				if( oo.IsSelected )
        					return oo;
        			}
        		}
        		return null;
        	}
        }
        
        /// <summary>
        /// выеделить какой-то объект
        /// </summary>
        /// <param name="aObj"></param>
        public void SelectObject(GraphicsBase aObj, GraphicsMode aMode)
        {
        	if( aMode == GraphicsMode.Selected )
        	{
        		aObj.IsSelected = true;
        	}
        }

        #region Команда "превратить в глину"
		private DelegateCommand<GraphicsMultiPoint> toClayCommand;
		public ICommand ToClayCommand
		{
            get
            {
                if (toClayCommand == null)
                {
                    toClayCommand = new DelegateCommand<GraphicsMultiPoint>(ToClay, CanToClay);
                }
                return toClayCommand;
            }
		}

		private void ToClay(GraphicsMultiPoint aObj)
		{	
			int index = _graphicsList.IndexOf(SelectedObject);

			GraphicsList.Insert(index, new GraphicsClay((GraphicsMultiPoint)SelectedObject));
			
			_graphicsList.RemoveAt(index + 1);
			
		}

		private bool CanToClay(GraphicsMultiPoint a)
		{
			return SelectedObject != null;
			//FIXME: добавить проверку на соответствие типов && Type(SelectedObject) is GraphicsMultiPoint;
		}
		#endregion
		
		
		
        #region Команды переключения кистей
		private DelegateCommand<String> setBrushCommand;
		public ICommand SetBrushCommand
		{
            get
            {
                if (setBrushCommand == null)
                {
                    setBrushCommand = new DelegateCommand<String>(SetBrush, CanSetBrush);
                }
                return setBrushCommand;
            }
		}

		private void SetBrush(String aBrushName)
		{	
			var t = (BrushType)Enum.Parse(typeof(BrushType), aBrushName);
            if ((int)t >= 0 && (int)t < (int)BrushType.Max)
            {
            	((ToolBrush)_tools[(int)Tool]).Brush = GetBrushFromCache(t);
                SendEvent("Я включил " + Brush.ToString());
                //TODO:включаем нужный курсор
                //tools[(int)Tool].SetCursor(this);
             }
            
             onPropertyChanged("Brush");
		}
		
		private bool CanSetBrush(String a)
		{
			if( Tool != ToolType.Brush )
				return false;
			
			return true;
		}

		/// <summary>
		/// возвращает индекс кисти из кэша, либо создает новый, помещает в кэш и все равно возвращает индекс
		/// </summary>
		/// <param name="aType"></param>
		/// <returns></returns>
		private BrushBase GetBrushFromCache(BrushType aType)
		{
			BrushBase res = null;
			//ищем в кэше
			try
			{
				res = _cacheBrush.First<BrushBase>(x => x.Type == aType);
			}
			catch(InvalidOperationException)
			{//не нашли, создаем
				switch(aType)
				{
					case BrushType.OutMover: 	res = new BrushOutMover(); break;
					case BrushType.Smoother: 	res = new BrushSmoother(); break;
					case BrushType.Mover: 		res = new BrushMover(); break;
				}
				_cacheBrush.Add(res);
			}
			return res;
		}

		private Collection<BrushBase> _cacheBrush = new Collection<BrushBase>();
		#endregion		
		
		#region команда "копировать в svg"
		private DelegateCommand toSVGCommand;
		public ICommand ToSVGCommand
		{
            get
            {
                if (toSVGCommand == null)
                {
                    toSVGCommand = new DelegateCommand(ToSVG, CanToSVG);
                }
                return toSVGCommand;
            }
		}

		private void ToSVG()
		{	
			GraphicsMultiPoint o = (GraphicsMultiPoint)SelectedObject;

			string text = o.ToSVG();
			
			System.Windows.Clipboard.SetText(text);
			
			MessageBox.Show("Данные скопированны в буфер обмена");
		}

		private bool CanToSVG()
		{
			if( SelectedObject == null ) //нет выделенных объектов
				return false;
			return SelectedObject is GraphicsMultiPoint;
		}		
		#endregion
	}
}

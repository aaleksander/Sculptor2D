/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 04/16/2014
 * Время: 13:43
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using Commands;
using DrawLibrary.Brushes;
using DrawLibrary.Graphics;
using DrawLibrary.Tools;
using DrawLibrary.Undo;
using DrawToolsLib;

//FUTURE: Блокирование некоторых вершин (участка), относительно друг-друга. Например, вырезы под сборку
//FUTURE: Слои и интерфейс для них

//TODO:000 скроллинг с помощью мыши
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

			_undoManager = new UndoManager(this);
			
            // создадим список инструментов
            _tools = new ToolBase[(int)ToolType.Max];

            _tools[(int)ToolType.Pointer] = new ToolPointer();
            _tools[(int)ToolType.Editor] = new ToolPointEditor();
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
            
            //мышка
            this.MouseDown += new MouseButtonEventHandler(DrawingCanvas_MouseDown);
            this.MouseMove += new MouseEventHandler(DrawingCanvas_MouseMove);
            this.MouseUp += new MouseButtonEventHandler(DrawingCanvas_MouseUp);

            //this.LostMouseCapture += new MouseEventHandler(DrawingCanvas_LostMouseCapture);
            
            this.Focus();
//            this.OnStylusDown += new StylusDownEventHandler(StylusDown);
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

        public VisualCollection GraphicsList
        {
            get
            {
                return _graphicsList;
            }
        }

        /// <summary>
        /// заменяет объект по индексу на новый
        /// </summary>
        /// <param name="aIndex"></param>
        /// <param name="aObj"></param>
        public void ReplaceObject(int aIndex, Visual aObj)
        {
        	GraphicsList.RemoveAt(aIndex);
        	InvalidateVisual();
        	GraphicsList.Insert(aIndex, aObj);
        	InvalidateVisual();
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

       
        #region опустить мышку/перо

        void Down(Point aP)
        {
            if (_tools[(int)Tool] == null)
            {//не выбран ни один инструмент
                return;
            }

            this.Focus();

            //if ( e.ChangedButton == MouseButton.Left )
            //{//нажали на левую кнопку
			_tools[(int)Tool].OnDown(this, aP);

			onPropertyChanged("SelectedObject");
                //UpdateState();
            //}
            //else if (e.ChangedButton == MouseButton.Right)
            //{
                //ShowContextMenu(e);
            //}
        }
        
        void OnStylusDown(object sender, StylusDownEventArgs e)
        {        	
        	Down(e.GetPosition(this));
        }

        void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
        	Down(e.GetPosition(this));        	     
        }        
		
        
        #endregion опустить мышку/перо
        
        
        #region поднять мышку/перо
        
        void Up(Point aPoint)
        {
            if (_tools[(int)Tool] == null)
            {
                return;
            }

            //if (e.ChangedButton == MouseButton.Left)
            //{
                _tools[(int)Tool].OnUp(this, aPoint);

                //UpdateState();
            //}
        }

        protected override void OnStylusUp(StylusEventArgs e)
        {
        	Up(e.GetPosition(this));
        }
        
        void DrawingCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
        	Up(e.GetPosition(this));
        }
        
        #endregion отпустили мышку/перо
        
        

		#region двигаем мышку/перо        
        void Move(Point aPoint, bool aPressed)
        {
            if (_tools[(int)Tool] == null)
            {
                return;
            }
            
            //если инструмент подразумевает продвинутую кисть, то надо ее перерисовать под мышкой
            if( _cursor != null )
            {
            	_cursor.Transform = new TranslateTransform(aPoint.X, aPoint.Y);
            }
			_tools[(int)Tool].OnMove(this, aPoint, aPressed);
        }

        protected override void OnStylusInAirMove(StylusEventArgs e)
        {
        	Move(e.GetPosition(this), false); //это событие вызывается, когда перо "летает" над планшетом.
        }
        
        protected override void OnStylusMove(StylusEventArgs e)
        {
        	Move(e.GetPosition(this), true); //это событие вызывается, когда перо касается планшета
        }
        
        void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
        	Move(e.GetPosition(this), e.LeftButton == MouseButtonState.Pressed);
        	e.Handled = true;
        }

		#endregion двигаем мышку/перо        
		
		
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
                    _tools[(int)Tool].SetCursor(this);
                    _tools[(int)Tool].Init(this);
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
        	//foreach(var g in _graphicsList)
        	GraphicsBase g;
        	for(int i=0; i<_graphicsList.Count; i++)
        	{
        		g = (GraphicsBase)_graphicsList[i];
        		if( g is GraphicsBase && !(g is GraphicsService) )
        			((GraphicsBase)g).IsSelected = false;
        	}

        	RemoveService(null);
        }

        public GraphicsBase SelectedObject
        {//FIXME: Что делать, если объектов несколько? Они понадобятся, когда будут булевые операции
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
       		aObj.IsSelected = aMode == GraphicsMode.Selected || aMode == GraphicsMode.Points;
       		aObj.Mode = aMode;
       		aObj.RefreshDrawing();
        }

        #region Команда "превратить в глину"
		private DelegateCommand toClayCommand;
		public ICommand ToClayCommand
		{
            get
            {
                if (toClayCommand == null)
                {
                    toClayCommand = new DelegateCommand(ToClay, CanToClay);
                }
                return toClayCommand;
            }
		}

		private void ToClay()
		{	
			int index = _graphicsList.IndexOf(SelectedObject);

			RemoveService(SelectedObject);
			
			GraphicsClay newO = new GraphicsClay(this, (GraphicsMultiPoint)SelectedObject);
			AddActionToHistory(new ActionToClay(SelectedObject, newO.Id));
			
			ReplaceObject(index, newO);
		}

		private bool CanToClay()
		{
			return SelectedObject != null && SelectedObject is GraphicsMultiPoint && !(SelectedObject is GraphicsClay);
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
            	var tmp = GetBrushFromCache(t);

            	((ToolBrush)_tools[(int)Tool]).Brush = tmp;

            	tmp.BrushEvent += new BrushBase.BrushEventHandler(BrushChanged);

                SendEvent("Я включил " + Brush.ToString());

                _tools[(int)Tool].SetCursor(this);

                //SetCursor(DrawingCursorType.Brush);
             }
            
             onPropertyChanged("Brush");
		}
		
		void BrushChanged(object sender, BrushEventArgs e)
		{
			var t = (GraphicsCursor)GetObjectBy(x => x is GraphicsCursor);
			if( t == null )
				return;
			
			t.Size = e.Size;
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
					case BrushType.Pincher: 	res = new BrushPincher(); break;
				}
				_cacheBrush.Add(res);
			}
			res.ClearPath();
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
		#endregion копировать в svg

		#region Команда "Сохранить"
		private DelegateCommand saveCommand;
		public ICommand SaveCommand
		{
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new DelegateCommand(Save);
                }
                return saveCommand;
            }
		}

		private void Save()
		{	
			foreach(var o in _graphicsList)
			{
				
			}
			
		}		
		#endregion

		#region кнопка Escape
		private DelegateCommand escapeCommand;
		public ICommand EscapeCommand
		{
            get
            {
                if (escapeCommand == null)
                {
                    escapeCommand = new DelegateCommand(Escape);
                }
                return escapeCommand;
            }
		}

		private void Escape()
		{	
            if (_tools[(int)Tool] == null)
            {
                return;
            }

     
            _tools[(int)Tool].KeyDown(this, Key.Escape);
		}	
		#endregion кнопка escape

		#region Undo
		UndoManager _undoManager;
        public void AddActionToHistory(ActionBase command)
        {
            _undoManager.AddActionToHistory(command);
        }		
		
		private DelegateCommand undoCommand;
		public ICommand UndoCommand
		{
            get
            {
                if (undoCommand == null)
                {
                    undoCommand = new DelegateCommand(Undo, CanUndo);
                }
                return undoCommand;
            }
		}

		private void Undo()
		{	
			_undoManager.Undo();
		//	canvas.GraphicsList.Add(p);		

//			ReplaceObject(0, p);
		}
		
		private bool CanUndo()
		{
			return _undoManager.CanUndo;
		}
		#endregion Undo

		/// <summary>
		/// возвращает коллекцию объектов, которые потенциально могу измениться.
		/// </summary>
		/// <returns></returns>
		public Collection<GraphicsBase> GetPotentObjects()
		{
			//FIXME: учитывать активный слой и свойства (заморозка, например), объектов
			Collection<GraphicsBase> res = new Collection<GraphicsBase>();
			foreach(var o in GraphicsList)
			{
				if( o is GraphicsMultiPoint )
					res.Add( ((GraphicsMultiPoint)o).Clone());
			}
			return res;
		}

        public GraphicsBase GetObjectBy(Func<GraphicsBase, bool> aPredicate)
        {
        	foreach(var o in _graphicsList)
        	{
        		if( aPredicate((GraphicsBase)o) )
        			return (GraphicsBase)o;
        	}
        	
        	return null;
        }
        
        public int GetIndexBy(Func<GraphicsBase, bool> aPredicate)
        {        	
        	Visual o = null;
        	for( int i=0; i< _graphicsList.Count; i++)//var o in _graphicsList)
        	{
        		o = _graphicsList[i];
        		if( aPredicate((GraphicsBase)o) )
        			return i;
        	}
        	
        	return -1;
        }
		
        
        #region работа с курсором
        private GraphicsCursor _cursor = null;
        public GraphicsCursor SetCursor(DrawingCursorType aType)
        {        	
        	if( _cursor != null)
        	{
        		_graphicsList.Remove(_cursor);
        	}

        	_cursor = null;
        	//создаем занового, нужного типа
        	switch(aType)
        	{
        		case DrawingCursorType.Brush:
        			_cursor = new GraphicsCursor(this, aType);        			
        			break;
        	}
        	
        	if( _cursor != null )
        	{
        		var p = Mouse.GetPosition(this);
        		_cursor.Transform = new TranslateTransform(p.X, p.Y);
        		_graphicsList.Add(_cursor);
        	}
        	
        	return _cursor;
        }
        #endregion работа с курсором

        #region Работа с сервисными объектами

        /// <summary>
        /// перемещает сервисные объекты наверх
        /// </summary>
        /// <param name="aObject"></param>
        public void UpdateServiceObjects()
        {
            List<GraphicsBase> list = new List<GraphicsBase>();

            for(int i = Count - 1; i >= 0; i--)
            {
                if ( GraphicsList[i] is GraphicsService )
                {
                	list.Insert(0, (GraphicsBase)GraphicsList[i]);
                    GraphicsList.RemoveAt(i);
                }
            }

            // Add all items from temporary list to the end of GraphicsList
            foreach(GraphicsBase g in list)
            {
                GraphicsList.Add(g);
            }

        }
        
        /// <summary>
        /// удалить все сервисные объекты определенного объекта
        /// </summary>
        public void RemoveService(GraphicsBase aOwner)
        {             	
        	for(int i=GraphicsList.Count - 1; i >= 0; i--)
        	{
        		if( GraphicsList[i] is GraphicsService )
        		{
        			if( aOwner != null )
        				if( ((GraphicsService)GraphicsList[i]).IsYourOwner(aOwner)) //это его хозяин
        					GraphicsList.RemoveAt(i);
        		}
        			
        	}
        }
        #endregion        
	}
}

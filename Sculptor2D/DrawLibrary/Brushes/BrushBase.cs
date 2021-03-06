﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.04.2014
 * Время: 17:49
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using DrawLibrary.Graphics;

//TODO: примитивные объекты (эллипс, прямоугольник)
//FUTURE: кисть "по границе" - пригодится, если надо раздвинуть на определенную дистанцию
//FUTURE: Кисть "по линейке". Если надо выровнять строго в линию. (хотя это можно сделать клиппингом)
//FUTURE: Кисть "эскиз". Запоминает всю траекторию мазка и старается приблизить к нему окружающие контуры. 
//	Если зажать Контрол, то будут создаваться новые полилинии и контуры (автоматом Clay)

//FUTURE: панель свойств объекта
//FUTURE: модификаторы объекта (для начала - симметрия).
//FUTURE: свойство "не пересекается".

//FUTURE: маскирование части объекта (Заморозка части вершин)

namespace DrawLibrary.Brushes
{
	public enum BrushType{
		None,
		OutMover,  	//отодвигает вершины от себя
		Smoother,	//сглаживает вершины
		Mover,		//перемещает вершины	
		Pincher,	//"отщипывает" вершины
		Max
	}
	
	
    public class BrushEventArgs
    {
        public BrushEventArgs(double aSize, double aPower) 
        {
    		Size = aSize;
    		Power = aPower;
    	}
        
        public double Size {get; private set;}
        public double Power {get; private set;}
    }

	/// <summary>
	/// базовая кисть
	/// </summary>
	public class BrushBase
	{	
		public BrushBase()
		{
			_type = BrushType.None;
			Power = 10;
			Size = 50;
		}

		protected BrushType _type;
		public BrushType Type{
			get{
				return _type;	
			}
		}

		//это путь, который проделала кисть
		public List<Point> Path{
			get{
				return _path;
			}
		}		
		private List<Point> _path = new List<Point>();

		/// <summary>
		/// добавить в путь новую точку
		/// </summary>
		/// <param name="p"></param>
		public void AddPath(Point p)
		{
			_path.Add(p);
		}			

		public Point LastPoint{
			get{
				return _path[_path.Count - 1];
			}
		}

		public void ClearPath()
		{
			_path.Clear();
		}
		
		/// <summary>
		/// модифицирует объект
		/// </summary>
		/// <param name="aObj"></param>
		public virtual bool Modify(GraphicsClay aObj)
		{//возвращает истину, если кисть изменила объект (смогла дотянуться до его вершин и сдвинуть их)
			return false;
		}
		
		/// <summary>
		/// Сила кисти. Как сильно она влияет на точки
		/// </summary>
		public double Power
		{
			set{
				_power = value;
				SendEvent();
			}
			get{
				return _power;
			}
		}
		private Double _power;
		
		
		public double Size
		{
			set{
				_size = value;
				SendEvent();
			}
			get{
				return _size;
			}
		}
		private Double _size;	


		#region события кисти
		public delegate void BrushEventHandler(object sender, BrushEventArgs e);
		public event BrushEventHandler BrushEvent;

		protected virtual void SendEvent()
        {
            if (BrushEvent != null)
                BrushEvent(this, new BrushEventArgs(_size, _power));
        }		
		#endregion события кисти

		
	}
}

/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 15.05.2014
 * Время: 11:24
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using DrawLibrary.Graphics;
using DrawLibrary.Serialize;

//TODO: добавить полупрозрачность

namespace DrawLibrary.Misc
{
	/// <summary>
	/// Реализация слоя
	/// </summary>
	public class Layer: GraphicsBase
	{
		private int _id;
		public Layer()
		{
			_id = GetHashCode();		
			IsVisible = true;			
			Alpha = 255;
		}

		public Layer(String aName)
		{
			_id = GetHashCode();
			Name = aName;
			IsVisible = true;
			Alpha = 255;
		}
		
		public Layer(SerializeLayer aObj): base(aObj)
		{
			IsVisible = aObj.IsVisible;
			
			foreach(SerializeBase o in aObj.List)
			{
				_list.Add(o.CreateGraphicsObject());
			}
			
			Name = aObj.Name;
			
			//Alpha = aObj.Alpha;
		}

		public Boolean IsVisible{
			set{
				_isVisible = value;

				foreach(var o in Objects)
				{
					o.RefreshDrawing();
				}

				onPropertyChanged("IsVisible");
			}
			get{
				return _isVisible;
			}
		}
		private  Boolean _isVisible;

		
		public byte Alpha{
			set{
				_alpha = value;
				//изменить прозрачность у объекта
				foreach(GraphicsBase o in Objects)
				{
					o.RefreshDrawing();
				}
				
				onPropertyChanged("Alpha");
			}
			get{
				return _alpha;
			}
		}
		private byte _alpha;		
		
		public String Name{set;get;}
		
		/// <summary>
		/// добавляем в слой новый объект
		/// </summary>
		/// <param name="aObj"></param>
		public void Add(GraphicsBase aObj)
		{
			_list.Add(aObj);
		}

		private Collection<GraphicsBase> _list = new Collection<GraphicsBase>();

		public Collection<GraphicsBase> Objects{
			get{
				return _list;
			}
		}

		public override DrawLibrary.Serialize.SerializeBase GetSerializebleObject()
		{
			return new SerializeLayer(this);
		}
	}
}

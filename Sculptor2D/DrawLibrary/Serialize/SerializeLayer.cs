/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 15.05.2014
 * Время: 11:16
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DrawLibrary.Graphics;
using DrawLibrary.Misc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DrawLibrary.Serialize
{
	/// <summary>
	/// Description of SerializeLayer.
	/// </summary>
	public class SerializeLayer: SerializeBase
	{
		public SerializeLayer()
		{}

		public SerializeLayer(Layer l)
		{
			Id = l.Id;
			Type = "Layer";
			IsVisible = l.IsVisible;
			Name = l.Name;

			List = new List<SerializeBase>();
			foreach(var o in l.Objects)
			{
				List.Add(o.GetSerializebleObject());
			}
		}

		[JsonProperty("Objects")]
		public List<SerializeBase> List{set;get;}

		[JsonProperty("IsVisible")]
		public bool IsVisible{set;get;}

		[JsonProperty("Name")]
		public String Name{set;get;}

		public SerializeLayer(JObject aObj): base(aObj)
		{
			Type = "Layer";
			IsVisible = bool.Parse(aObj["IsVisible"].ToString());

			//читаем объекты
			var  pp = aObj["Objects"];
			List = new List<SerializeBase>();
			foreach(var p in pp)
			{
				var tmp = Loader.Load((JObject)p);
				List.Add(tmp);
			}
			
			Name = aObj["Name"].ToString();
		}
		
		
		public override GraphicsBase CreateGraphicsObject()
		{
			return new Layer(this);
		}		
	}
}

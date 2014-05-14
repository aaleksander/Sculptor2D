/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 07.05.2014
 * Время: 10:01
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using DrawLibrary.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DrawLibrary.Serialize
{
	/// <summary>
	/// Description of Loader.
	/// </summary>
	public class Loader:IDisposable
	{
		public Loader(string aFileName)
		{
			_file = System.IO.File.ReadAllText(aFileName);

			_graphConstructors = new Dictionary<string, ConstructorDelegate>
			{
				{"Base", 	this._NewBase},
				{"Polygon", this._NewPolygon}
			};
		}		
		private String _file = null;

		private delegate GraphicsBase ConstructorDelegate(JObject aObj);
		private Dictionary<string, ConstructorDelegate> _graphConstructors;
		
		public GraphicsBase Load()
		{
			JObject l = (JObject)JsonConvert.DeserializeObject(_file);
			GraphicsBase res = null;
			var type = l["Type"].ToString();
			
			if( !_graphConstructors.ContainsKey(type) )
				throw new Exception("Неизвестный тип: " + type);
			
			res = _graphConstructors[type](l);

            res.Id = int.Parse(l["Id"].ToString());
			return res;
		}

		public void Dispose()
		{
			//_file.Dispose();
		}
		
		private GraphicsBase _NewBase(JObject aObj)
		{
			return (GraphicsPolygon) new SerializeBase(aObj).CreateGraphicsObject();
		}

		private GraphicsPolygon _NewPolygon(JObject aObj)
		{
			return (GraphicsPolygon) new SerializePolygon(aObj).CreateGraphicsObject();;
		}		
		
	}
}

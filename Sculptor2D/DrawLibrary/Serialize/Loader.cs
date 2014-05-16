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
using DrawLibrary.Misc;
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

/*			_graphConstructors = new Dictionary<string, ConstructorDelegate>
			{//Список функций-конструкторов для всяких типов
				{"Base",	Loader._NewBase},
				{"Polygon",	Loader._NewPolygon},
				{"Clay", 	Loader._NewClay},
				{"Layer", 	Loader._NewLayer}
			};*/
		}		
		private String _file = null;

		private delegate SerializeBase ConstructorDelegate(JObject aObj);
		private static Dictionary<string, ConstructorDelegate> _graphConstructors	= new Dictionary<string, ConstructorDelegate>
			{//Список функций-конструкторов для всяких типов
				{"Base",	Loader._NewBase},
				{"Polygon",	Loader._NewPolygon},
				{"Clay", 	Loader._NewClay},
				{"Layer", 	Loader._NewLayer}
			};

		public SerializeBase Load()
		{
			JObject l = (JObject)JsonConvert.DeserializeObject(_file);
			return Loader.Load(l);
		}

/*		public static GraphicsBase Load(JObject aObj)
		{
			GraphicsBase res = null;
			var type = aObj["Type"].ToString();

			if( !_graphConstructors.ContainsKey(type) )
				throw new Exception("Неизвестный тип: " + type);

			res = _graphConstructors[type](aObj).CreateGraphicsObject();

            res.Id = int.Parse(aObj["Id"].ToString());
			return res;
		}*/
		
		public static SerializeBase Load(JObject aObj)
		{
			SerializeBase res = null;
			var type = aObj["Type"].ToString();

			if( !_graphConstructors.ContainsKey(type) )
				throw new Exception("Неизвестный тип: " + type);

			res = _graphConstructors[type](aObj);

            res.Id = int.Parse(aObj["Id"].ToString());
			return res;
		}		

		public void Dispose()
		{
			//_file.Dispose();
		}

		private static SerializeBase _NewBase(JObject aObj)
		{
			return new SerializeBase(aObj);
		}

		private static SerializePolygon _NewPolygon(JObject aObj)
		{
			return new SerializePolygon(aObj);
		}

		private static SerializeClay _NewClay(JObject aObj)
		{
			return new SerializeClay(aObj);
		}

		private static SerializeLayer _NewLayer(JObject aObj)
		{
			return new SerializeLayer(aObj);
		}
	}
}

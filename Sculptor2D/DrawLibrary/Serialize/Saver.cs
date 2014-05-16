/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 07.05.2014
 * Время: 9:31
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.IO;
using System.IO.Compression;
using DrawLibrary.Graphics;
using Newtonsoft.Json;

namespace DrawLibrary.Serialize
{
	/// <summary>
	/// Description of Serializer.
	/// </summary>
	public class Saver:IDisposable
	{
		public Saver(String aFileName)
		{
			_file = new StreamWriter(aFileName);
		}
		private StreamWriter _file = null;

		public void Save(GraphicsBase aObj)
		{
			var sObj = aObj.GetSerializebleObject();
			_file.Write( JsonConvert.SerializeObject(sObj, Formatting.Indented) );
		}

		public void Dispose()
		{
			//TODO: надо бы потом запаковать в ZipStream
			_file.Dispose();
		}
	}
}

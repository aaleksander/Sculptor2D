/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 08.05.2014
 * Время: 13:42
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using DrawLibrary.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DrawLibrary.Serialize
{
	/// <summary>
	/// Description of SerializePolygon.
	/// </summary>
	[JsonObject]
	public class SerializePolygon: SerializeMultiPoint
	{
		public SerializePolygon(JObject aObj): base(aObj)
		{
			Type = "Polygon";
			_jObject = aObj;
		}
		private JObject _jObject;

		public SerializePolygon(GraphicsPolygon aObj): base(aObj)
		{
			Type = "Polygon";
		}
		
		public override GraphicsBase CreateGraphicsObject()
		{
			GraphicsPolygon res = new GraphicsPolygon();

			JPoints2Points(res);

			return res;
		}		
	}
}

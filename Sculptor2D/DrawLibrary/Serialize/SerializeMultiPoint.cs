/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 08.05.2014
 * Время: 13:13
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using DrawLibrary.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DrawLibrary.Serialize
{
	/// <summary>
	/// Description of SerializeMultiPoint.
	/// </summary>
	[JsonObject]
	public class SerializeMultiPoint: SerializeBase
	{
		[JsonProperty("Points")]
		public Collection<Point> Points{set;get;}

		//public SerializeMultiPoint(){}
		public SerializeMultiPoint(GraphicsMultiPoint aObj): base(aObj)
		{
			Type = "MultiPoint";
			
			Points = new Collection<Point>();
			foreach(var p in aObj.Points)
			{
				Points.Add(new Point(p.X, p.Y));
			}
		}

		public SerializeMultiPoint(JObject aObj): base(aObj)
		{
			Type = "MultiPoint";			
		}		

		private void CopyPoints(GraphicsMultiPoint res, JObject aObj)
		{
			var l = aObj["Points"];
			
			foreach(JValue p in l)
			{
				Point pp = JsonConvert.DeserializeObject<Point>(p.Value.ToString());
				//res.Points.Add(pp);
			}
		}

		/// <summary>
		/// копирует точки из _jObject в aRes.Points
		/// </summary>
		/// <param name="aRes"></param>
		protected void JPoints2Points(GraphicsMultiPoint aRes)
		{
			aRes.Points.Add(new Point(10, 1));
		}
		
	}
}

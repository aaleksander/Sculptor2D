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
using System.Windows.Media;
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

		[JsonProperty("IsClosed")]
		public bool IsClosed{set;get;}

		public Brush brush;
		
		public SerializeMultiPoint(GraphicsMultiPoint aObj):base(aObj)
		{
			Type = "MultiPoint";
			brush = aObj.Brush;
			
			Points = new Collection<Point>();
			foreach(var p in aObj.Points)
			{
				Points.Add(new Point(p.X, p.Y));
			}

			IsClosed = aObj.IsClosed;
		}

		public SerializeMultiPoint(JObject aObj): base(aObj)
		{
			
			Type = "MultiPoint";
			
			var  pp = aObj["Points"];
			Points = new Collection<Point>();
			Point tmp;
			foreach(var p in pp)
			{
				tmp = Point.Parse(p.ToString());
				Points.Add(tmp);
			}		

			IsClosed = bool.Parse(aObj["IsClosed"].ToString());
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
		
		/// <summary>
		/// возвращает графический объект, полностью восстановленный
		/// </summary>
		public override GraphicsBase CreateGraphicsObject()
		{
			return new GraphicsMultiPoint(this);
		}
		
	}
}

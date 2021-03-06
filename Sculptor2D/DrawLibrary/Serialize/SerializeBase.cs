﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 06.05.2014
 * Время: 17:54
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
	/// Description of SerializeBase.
	/// </summary>
	[JsonObject]
	public class SerializeBase
	{
		[JsonProperty("Id")]
		public int Id{set;get;}

		[JsonProperty("Type")]
		public String Type{set;get;}

		//это не пойдет в файл, но нужно для реализации undo/redo
		public bool isSelected; 
		public GraphicsMode mode;
		public DrawingCanvas ownerCanvas;

		public SerializeBase(){}

		/// <summary>
		/// инициализация из другого объекта (для сохранения)
		/// </summary>
		/// <param name="aObj"></param>
		public SerializeBase(GraphicsBase aObj)
		{
			Id = aObj.Id;
			Type = "Base";
			
			isSelected = aObj.IsSelected;
			mode =aObj.Mode;
			ownerCanvas = aObj.OwnerCanvas;
		}

		/// <summary>
		/// инициализация из JObject (для чтения)
		/// </summary>
		/// <param name="aJSONStr"></param>
		public SerializeBase(JObject aJObject)
		{

		}

		/// <summary>
		/// возвращает графический объект, полностью восстановленный
		/// </summary>
		public virtual GraphicsBase CreateGraphicsObject()
		{
			GraphicsBase res = new GraphicsBase(this);
			return res;
		}
	}
}

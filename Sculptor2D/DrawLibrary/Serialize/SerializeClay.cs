/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 14.05.2014
 * Время: 13:11
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using DrawLibrary.Graphics;

namespace DrawLibrary.Serialize
{
	/// <summary>
	/// Description of SerializeClay.
	/// </summary>
	public class SerializeClay: SerializeMultiPoint
	{
		public SerializeClay(GraphicsMultiPoint aObj):base(aObj)
		{
			Type = "Clay";
		}
		
		public override GraphicsBase CreateGraphicsObject()
		{
			return new GraphicsClay(null, this);
		}
	}
}

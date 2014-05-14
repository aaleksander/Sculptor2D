/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 07.05.2014
 * Время: 9:27
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows;
using DrawLibrary.Graphics;
using DrawLibrary.Serialize;
using NUnit.Framework;

namespace Tests
{
	/// <summary>
	/// Тестируем чтение и запись в файл
	/// </summary>
	[TestFixture]
	public class Test_02_SaveLoad
	{
		[Test]
		public void Test_01_Polygon()
		{
			GraphicsPolygon p = new GraphicsPolygon();
			p.Points.Add(new Point(0, 0));
			p.Points.Add(new Point(10, 20));
			p.Points.Add(new Point(20, 100));

			SerializePolygon sp = (SerializePolygon)p.GetSerializebleObject();
			Assert.IsTrue(sp.IsClosed);
			Assert.AreEqual(sp.Id, p.Id);

			GraphicsPolygon p2 = new GraphicsPolygon(null, sp);

			Assert.IsInstanceOf<GraphicsPolygon>(p2, "прочитал не тот тип");
			Assert.IsNotEmpty(p2.Points, "Точки не прочитались");
			Assert.AreEqual(p2.Points[0].X, 0);
			Assert.AreEqual(p2.Points[0].Y, 0);
			Assert.AreEqual(p2.Points[1].X, 10);
			Assert.AreEqual(p2.Points[1].Y, 20);
			Assert.AreEqual(p2.Points[2].X, 20);
			Assert.AreEqual(p2.Points[2].Y, 100);

		}
	}
}

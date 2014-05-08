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

			//GraphicsClay cl = new GraphicsClay(null, p);
			using (	var s = new Saver("test.scp") )
			{
				s.Save(p);
			}

			using( var l = new Loader("test.scp") )
			{
				GraphicsPolygon pp = (GraphicsPolygon)l.Load();
				Assert.IsInstanceOf<GraphicsPolygon>(pp, "прочитал не тот тип");
				Assert.IsNotEmpty(pp.Points, "Точки не прочитались");
				Assert.AreEqual(pp.Points[0].X, 0);
				Assert.AreEqual(pp.Points[0].Y, 0);
				Assert.AreEqual(pp.Points[1].X, 10);
				Assert.AreEqual(pp.Points[1].Y, 20);
				Assert.AreEqual(pp.Points[2].X, 20);
				Assert.AreEqual(pp.Points[2].Y, 100);
			}
		}
	}
}

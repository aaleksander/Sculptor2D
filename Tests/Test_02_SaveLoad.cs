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
using DrawLibrary;
using DrawLibrary.Graphics;
using DrawLibrary.Misc;
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
		public void Test_01_Polygon_Clone()
		{
			GraphicsPolygon p = new GraphicsPolygon();
			p.Points.Add(new Point(0, 0));
			p.Points.Add(new Point(10, 20));
			p.Points.Add(new Point(20, 100));

			SerializePolygon sp = (SerializePolygon)p.GetSerializebleObject();
			Assert.IsTrue(sp.IsClosed);
			Assert.AreEqual(sp.Id, p.Id);

			GraphicsPolygon p2 = new GraphicsPolygon(sp);

			Assert.IsInstanceOf<GraphicsPolygon>(p2, "прочитал не тот тип");
			Assert.IsNotEmpty(p2.Points, "Точки не прочитались");
			Assert.AreEqual(p2.Points[0].X, 0);
			Assert.AreEqual(p2.Points[0].Y, 0);
			Assert.AreEqual(p2.Points[1].X, 10);
			Assert.AreEqual(p2.Points[1].Y, 20);
			Assert.AreEqual(p2.Points[2].X, 20);
			Assert.AreEqual(p2.Points[2].Y, 100);
		}

		[Test]
		public void Test_02_Polygon_File()
		{
			GraphicsPolygon p = new GraphicsPolygon();
			p.Points.Add(new Point(0, 0));
			p.Points.Add(new Point(10, 20));
			p.Points.Add(new Point(20, 100));

			using( var s = new Saver("test.scp") )
			{
				s.Save(p);
			}

			GraphicsBase pp;
			using( var l = new Loader("test.scp"))
			{
				pp = l.Load().CreateGraphicsObject();
			}

			GraphicsPolygon p2 = (GraphicsPolygon)pp;
			Assert.IsInstanceOf<GraphicsPolygon>(p2, "прочитал не тот тип");
			Assert.IsNotEmpty(p2.Points, "Точки не прочитались");
			Assert.AreEqual(p2.Points[0].X, 0);
			Assert.AreEqual(p2.Points[0].Y, 0);
			Assert.AreEqual(p2.Points[1].X, 10);
			Assert.AreEqual(p2.Points[1].Y, 20);
			Assert.AreEqual(p2.Points[2].X, 20);
			Assert.AreEqual(p2.Points[2].Y, 100);
		}

		[Test]
		public void Test_03_Layer()
		{
			GraphicsPolygon p1 = new GraphicsPolygon();
			p1.Points.Add(new Point(0, 0));
			p1.Points.Add(new Point(10, 20));
			p1.Points.Add(new Point(20, 100));

			GraphicsPolygon p2 = new GraphicsPolygon();
			p2.Points.Add(new Point(0, 0));
			p2.Points.Add(new Point(10, 20));
			p2.Points.Add(new Point(20, 100));

			GraphicsClay p3 = new GraphicsClay(null, p2);
			DrawingCanvas canvas = new DrawingCanvas();
			canvas.GraphicsList.Add(p1);
			canvas.GraphicsList.Add(p2);
			canvas.GraphicsList.Add(p3);

			Layer l = new Layer("name1Имя");
			l.Add(p1);
			l.Add(p2);
			l.Add(p3);
			l.IsVisible = true;

			using( var s = new Saver("test.scp") )
			{
				s.Save(l);
			}

			SerializeLayer pp;
			using( var ll = new Loader("test.scp"))
			{
				pp = (SerializeLayer)ll.Load();//.CreateGraphicsObject();
			}

			Assert.IsTrue(pp.IsVisible, "Видимость слоя не прочиталась");
			Assert.IsNotEmpty(pp.List, "Объекты не прочитались");

			Layer layer = (Layer)pp.CreateGraphicsObject();
			
			Assert.AreEqual(layer.Name, "name1Имя", "Имя не восстановилось");
			
			
			Assert.IsTrue(layer.IsVisible);
			Assert.IsNotEmpty(layer.Objects, "Объекты не восстановились");
			
			Assert.IsInstanceOf<GraphicsPolygon>(layer.Objects[0], "первый элементо не того типа");
			Assert.IsInstanceOf<GraphicsClay>(layer.Objects[2], "третий элементо не того типа");
		}

		[Test]
		public void Test_04_Root()
		{//тестируем сохранение прямо из канваса
			DrawingCanvas canvas = new DrawingCanvas();

			//canvas.SaveAs("test.scp");
		}
		
	}
}

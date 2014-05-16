/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 15.05.2014
 * Время: 16:56
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DrawLibrary;
using DrawLibrary.Tools;
using NUnit.Framework;

namespace Tests
{
	/// <summary>
	/// тестируем работу со слоями
	/// </summary>
	[TestFixture]
	public class Test_03_Layers
	{
		[TestFixtureSetUp]
		public void Init()
		{//в начале всех тестов
			_canvas = new DrawingCanvas();
		}
		private DrawingCanvas _canvas;

		[Test]
		public void Test_01_New()
		{
			Assert.AreEqual(_canvas.Layers.Count, 1, "нет слоя по умолчанию");
			Assert.IsTrue(_canvas.Layers[0].IsSelected, "слой по умолчанию не выделен");

			_canvas.AddLayer("name2");

			Assert.AreEqual(_canvas.Layers.Count, 2, "не добавился лист");
			Assert.IsFalse(_canvas.Layers[0].IsSelected, "не ушло выделение");
			Assert.IsTrue(_canvas.Layers[1].IsSelected, "новый слой не выделился");
		}

		[Test]
		public void Test_02_Add_Object()
		{//отдельно не запускать (нужен слой, который создается в Test_01
			//рисуем полигон
			_canvas.Tool = ToolType.Polygone;			
			Click(10, 10);
			Click(100, 100);
			Click(50, 200);			
			_canvas.EscapeCommand.Execute(null);

			Assert.AreEqual(_canvas.Layers[0].Objects.Count, 0, "добавилось не в тот слой");
			Assert.AreEqual(_canvas.Layers[1].Objects.Count, 1, "не добавилось на слой");
		}
		
		
		private void Click(double x, double y)
		{
			var p = new Point(x, y);
			_canvas.Down(p);
			Thread.Sleep(500);
			_canvas.Up(p);
		}
	}
}

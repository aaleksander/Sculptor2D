/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 28.04.2014
 * Время: 15:41
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using DrawLibrary;
using DrawLibrary.Graphics;
using DrawLibrary.Tools;
using NUnit.Framework;

namespace Tests
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	[TestFixture]
	public class Test_01
	{
		[Test]
		public void Test_01_CursorNone()
		{//убираем курсор
			var canvas = new DrawingCanvas();
			
			canvas.SetCursor(DrawingCursorType.None);
			
			Assert.IsNull(canvas.GetObjectBy(x=> x is GraphicsCursor), "Нашел то, чего не надо");
		}

		[Test]
		public void Test_02_CursorBrush()
		{//убираем курсор
			var canvas = new DrawingCanvas();

			var tmp = canvas.SetCursor(DrawingCursorType.Brush );

			var o = canvas.GetObjectBy( x => x is GraphicsCursor );

			Assert.AreEqual(o, tmp, "Не равны");
			Assert.IsNotNull(o, "Не нашел объект");
			Assert.IsTrue(o is GraphicsCursor, "Не тот тип");
			

		}
		
		[Test]
		public void Test03_AlwaysTop()
		{//пока не понятно, где обновлять курсор. Если в Tool, то и фигуры надо создавать через Tool
			/*var canvas = new DrawingCanvas();
			var cur = canvas.SetCursor(DrawingCursorType.Brush);

			// проверить, чтобы всегда был сверху
			var poly = new GraphicsPolygon();
			poly.AddPoint(0, 0);
			poly.AddPoint(100, 100);
			poly.AddPoint(0, 200);
			canvas.GraphicsList.Add(poly); //добавляем полигон, при этом курсор должен перескочить наверх
			
			int i = canvas.GetIndexBy(x => x is GraphicsCursor);
			
			Assert.AreEqual(i, 1, "не попал наверх");*/
		}
	}
}
/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.04.2014
 * Время: 16:45
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace DrawLibrary.Graphics
{
	/// <summary>
	/// Description of GraphicsLine.
	/// </summary>
	public class GraphicsLine: GraphicsMultiPoint
	{
		public GraphicsLine(){}
		
		/// <summary>
		/// делает полную копию объекта
		/// </summary>
		/// <returns></returns>
		public override GraphicsBase Clone()
		{
			GraphicsLine res = new GraphicsLine();
			CloneAttributes(res);
			return res;
		}
		
	}
}

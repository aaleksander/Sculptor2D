/*
 * Сделано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 25.04.2014
 * Время: 12:50
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;

namespace DrawLibrary.Undo
{
	/// <summary>
	/// базовый класс для всех действий, которые можно откатить
	/// </summary>
	abstract public class ActionBase
	{
        public abstract void Undo(DrawingCanvas drawingCanvas);
	    public abstract void Redo(DrawingCanvas drawingCanvas);	
	}
}

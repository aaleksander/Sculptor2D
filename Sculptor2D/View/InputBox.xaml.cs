/*
 * Сделано в SharpDevelop.
 * Пользователь: anufrievaa
 * Дата: 16.05.2014
 * Время: 11:52
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Sculptor2D.View
{
	/// <summary>
	/// Interaction logic for InputBox.xaml
	/// </summary>
	public partial class InputBox : Window
	{
		public InputBox()
		{
			InitializeComponent();
			
			DataContext = this;
			
			Prompt = "Введите что-нибудь:";
		}
		
		void Button_Click(object sender, RoutedEventArgs e)
		{
			if( Value == "" )
			{
				MessageBox.Show("Пустое значение не допустимо");
				return;
			}
			
			DialogResult = true;
		}

		public String Prompt{set;get;}
		public String Value{set;get;}
	}
}
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace Sculptor2D
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
            CultureInfo ci = new CultureInfo("ru-RU", true);
            ci.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = ci;			
		}
	}
}
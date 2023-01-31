using Library.Persistent;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.Client
{
	public partial class App : System.Windows.Application
	{
		private void Application_Exit(object sender, System.Windows.ExitEventArgs e)
		{
			Environment.Exit(0);
		}
	}
}
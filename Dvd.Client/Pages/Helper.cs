
using System.Windows.Forms;
using System.Windows.Input;

namespace Library.Client.Pages
{
	static internal class Helper
	{
		static internal void Open(System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.F1)
			{
				System.Console.WriteLine(e.Key);
				Help.ShowHelp(null, "3333.chm");
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Paraiba.TaskList;

namespace FarenDotNet
{
    static public class Program
    {
		/// <summary>
        /// アプリケーションのメイン エントリ ポイントです。（でした）
        /// </summary>
        [STAThread]
        static void Main()
        {
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			var mainWindow = new MainWindow();
			mainWindow.Show();

			Global.MainLoop.Do(() => mainWindow.Created);
		}
    }
}

using System;
using System.Windows.Forms;
using GS.Core.UI.Demo.Forms;

namespace GS.Core.UI.Demo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Forms.Pages.FrmConsultaCliente());
            Application.Run(new MainForm());
        }
    }
}

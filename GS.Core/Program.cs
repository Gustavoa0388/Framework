using ECTurbo_CRUD;


namespace GS.Core.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            //if (MYSQL.TestarConexao() == string.Empty)
                Application.Run(new Formularios.FrmPrincipal());
            //else
                //Funcoes.Modal(new Formularios.FrmConexaoMySQL());
            
        }
    }
}
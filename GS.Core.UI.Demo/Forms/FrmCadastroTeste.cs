using System.Windows.Forms;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Demo.Forms
{
    public partial class FrmCadastroTeste : FormBaseCadastro
    {
        public FrmCadastroTeste()
        {
            InitializeComponent();

            // Exemplo: título do form
            Text = "Cadastro de Teste";
        }

        /// <summary>
        /// Simula o salvamento para validar o fluxo do GS Core.
        /// </summary>
        protected override bool OnSalvar()
        {
            MessageBox.Show(
                "Salvo com sucesso (teste)",
                "GS Core",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            return true;
        }
    }
}

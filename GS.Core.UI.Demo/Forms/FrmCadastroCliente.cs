using System.Windows.Forms;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Demo.Forms
{
    public partial class FrmCadastroCliente : FormBaseCadastro
    {
        public FrmCadastroCliente()
        {
            InitializeComponent();

            Text = "Cadastro de Cliente";
        }

        /// <summary>
        /// Lógica real de salvamento do cliente.
        /// </summary>
        protected override bool OnSalvar()
        {
            // Exemplo:
            // var nome = txtNome.Text;
            // var idade = numericIdade.Value;
            // Salvar no banco...

            MessageBox.Show(
                "Cliente salvo com sucesso!",
                "GS Core",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            return true;
        }
    }
}

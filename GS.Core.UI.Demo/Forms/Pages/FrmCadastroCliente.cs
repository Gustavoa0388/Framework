using System;
using System.Windows.Forms;
using GS.Core.UI.Controls.Inputs;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmCadastroCliente
    ///
    /// Tela DEMO de cadastro de cliente.
    /// Demonstra uso correto do GS Core UI.
    /// </summary>
    public class FrmCadastroCliente : FormBaseCadastro
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private Label lblNome;
        private GsTextBox txtNome;

        private Label lblEmail;
        private GsTextBox txtEmail;

        private GsCheckBox chkAtivo;

        private Button btnSalvar;
        private Button btnCancelar;

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        public FrmCadastroCliente()
        {
            Text = "Cadastro de Cliente";
            Width = 500;
            Height = 300;

            BuildLayout();
        }

        // =====================================================
        // LAYOUT
        // =====================================================

        private void BuildLayout()
        {
            lblNome = new Label
            {
                Text = "Nome",
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20)
            };

            txtNome = new GsTextBox
            {
                Required = true,
                Location = new System.Drawing.Point(20, 40),
                Width = 420
            };

            lblEmail = new Label
            {
                Text = "E-mail",
                AutoSize = true,
                Location = new System.Drawing.Point(20, 80)
            };

            txtEmail = new GsTextBox
            {
                Required = true,
                Location = new System.Drawing.Point(20, 100),
                Width = 420
            };

            chkAtivo = new GsCheckBox
            {
                Text = "Ativo",
                Location = new System.Drawing.Point(20, 150),
                Checked = true
            };

            btnSalvar = new Button
            {
                Text = "Salvar",
                Width = 100,
                Location = new System.Drawing.Point(240, 200)
            };

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Location = new System.Drawing.Point(350, 200)
            };

            btnSalvar.Click += (_, _) => Save();
            btnCancelar.Click += (_, _) => Close();

            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(chkAtivo);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        // =====================================================
        // FLUXO BASE
        // =====================================================

        protected override void OnLoadEntity()
        {
            // DEMO:
            // Cadastro novo
        }

        protected override void OnSaveEntity()
        {
            // DEMO:
            var nome = txtNome.Text;
            var email = txtEmail.Text;
            var ativo = chkAtivo.Checked;

            // Simula sucesso
        }
    }
}

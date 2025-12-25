using System;
using System.Windows.Forms;
using GS.Core.UI.Controls.Inputs;
using GS.Core.UI.Forms;
using GS.Core.UI.Forms.Navigation;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmCadastroCliente
    ///
    /// Tela DEMO de cadastro de cliente.
    ///
    /// OBJETIVO:
    /// - Demonstrar uso do FormBaseCadastro
    /// - Validar fluxo de salvamento com retorno semântico
    ///
    /// OBSERVAÇÕES IMPORTANTES:
    /// - Não acessa banco
    /// - Não possui regra de negócio
    /// - Serve apenas para validar o GS Core UI
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

        /// <summary>
        /// Cria o layout manualmente.
        /// 
        /// DECISÃO:
        /// - Layout simples
        /// - Sem Designer (mais controle e previsibilidade)
        /// </summary>
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

            // =================================================
            // EVENTOS
            // =================================================

            btnSalvar.Click += (_, _) => Save();

            btnCancelar.Click += (_, _) =>
            {
                // Encerramento explícito com intenção
                CloseWithResult(FormResult.Canceled());
            };

            Controls.Add(lblNome);
            Controls.Add(txtNome);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(chkAtivo);
            Controls.Add(btnSalvar);
            Controls.Add(btnCancelar);
        }

        // =====================================================
        // FLUXO BASE (FORMBASECADASTRO)
        // =====================================================

        /// <summary>
        /// Carregamento da entidade.
        /// 
        /// DEMO:
        /// - Sempre cadastro novo
        /// </summary>
        protected override void OnLoadEntity()
        {
            // Nada a carregar (DEMO)
        }

        /// <summary>
        /// Salvamento da entidade.
        /// 
        /// DEMO:
        /// - Simula sucesso
        /// - Encerra com resultado Saved
        /// </summary>
        protected override void OnSaveEntity()
        {
            var nome = txtNome.Text;
            var email = txtEmail.Text;
            var ativo = chkAtivo.Checked;

            // Simula salvamento bem-sucedido
            CloseWithResult(FormResult.Saved());
        }
    }
}

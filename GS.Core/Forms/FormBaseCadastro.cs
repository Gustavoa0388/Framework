using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// Base padrão para formulários de cadastro.
    /// Fornece layout consistente, validação automática e fluxo de salvamento.
    /// </summary>
    public partial class FormBaseCadastro : GsBaseForm
    {
        // ============================
        // CONFIGURAÇÕES PÚBLICAS
        // ============================

        public bool ShowCancelButton { get; set; } = true;
        public bool CloseOnSave { get; set; } = true;
        public bool ConfirmCancel { get; set; } = true;

        // ============================
        // CONTROLES BASE
        // ============================

        protected Panel ContentPanel;
        protected Panel ActionPanel;

        protected Button BtnSalvar;
        protected Button BtnCancelar;

        // ============================
        // CONSTRUTOR
        // ============================

        public FormBaseCadastro()
        {
            InitializeComponent(); // esse é do próprio FormBaseCadastro
            InitializeLayout();
        }


        // ============================
        // LAYOUT BASE
        // ============================

        private void InitializeLayout()
        {
            SuspendLayout();

            // Painel principal (conteúdo)
            ContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                BackColor = Color.Transparent
            };

            // Painel inferior (ações)
            ActionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(10),
                BackColor = Color.Transparent
            };

            // Botão Salvar
            BtnSalvar = new Button
            {
                Text = "Salvar",
                Width = 100,
                Height = 32,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            BtnSalvar.Click += (_, _) => OnSalvarClick();

            // Botão Cancelar
            BtnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 100,
                Height = 32,
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            BtnCancelar.Click += (_, _) => OnCancelarClick();

            ActionPanel.Controls.Add(BtnSalvar);
            ActionPanel.Controls.Add(BtnCancelar);

            Controls.Add(ContentPanel);
            Controls.Add(ActionPanel);

            ResumeLayout();
        }

        // ============================
        // POSICIONAMENTO DOS BOTÕES
        // ============================

        protected virtual void PositionButtons()
        {
            if (BtnSalvar == null || ActionPanel == null)
                return;

            BtnCancelar.Visible = ShowCancelButton;

            int right = ActionPanel.Width - 10;
            int top = (ActionPanel.Height - BtnSalvar.Height) / 2;

            if (BtnCancelar.Visible)
            {
                BtnCancelar.Location = new Point(
                    right - BtnCancelar.Width,
                    top
                );

                right -= BtnCancelar.Width + 10;
            }

            BtnSalvar.Location = new Point(
                right - BtnSalvar.Width,
                top
            );
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionButtons();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            PositionButtons();
        }

        // ============================
        // FLUXO DE SALVAR
        // ============================

        private void OnSalvarClick()
        {
            if (!ValidateForm())
                return;

            try
            {
                bool sucesso = OnSalvar();

                if (sucesso)
                {
                    OnSalvarSuccess();

                    if (CloseOnSave)
                        Close();
                }
            }
            catch (Exception ex)
            {
                OnSalvarError(ex);
            }
        }

        // ============================
        // FLUXO DE CANCELAR
        // ============================

        private void OnCancelarClick()
        {
            if (ConfirmCancel)
            {
                var result = MessageBox.Show(
                    "Deseja cancelar as alterações?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                    return;
            }

            Close();
        }

        // ============================
        // GANCHOS PARA OVERRIDE
        // ============================

        /// <summary>
        /// Executa a lógica de salvamento.
        /// Retorne true para indicar sucesso.
        /// </summary>
        protected virtual bool OnSalvar()
        {
            return true;
        }

        /// <summary>
        /// Executado após salvar com sucesso.
        /// </summary>
        protected virtual void OnSalvarSuccess()
        {
            // Hook para toast, log, eventos, etc.
        }

        /// <summary>
        /// Executado quando ocorre erro no salvamento.
        /// </summary>
        protected virtual void OnSalvarError(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Erro ao salvar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}

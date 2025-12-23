using GS.Core.UI.Formularios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// FormBaseCadastro
    /// 
    /// Classe base para formulários de cadastro (Create / Edit).
    /// 
    /// RESPONSABILIDADES:
    /// - Fornecer layout padrão (conteúdo + ações)
    /// - Centralizar fluxo de salvar e cancelar
    /// - Executar validação global automaticamente
    /// 
    /// CARACTERÍSTICAS:
    /// - NÃO utiliza Designer
    /// - Deve ser herdada por formulários concretos
    /// - Usa GsBaseForm como base
    /// 
    /// OBSERVAÇÃO:
    /// - Os botões ainda são Button nativo
    ///   (migração para GsButton fica para fase extra)
    /// </summary>
    public class FormBaseCadastro : GsBaseForm
    {
        // ============================
        // CONFIGURAÇÕES PÚBLICAS
        // ============================

        /// <summary>
        /// Define se o botão Cancelar deve ser exibido.
        /// </summary>
        public bool ShowCancelButton { get; set; } = true;

        /// <summary>
        /// Define se o formulário deve fechar após salvar com sucesso.
        /// </summary>
        public bool CloseOnSave { get; set; } = true;

        /// <summary>
        /// Define se deve solicitar confirmação ao cancelar.
        /// </summary>
        public bool ConfirmCancel { get; set; } = true;

        // ============================
        // CONTROLES BASE
        // ============================

        /// <summary>
        /// Painel onde os controles de cadastro devem ser adicionados.
        /// </summary>
        protected Panel ContentPanel;

        /// <summary>
        /// Painel inferior de ações (Salvar / Cancelar).
        /// </summary>
        protected Panel ActionPanel;

        protected Button BtnSalvar;
        protected Button BtnCancelar;

        // ============================
        // CONSTRUTOR
        // ============================

        protected FormBaseCadastro()
        {
            InitializeLayout();
        }

        // ============================
        // LAYOUT BASE
        // ============================

        private void InitializeLayout()
        {
            SuspendLayout();

            // Painel de conteúdo (inputs)
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
            // Validação global vem do GsBaseForm
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
                if (!FormMsg.Confirm("Deseja cancelar as alterações?"))
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
        protected virtual bool OnSalvar() => true;

        /// <summary>
        /// Executado após salvar com sucesso.
        /// </summary>
        protected virtual void OnSalvarSuccess() { }

        /// <summary>
        /// Executado quando ocorre erro no salvamento.
        /// </summary>
        protected virtual void OnSalvarError(Exception ex)
        {
            FormMsg.Error(ex.Message);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// Form base para cadastros do GS Core.
    /// Centraliza botões, validação e fluxo de ações.
    /// </summary>
    public class FormBaseCadastro : GsBaseForm

    {
        // ============================
        // BOTÕES PADRÃO
        // ============================

        protected Button BtnSalvar;
        protected Button BtnCancelar;

        protected FormBaseCadastro()
        {
            CriarBotoesPadrao();
        }

        /// <summary>
        /// Cria os botões Salvar e Cancelar com layout padrão.
        /// </summary>
        private void CriarBotoesPadrao()
        {
            BtnSalvar = new Button
            {
                Text = "Salvar",
                Width = 120,
                Height = 35,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            BtnCancelar = new Button
            {
                Text = "Cancelar",
                Width = 120,
                Height = 35,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            // Eventos
            BtnSalvar.Click += (_, _) => ExecutarSalvar();
            BtnCancelar.Click += (_, _) => ExecutarCancelar();

            Controls.Add(BtnSalvar);
            Controls.Add(BtnCancelar);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PosicionarBotoes();
        }

        /// <summary>
        /// Posiciona os botões no canto inferior direito.
        /// </summary>
        protected virtual void PosicionarBotoes()
        {
            if (BtnSalvar == null || BtnCancelar == null)
                return;

            int margin = 15;

            BtnCancelar.Location = new Point(
                ClientSize.Width - BtnCancelar.Width - margin,
                ClientSize.Height - BtnCancelar.Height - margin
            );

            BtnSalvar.Location = new Point(
                BtnCancelar.Left - BtnSalvar.Width - 10,
                BtnCancelar.Top
            );
        }



        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (DesignMode)
                return;

            PosicionarBotoes();
        }


        // ============================
        // FLUXO DE AÇÃO
        // ============================

        private void ExecutarSalvar()
        {
            // 1️⃣ Validação global
            if (!ValidarFormulario())
            {
                OnFalhaValidacao();
                return;
            }

            // 2️⃣ Hook de negócio
            OnSalvar();
        }

        private void ExecutarCancelar()
        {
            if (OnCancelar())
            {
                Close();
            }
        }

        // ============================
        // MÉTODOS PARA SOBRESCRITA
        // ============================

        /// <summary>
        /// Implementação obrigatória da lógica de salvar.
        /// </summary>
        protected virtual void OnSalvar()
        {
            throw new NotImplementedException(
                "Implemente o método OnSalvar no formulário derivado."
            );
        }


        /// <summary>
        /// Executado quando a validação falha.
        /// Pode ser sobrescrito para exibir mensagens customizadas.
        /// </summary>
        protected virtual void OnFalhaValidacao()
        {
            // Gancho para FormMsg, toast, log, etc.
        }

        /// <summary>
        /// Executado ao clicar em cancelar.
        /// Retorne false para impedir o fechamento.
        /// </summary>
        protected virtual bool OnCancelar()
        {
            return true;
        }
    }
}

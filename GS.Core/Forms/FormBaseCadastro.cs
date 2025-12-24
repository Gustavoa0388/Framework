using GS.Core.UI.Controls.UX;
using GS.Core.UI.Forms;
using System;
using System.Windows.Forms;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// FormBaseCadastro
    ///
    /// Form base para telas de cadastro/edição.
    ///
    /// RESPONSABILIDADES:
    /// - Orquestrar estados de UX (Loading, Error, Success)
    /// - Centralizar fluxo de salvar / carregar registro
    /// - Integrar validação global do formulário
    ///
    /// NÃO FAZ:
    /// - Não usa Grid
    /// - Não usa Paginação
    /// - Não acessa banco
    /// - Não executa regra de negócio
    /// </summary>
    public abstract class FormBaseCadastro : GsBaseForm
    {
        // =====================================================
        // CONTROLES BASE
        // =====================================================

        protected GsStateView StateView { get; }

        // =====================================================
        // ESTADO
        // =====================================================

        protected bool IsReadOnly { get; private set; }

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        protected FormBaseCadastro()
        {
            StateView = new GsStateView
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            Controls.Add(StateView);
            StateView.BringToFront();
        }

        // =====================================================
        // CICLO DE VIDA
        // =====================================================

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            OnLoadEntity();
        }

        // =====================================================
        // FLUXO PRINCIPAL
        // =====================================================

        /// <summary>
        /// Carrega o registro (novo ou existente).
        /// Implementação obrigatória no formulário concreto.
        /// </summary>
        protected abstract void OnLoadEntity();

        /// <summary>
        /// Salva o registro.
        /// Implementação obrigatória no formulário concreto.
        /// </summary>
        protected abstract void OnSaveEntity();

        // =====================================================
        // AÇÕES PADRÃO
        // =====================================================

        protected virtual void Save()
        {
            if (!ValidateForm())
                return;

            SetLoading("Salvando...");

            try
            {
                OnSaveEntity();
                ShowSuccess("Registro salvo com sucesso.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        // =====================================================
        // UX STATES (PADRÃO BLOCO 4)
        // =====================================================

        protected void SetLoading(string message)
        {
            ToggleInputs(false);

            StateView.State = GsUxState.Loading;
            StateView.Message = message;
            StateView.ShowProgress = true;
            StateView.Visible = true;
        }

        protected void ShowSuccess(string message)
        {
            StateView.State = GsUxState.Success;
            StateView.Message = message;
            StateView.ShowProgress = false;
            StateView.Visible = true;

            // UX corporativa: feedback rápido e não bloqueante
            var timer = new System.Windows.Forms.Timer { Interval = 1500 };
            timer.Tick += (_, _) =>
            {
                timer.Stop();
                timer.Dispose();

                StateView.State = GsUxState.Hidden;
                StateView.Visible = false;
                ToggleInputs(true);
            };
            timer.Start();
        }

        protected void ShowError(string message)
        {
            ToggleInputs(true);

            StateView.State = GsUxState.Error;
            StateView.Message = message;
            StateView.ShowProgress = false;
            StateView.Visible = true;
        }

        protected void SetReadOnly(bool readOnly)
        {
            IsReadOnly = readOnly;
            ToggleInputs(!readOnly);
        }

        // =====================================================
        // UTILITÁRIOS
        // =====================================================

        protected virtual void ToggleInputs(bool enabled)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl == StateView)
                    continue;

                ctrl.Enabled = enabled;
            }
        }
    }
}

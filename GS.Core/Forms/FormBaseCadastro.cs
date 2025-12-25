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
    /// - Centralizar fluxo de carregar / salvar registro
    /// - Integrar validação global do formulário
    ///
    /// NÃO FAZ:
    /// - Não acessa banco
    /// - Não executa regra de negócio
    /// - Não conhece telas externas
    /// </summary>
    public abstract class FormBaseCadastro : GsBaseForm
    {
        // =====================================================
        // CONTROLES BASE
        // =====================================================

        protected GsStateView StateView { get; }

        private readonly GsBusyOverlay _busy;

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

            _busy = new GsBusyOverlay();
            Controls.Add(_busy);
            _busy.BringToFront();
        }

        // =====================================================
        // LIFECYCLE GS CORE
        // =====================================================

        /// <summary>
        /// Carrega o registro (novo ou existente).
        /// Executado automaticamente pelo lifecycle GS Core.
        /// </summary>
        protected override void OnLoadData()
        {
            OnLoadEntity();
        }

        /// <summary>
        /// Gancho para ajustes finais após carga do registro.
        /// </summary>
        protected override void OnAfterLoad()
        {
            // reservado para formulários concretos
        }

        // =====================================================
        // CONTRATOS OBRIGATÓRIOS
        // =====================================================

        /// <summary>
        /// Implementação obrigatória para carregar o registro.
        /// </summary>
        protected abstract void OnLoadEntity();

        /// <summary>
        /// Implementação obrigatória para salvar o registro.
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
        // UX STATES
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
        // BUSY OVERLAY
        // =====================================================

        protected void ShowBusy(string message)
        {
            _busy.Show(message);
        }

        protected void HideBusy()
        {
            _busy.Hide();
        }

        // =====================================================
        // UTILITÁRIOS
        // =====================================================

        protected virtual void ToggleInputs(bool enabled)
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl == StateView || ctrl == _busy)
                    continue;

                ctrl.Enabled = enabled;
            }
        }
    }
}

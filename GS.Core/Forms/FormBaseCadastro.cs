using System;
using System.Linq;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// FormBaseCadastro
    /// 
    /// Base oficial para todos os formulários de cadastro do GS Core.
    /// 
    /// RESPONSABILIDADE:
    /// - Controlar o ciclo de vida do cadastro
    /// - Centralizar habilitação/desabilitação de inputs
    /// - Executar validação via IGsValidatable
    /// 
    /// NÃO FAZ:
    /// - Layout
    /// - Mensagens
    /// - Persistência
    /// </summary>
    public abstract class FormBaseCadastro : GsBaseForm
    {
        /// <summary>
        /// Indica se o formulário está em modo de edição.
        /// </summary>
        protected bool IsEditMode { get; private set; }

        protected FormBaseCadastro()
        {
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;

            Load += OnFormLoad;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            OnInitialize();
            EnterViewMode();
        }

        // =====================================================
        // CONTRATOS OBRIGATÓRIOS
        // =====================================================

        protected abstract void OnInitialize();
        protected abstract void OnLoadData();
        protected abstract void OnSave();
        protected abstract void OnDelete();

        // =====================================================
        // MODOS
        // =====================================================

        protected virtual void EnterViewMode()
        {
            IsEditMode = false;
            SetInputsEnabled(false);
            OnLoadData();
        }

        protected virtual void EnterEditMode()
        {
            IsEditMode = true;
            SetInputsEnabled(true);
        }

        protected virtual void EnterNewMode()
        {
            IsEditMode = true;
            ClearInputs();
            SetInputsEnabled(true);
        }

        // =====================================================
        // AÇÕES PADRÃO
        // =====================================================

        protected void ActionNovo()
        {
            EnterNewMode();
        }

        protected void ActionEditar()
        {
            EnterEditMode();
        }

        protected void ActionSalvar()
        {
            ValidateForm(); // segue o contrato atual do Core

            OnSave();
            EnterViewMode();
        }

        protected void ActionExcluir()
        {
            OnDelete();
            Close();
        }

        protected void ActionCancelar()
        {
            EnterViewMode();
        }

        // =====================================================
        // VALIDAÇÃO
        // =====================================================

        protected virtual void ValidateForm()
        {
            var validatables = GetAllControls(this)
                .OfType<IGsValidatable>();

            foreach (var control in validatables)
            {
                control.Validate();
            }
        }

        // =====================================================
        // UTILITÁRIOS
        // =====================================================

        private void SetInputsEnabled(bool enabled)
        {
            foreach (var control in GetAllControls(this))
            {
                if (control is Control c && c is IGsValidatable)
                    c.Enabled = enabled;
            }
        }

        private void ClearInputs()
        {
            foreach (var control in GetAllControls(this))
            {
                switch (control)
                {
                    case TextBoxBase txt:
                        txt.Clear();
                        break;

                    case CheckBox chk:
                        chk.Checked = false;
                        break;

                    case RadioButton rb:
                        rb.Checked = false;
                        break;

                    case ComboBox cb:
                        cb.SelectedIndex = -1;
                        break;
                }
            }
        }

        private static Control[] GetAllControls(Control parent)
        {
            return parent.Controls
                .Cast<Control>()
                .SelectMany(GetAllControls)
                .Concat(parent.Controls.Cast<Control>())
                .ToArray();
        }
    }
}

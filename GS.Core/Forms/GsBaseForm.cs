using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// GsBaseForm
    ///
    /// Form base do GS Core UI.
    ///
    /// RESPONSABILIDADES:
    /// - Centralizar aplicação de tema
    /// - Oferecer validação global padronizada
    /// - Servir como base neutra para formulários
    ///
    /// NÃO FAZ:
    /// - Não controla estados de UX (Loading, Error, Empty)
    /// - Não executa lógica de negócio
    /// - Não define layout
    /// </summary>
    public class GsBaseForm : Form
    {
        // =====================================================
        // TEMA
        // =====================================================

        /// <summary>
        /// Aplica o tema automaticamente ao carregar o formulário.
        /// </summary>
        protected bool AutoApplyTheme { get; set; } = true;

        /// <summary>
        /// Permite sobrescrever o tema apenas neste formulário.
        /// Se null, utiliza o tema atual do ThemeManager.
        /// </summary>
        protected GsTheme CustomTheme { get; set; }

        // =====================================================
        // CONSTRUTOR
        // =====================================================

        protected GsBaseForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        // =====================================================
        // CICLO DE VIDA
        // =====================================================

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyThemeIfNeeded();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Gancho futuro:
            // - métricas
            // - logging
            // - animações
        }

        // =====================================================
        // TEMA
        // =====================================================

        protected virtual void ApplyThemeIfNeeded()
        {
            if (!AutoApplyTheme)
                return;

            var theme = CustomTheme ?? ThemeManager.Current;
            if (theme == null)
                return;

            ThemeManager.ApplyTheme(this, theme);
            Invalidate(true);
        }

        // =====================================================
        // VALIDAÇÃO GLOBAL
        // =====================================================

        /// <summary>
        /// Valida todos os controles que implementam IGsValidatable.
        /// Retorna true se todos estiverem válidos.
        /// </summary>
        public virtual bool ValidateForm()
        {
            var inputs = GetAllValidatableControls(this);

            IGsValidatable firstInvalid = null;

            foreach (var input in inputs)
            {
                input.Validate();

                if (!input.IsValid && firstInvalid == null)
                    firstInvalid = input;
            }

            // UX profissional: foca no primeiro erro
            if (firstInvalid is Control ctrl && ctrl.CanFocus)
                ctrl.Focus();

            return firstInvalid == null;
        }

        /// <summary>
        /// Obtém recursivamente todos os controles IGsValidatable.
        /// Inclui Panels, GroupBox, TabPages, etc.
        /// </summary>
        protected virtual List<IGsValidatable> GetAllValidatableControls(Control parent)
        {
            var list = new List<IGsValidatable>();

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is IGsValidatable validatable)
                    list.Add(validatable);

                if (ctrl.HasChildren)
                    list.AddRange(GetAllValidatableControls(ctrl));
            }

            return list;
        }

        // =====================================================
        // LEGADO (COMPATIBILIDADE)
        // =====================================================

        /// <summary>
        /// Método legado em PT-BR.
        /// Use ValidateForm().
        /// </summary>
        [Obsolete("Use ValidateForm()")]
        public bool ValidarFormulario()
        {
            return ValidateForm();
        }
    }
}

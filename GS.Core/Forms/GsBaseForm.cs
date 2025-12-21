using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Forms
{
    /// <summary>
    /// Form base do GS Core.
    /// Centraliza aplicação de tema, validação global e comportamento padrão.
    /// NÃO utiliza Designer.
    /// </summary>
    public class GsBaseForm : Form
    {
        // ============================
        // CONFIGURAÇÕES DE TEMA
        // ============================

        /// <summary>
        /// Define se o tema será aplicado automaticamente ao carregar o Form.
        /// </summary>
        protected bool AutoApplyTheme { get; set; } = true;

        /// <summary>
        /// Permite sobrescrever o tema padrão apenas neste Form.
        /// Se null, usa o tema atual do ThemeManager.
        /// </summary>
        protected GsTheme CustomTheme { get; set; }

        // ============================
        // CONSTRUTOR
        // ============================

        protected GsBaseForm()
        {
            // Boas práticas padrão
            StartPosition = FormStartPosition.CenterScreen;
            Font = SystemFonts.DefaultFont;
        }

        // ============================
        // CICLO DE VIDA
        // ============================

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyThemeIfNeeded();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Gancho futuro: animações, logging, métricas
        }

        // ============================
        // TEMA
        // ============================

        protected virtual void ApplyThemeIfNeeded()
        {
            if (!AutoApplyTheme)
                return;

            var theme = CustomTheme ?? ThemeManager.Current;
            if (theme == null)
                return;

            ThemeManager.ApplyTheme(this, theme);
        }

        // ============================
        // VALIDAÇÃO GLOBAL
        // ============================

        /// <summary>
        /// Valida todos os inputs que implementam IGsValidatable.
        /// Retorna true se todos estiverem válidos.
        /// Pode ser sobrescrito por formulários especializados.
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

            // Foca no primeiro erro (UX profissional)
            if (firstInvalid is Control ctrl && ctrl.CanFocus)
            {
                ctrl.Focus();
            }

            return firstInvalid == null;
        }

        /// <summary>
        /// Obtém recursivamente todos os controles que implementam IGsValidatable.
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

        // ============================
        // LEGADO / COMPATIBILIDADE (OPCIONAL)
        // ============================

        /// <summary>
        /// Método legado em PT-BR para compatibilidade temporária.
        /// Use ValidateForm().
        /// </summary>
        [Obsolete("Use ValidateForm()")]
        public bool ValidarFormulario()
        {
            return ValidateForm();
        }
    }
}

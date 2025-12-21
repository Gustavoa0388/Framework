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
    /// Centraliza tema, validação e comportamento padrão dos formulários.
    /// TODOS os forms do sistema devem herdar deste.
    /// </summary>
    public partial class GsBaseForm : Form
    {
        /// <summary>
        /// Define se o tema será aplicado automaticamente ao carregar o Form.
        /// </summary>
        protected bool AutoApplyTheme { get; set; } = true;

        /// <summary>
        /// Permite sobrescrever o tema padrão para este Form.
        /// Se null, usa o tema atual do ThemeManager.
        /// </summary>
        protected GsTheme CustomTheme { get; set; }

        protected GsBaseForm()
        {
            InitializeComponent();

            // Boas práticas padrão
            StartPosition = FormStartPosition.CenterScreen;
            Font = SystemFonts.DefaultFont;
        }

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

        /// <summary>
        /// Aplica o tema automaticamente, se habilitado.
        /// </summary>
        protected virtual void ApplyThemeIfNeeded()
        {
            if (!AutoApplyTheme)
                return;

            var theme = CustomTheme ?? ThemeManager.Current;
            if (theme == null)
                return;

            ThemeManager.ApplyTheme(this, theme);
        }

        /// <summary>
        /// Valida todos os inputs do formulário.
        /// Retorna true se todos estiverem válidos.
        /// </summary>
        public virtual bool ValidarFormulario()
        {
            var inputs = ObterTodosInputs(this);

            bool allValid = true;

            foreach (var input in inputs)
            {
                input.Validate();

                if (!input.IsValid)
                {
                    allValid = false;
                }
            }

            return allValid;
        }

        /// <summary>
        /// Obtém todos os controles que implementam IGsValidatable,
        /// incluindo os que estão dentro de Panels, TabPages, GroupBox etc.
        /// </summary>
        protected virtual List<IGsValidatable> ObterTodosInputs(Control parent)
        {
            var list = new List<IGsValidatable>();

            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is IGsValidatable validatable)
                    list.Add(validatable);

                if (ctrl.HasChildren)
                    list.AddRange(ObterTodosInputs(ctrl));
            }

            return list;
        }
    }
}

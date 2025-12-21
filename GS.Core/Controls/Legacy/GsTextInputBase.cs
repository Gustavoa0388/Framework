// LEGACY CONTROL
// Originado do ECTurbo
// Mantido apenas para compatibilidade
// NÃO segue o padrão GS Core

using GS.Core.UI.Theming;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controls.Legacy
{
    [ToolboxItem(false)]
    public abstract class GsTextInputBase : TextBox, IThemedControl
    {
        protected GsTheme Theme;

        protected GsTextInputBase()
        {
            BorderStyle = BorderStyle.FixedSingle;

            MouseEnter += (_, _) => HandleHover();
            MouseLeave += (_, _) => HandleLeave();
            GotFocus += (_, _) => HandleFocus();
            LostFocus += (_, _) => HandleBlur();
        }

        public virtual void ApplyTheme(GsTheme theme)
        {
            Theme = theme;

            BackColor = theme.InputBackground;
            ForeColor = theme.TextPrimary;
        }

        protected virtual void HandleHover()
        {
            if (!Focused)
                BackColor = Theme.InputHover;
        }

        protected virtual void HandleLeave()
        {
            if (!Focused)
                BackColor = Theme.InputBackground;
        }

        protected virtual void HandleFocus()
        {
            BackColor = Theme.InputFocus;
        }

        protected virtual void HandleBlur()
        {
            BackColor = Theme.InputBackground;
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Controls
{
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

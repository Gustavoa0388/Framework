using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    public class GsTextBox : GsInputBase
    {
        protected override TextBox CreateInnerTextBox()
        {
            var txt = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Location = new Point(6, 7),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Font = Font
            };

            txt.GotFocus += (_, _) =>
            {
                IsFocused = true;
                Invalidate();
            };

            txt.LostFocus += (_, _) =>
            {
                IsFocused = false;
                ValidateRequired();
                Invalidate();
            };

            txt.TextChanged += (_, _) =>
            {
                ClearError();
            };

            return txt;
        }
    }
}

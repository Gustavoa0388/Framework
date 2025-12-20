using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    public class GsTextBox : GsInputBase
    {
        protected override TextBox CreateInnerTextBox()
        {
            return new TextBox();
        }
    }
}

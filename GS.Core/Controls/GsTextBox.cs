using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// TextBox padrão do GS Core.
    /// Toda a lógica visual, validação e erro
    /// vem do GsInputBase.
    /// </summary>
    public class GsTextBox : GsInputBase
    {
        protected override TextBox CreateInnerTextBox()
        {
            return new TextBox();
        }
    }
}

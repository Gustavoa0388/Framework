using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Controles.Base
{
    public abstract class GsControlBase : Control
    {
        public Color BorderColor { get; set; } = Color.Gray;
        public int BorderThickness { get; set; } = 1;
        public int BorderRadius { get; set; } = 0;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Base pronta pra estilos futuros
        }
    }
}

using System.Windows.Forms;
using GS.Core.UI.Theming;

namespace GS.Core.UI.Forms
{
    public partial class GsBaseForm : Form
    {
        protected bool AutoApplyTheme { get; set; } = true;

        public GsBaseForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (AutoApplyTheme && ThemeManager.Current != null)
            {
                ThemeManager.ApplyTheme(this, ThemeManager.Current);
            }
        }
    }
}

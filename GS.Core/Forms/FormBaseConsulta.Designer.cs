namespace GS.Core.UI.Forms
{
    partial class FormBaseConsulta
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Name = "FormBaseConsulta";
            this.Text = "Consulta";
            this.ResumeLayout(false);
        }
    }
}

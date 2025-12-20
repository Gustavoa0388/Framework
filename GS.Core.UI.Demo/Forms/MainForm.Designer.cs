namespace GS.Core.UI.Demo
{
    partial class MainForm : GS.Core.UI.Forms.GsBaseForm

    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabMain = new TabControl();
            Inputs = new TabPage();
            Buttons = new TabPage();
            Layout = new TabPage();
            Charts = new TabPage();
            tabMain.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(Inputs);
            tabMain.Controls.Add(Buttons);
            tabMain.Controls.Add(Layout);
            tabMain.Controls.Add(Charts);
            tabMain.Location = new Point(12, 12);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(776, 515);
            tabMain.TabIndex = 0;
            // 
            // Inputs
            // 
            Inputs.BackColor = Color.White;
            Inputs.Location = new Point(4, 24);
            Inputs.Name = "Inputs";
            Inputs.Padding = new Padding(3);
            Inputs.Size = new Size(768, 487);
            Inputs.TabIndex = 0;
            Inputs.Text = "Inputs";
            // 
            // Buttons
            // 
            Buttons.BackColor = Color.White;
            Buttons.Location = new Point(4, 24);
            Buttons.Name = "Buttons";
            Buttons.Padding = new Padding(3);
            Buttons.Size = new Size(768, 487);
            Buttons.TabIndex = 1;
            Buttons.Text = "Buttons";
            // 
            // Layout
            // 
            Layout.BackColor = Color.White;
            Layout.Location = new Point(4, 24);
            Layout.Name = "Layout";
            Layout.Padding = new Padding(3);
            Layout.Size = new Size(768, 487);
            Layout.TabIndex = 2;
            Layout.Text = "Layout";
            // 
            // Charts
            // 
            Charts.BackColor = Color.White;
            Charts.Location = new Point(4, 24);
            Charts.Name = "Charts";
            Charts.Padding = new Padding(3);
            Charts.Size = new Size(768, 487);
            Charts.TabIndex = 3;
            Charts.Text = "Charts";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 539);
            Controls.Add(tabMain);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.Black;
            Name = "MainForm";
            Text = "MainForm";
            tabMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage Inputs;
        private TabPage Buttons;
        private TabPage Layout;
        private TabPage Charts;
    }
}

namespace ECTurbo.Formularios
{
    partial class FormMsg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMsg));
            MpConteudo = new TabControl();
            pagSucesso = new TabPage();
            MsgSucesso = new Label();
            BtOkSucesso = new Button();
            TituloSucesso = new Label();
            pictureBox1 = new PictureBox();
            pagAlertas = new TabPage();
            MsgAlerta = new Label();
            BtOkAlertas = new Button();
            TituloALerta = new Label();
            pictureBox2 = new PictureBox();
            pagErros = new TabPage();
            MsgErro = new TextBox();
            BtOkErros = new Button();
            TituloErro = new Label();
            pictureBox3 = new PictureBox();
            pagConfirmar = new TabPage();
            MsgPergunta = new Label();
            BtSim = new Button();
            BtNao = new Button();
            TituloPergunta = new Label();
            pictureBox4 = new PictureBox();
            MpConteudo.SuspendLayout();
            pagSucesso.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pagAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            pagErros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            pagConfirmar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // MpConteudo
            // 
            MpConteudo.Alignment = TabAlignment.Bottom;
            MpConteudo.Controls.Add(pagSucesso);
            MpConteudo.Controls.Add(pagAlertas);
            MpConteudo.Controls.Add(pagErros);
            MpConteudo.Controls.Add(pagConfirmar);
            MpConteudo.Location = new Point(-6, -10);
            MpConteudo.Name = "MpConteudo";
            MpConteudo.SelectedIndex = 0;
            MpConteudo.Size = new Size(513, 277);
            MpConteudo.TabIndex = 0;
            MpConteudo.KeyDown += MpConteudo_KeyDown;
            // 
            // pagSucesso
            // 
            pagSucesso.BackColor = Color.White;
            pagSucesso.Controls.Add(MsgSucesso);
            pagSucesso.Controls.Add(BtOkSucesso);
            pagSucesso.Controls.Add(TituloSucesso);
            pagSucesso.Controls.Add(pictureBox1);
            pagSucesso.Location = new Point(4, 4);
            pagSucesso.Name = "pagSucesso";
            pagSucesso.Padding = new Padding(3);
            pagSucesso.Size = new Size(505, 249);
            pagSucesso.TabIndex = 0;
            pagSucesso.Text = "Sucessos";
            // 
            // MsgSucesso
            // 
            MsgSucesso.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MsgSucesso.ForeColor = Color.MediumSeaGreen;
            MsgSucesso.Location = new Point(194, 78);
            MsgSucesso.Name = "MsgSucesso";
            MsgSucesso.Size = new Size(288, 113);
            MsgSucesso.TabIndex = 3;
            MsgSucesso.Text = "Aqui vai o texto referente a mensagem desejada";
            MsgSucesso.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtOkSucesso
            // 
            BtOkSucesso.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtOkSucesso.ForeColor = Color.MediumSeaGreen;
            BtOkSucesso.Location = new Point(392, 194);
            BtOkSucesso.Name = "BtOkSucesso";
            BtOkSucesso.Size = new Size(90, 33);
            BtOkSucesso.TabIndex = 2;
            BtOkSucesso.Text = "Ok";
            BtOkSucesso.UseVisualStyleBackColor = true;
            BtOkSucesso.Click += BtOkSucesso_Click;
            // 
            // TituloSucesso
            // 
            TituloSucesso.Font = new Font("Showcard Gothic", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TituloSucesso.ForeColor = Color.MediumSeaGreen;
            TituloSucesso.Location = new Point(39, 22);
            TituloSucesso.Name = "TituloSucesso";
            TituloSucesso.Size = new Size(454, 53);
            TituloSucesso.TabIndex = 0;
            TituloSucesso.Text = "SUCESSO!";
            TituloSucesso.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(11, 65);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(177, 162);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pagAlertas
            // 
            pagAlertas.BackColor = Color.White;
            pagAlertas.Controls.Add(MsgAlerta);
            pagAlertas.Controls.Add(BtOkAlertas);
            pagAlertas.Controls.Add(TituloALerta);
            pagAlertas.Controls.Add(pictureBox2);
            pagAlertas.Location = new Point(4, 4);
            pagAlertas.Name = "pagAlertas";
            pagAlertas.Padding = new Padding(3);
            pagAlertas.Size = new Size(505, 249);
            pagAlertas.TabIndex = 1;
            pagAlertas.Text = "Alertas";
            // 
            // MsgAlerta
            // 
            MsgAlerta.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MsgAlerta.ForeColor = Color.DarkOrange;
            MsgAlerta.Location = new Point(194, 78);
            MsgAlerta.Name = "MsgAlerta";
            MsgAlerta.Size = new Size(288, 113);
            MsgAlerta.TabIndex = 7;
            MsgAlerta.Text = "Aqui vai o texto referente a mensagem desejada";
            MsgAlerta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtOkAlertas
            // 
            BtOkAlertas.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtOkAlertas.ForeColor = Color.DarkOrange;
            BtOkAlertas.Location = new Point(392, 194);
            BtOkAlertas.Name = "BtOkAlertas";
            BtOkAlertas.Size = new Size(90, 33);
            BtOkAlertas.TabIndex = 6;
            BtOkAlertas.Text = "Ok";
            BtOkAlertas.UseVisualStyleBackColor = true;
            BtOkAlertas.Click += BtOkSucesso_Click;
            // 
            // TituloALerta
            // 
            TituloALerta.Font = new Font("Showcard Gothic", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TituloALerta.ForeColor = Color.DarkOrange;
            TituloALerta.Location = new Point(39, 22);
            TituloALerta.Name = "TituloALerta";
            TituloALerta.Size = new Size(454, 53);
            TituloALerta.TabIndex = 4;
            TituloALerta.Text = "ATENÇÃO!";
            TituloALerta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(11, 65);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(177, 162);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // pagErros
            // 
            pagErros.BackColor = Color.White;
            pagErros.Controls.Add(MsgErro);
            pagErros.Controls.Add(BtOkErros);
            pagErros.Controls.Add(TituloErro);
            pagErros.Controls.Add(pictureBox3);
            pagErros.Location = new Point(4, 4);
            pagErros.Name = "pagErros";
            pagErros.Padding = new Padding(3);
            pagErros.Size = new Size(505, 249);
            pagErros.TabIndex = 2;
            pagErros.Text = "Erros";
            // 
            // MsgErro
            // 
            MsgErro.BackColor = Color.White;
            MsgErro.BorderStyle = BorderStyle.None;
            MsgErro.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MsgErro.ForeColor = Color.Crimson;
            MsgErro.Location = new Point(202, 96);
            MsgErro.Multiline = true;
            MsgErro.Name = "MsgErro";
            MsgErro.ReadOnly = true;
            MsgErro.Size = new Size(280, 92);
            MsgErro.TabIndex = 7;
            MsgErro.Text = "Aqui vai o texto referente a mensagem desejada";
            // 
            // BtOkErros
            // 
            BtOkErros.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtOkErros.ForeColor = Color.Crimson;
            BtOkErros.Location = new Point(392, 194);
            BtOkErros.Name = "BtOkErros";
            BtOkErros.Size = new Size(90, 33);
            BtOkErros.TabIndex = 6;
            BtOkErros.Text = "Ok";
            BtOkErros.UseVisualStyleBackColor = true;
            BtOkErros.Click += BtOkSucesso_Click;
            // 
            // TituloErro
            // 
            TituloErro.Font = new Font("Showcard Gothic", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TituloErro.ForeColor = Color.Crimson;
            TituloErro.Location = new Point(39, 22);
            TituloErro.Name = "TituloErro";
            TituloErro.Size = new Size(454, 53);
            TituloErro.TabIndex = 4;
            TituloErro.Text = "ERRO!";
            TituloErro.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(11, 65);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(177, 162);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // pagConfirmar
            // 
            pagConfirmar.BackColor = Color.White;
            pagConfirmar.Controls.Add(MsgPergunta);
            pagConfirmar.Controls.Add(BtSim);
            pagConfirmar.Controls.Add(BtNao);
            pagConfirmar.Controls.Add(TituloPergunta);
            pagConfirmar.Controls.Add(pictureBox4);
            pagConfirmar.Location = new Point(4, 4);
            pagConfirmar.Name = "pagConfirmar";
            pagConfirmar.Padding = new Padding(3);
            pagConfirmar.Size = new Size(505, 249);
            pagConfirmar.TabIndex = 3;
            pagConfirmar.Text = "Confirmações";
            // 
            // MsgPergunta
            // 
            MsgPergunta.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MsgPergunta.ForeColor = Color.SteelBlue;
            MsgPergunta.Location = new Point(194, 78);
            MsgPergunta.Name = "MsgPergunta";
            MsgPergunta.Size = new Size(288, 113);
            MsgPergunta.TabIndex = 7;
            MsgPergunta.Text = "Aqui vai o texto referente a mensagem desejada";
            MsgPergunta.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtSim
            // 
            BtSim.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtSim.ForeColor = Color.SteelBlue;
            BtSim.Location = new Point(194, 194);
            BtSim.Name = "BtSim";
            BtSim.Size = new Size(90, 33);
            BtSim.TabIndex = 6;
            BtSim.Text = "Sim";
            BtSim.UseVisualStyleBackColor = true;
            BtSim.Click += BtSim_Click;
            // 
            // BtNao
            // 
            BtNao.Font = new Font("Century Gothic", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtNao.ForeColor = Color.SteelBlue;
            BtNao.Location = new Point(392, 194);
            BtNao.Name = "BtNao";
            BtNao.Size = new Size(90, 33);
            BtNao.TabIndex = 6;
            BtNao.Text = "Não";
            BtNao.UseVisualStyleBackColor = true;
            BtNao.Click += BtNao_Click;
            // 
            // TituloPergunta
            // 
            TituloPergunta.Font = new Font("Showcard Gothic", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TituloPergunta.ForeColor = Color.SteelBlue;
            TituloPergunta.Location = new Point(39, 22);
            TituloPergunta.Name = "TituloPergunta";
            TituloPergunta.Size = new Size(454, 53);
            TituloPergunta.TabIndex = 4;
            TituloPergunta.Text = "CONFIRMAÇÃO!";
            TituloPergunta.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(11, 65);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(177, 162);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 5;
            pictureBox4.TabStop = false;
            // 
            // FormMsg
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(497, 239);
            Controls.Add(MpConteudo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormMsg";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += FormMsg_Load;
            MpConteudo.ResumeLayout(false);
            pagSucesso.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pagAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            pagErros.ResumeLayout(false);
            pagErros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            pagConfirmar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button BtOkSucesso;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtOkAlertas;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button BtOkErros;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button BtSim;
        private System.Windows.Forms.Button BtNao;
        private System.Windows.Forms.PictureBox pictureBox4;
        public System.Windows.Forms.Label MsgSucesso;
        public System.Windows.Forms.Label MsgAlerta;
        public System.Windows.Forms.TabControl MpConteudo;
        public System.Windows.Forms.TabPage pagSucesso;
        public System.Windows.Forms.TabPage pagAlertas;
        public System.Windows.Forms.TabPage pagErros;
        public System.Windows.Forms.TabPage pagConfirmar;
        public System.Windows.Forms.Label MsgPergunta;
        public System.Windows.Forms.Label TituloSucesso;
        public System.Windows.Forms.Label TituloALerta;
        public System.Windows.Forms.Label TituloErro;
        public System.Windows.Forms.Label TituloPergunta;
        public TextBox MsgErro;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace GS.Core.UI.Formularios
{
    public partial class FormMsg : Form
    {
        public FormMsg()
        {
            InitializeComponent();
        }

        private void BtOkSucesso_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MpConteudo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void FormMsg_Load(object sender, EventArgs e)
        {
            Funcoes.Resposta = false;
        }

        private void BtSim_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void BtNao_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}

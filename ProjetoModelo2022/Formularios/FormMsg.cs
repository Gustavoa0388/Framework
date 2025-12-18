using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ECTurbo.Codigos;

namespace ECTurbo.Formularios
{
    public partial class FormMsg : Form
    {
        public FormMsg()
        {
            InitializeComponent();
        }

        private void BtOkSucesso_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MpConteudo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void FormMsg_Load(object sender, EventArgs e)
        {
            Funcoes.Resposta = false;
        }

        private void BtSim_Click(object sender, EventArgs e)
        {
            Funcoes.Resposta = true;
            Close();
        }

        private void BtNao_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

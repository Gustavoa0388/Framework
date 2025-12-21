using GS.Core.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS.Core.UI.Demo.Forms
{
    public partial class FrmCadastroTeste : FormBaseCadastro
    {
        public FrmCadastroTeste()
        {
            InitializeComponent(); // ✅ só aqui
        }

        protected override void OnSalvar()
        {
            MessageBox.Show("Salvo com sucesso!");
            Close();
        }
    }
}
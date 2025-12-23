using GS.Core.UI.Controls.Display;
using GS.Core.UI.Controls.Inputs;
using GS.Core.UI.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmCadastroCliente (DEMO)
    /// 
    /// Demonstra o uso correto do FormBaseCadastro
    /// com inputs GS Core e layout local.
    /// </summary>
    public partial class FrmCadastroCliente : FormBaseCadastro
    {
        // =========================
        // CONTROLES
        // =========================

        private Panel pnlConteudo;

        private GsTextBox txtNome;
        private GsMaskedInput txtCpf;
        private GsNumericInput txtIdade;
        private GsDateSelector txtNascimento;

        // =========================
        // CONSTRUTOR
        // =========================

        public FrmCadastroCliente()
        {
            InitializeComponent();
            BuildLayout();
            BuildForm();
        }

        // =========================
        // LAYOUT
        // =========================

        private void BuildLayout()
        {
            pnlConteudo = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            Controls.Add(pnlConteudo);
        }

        // =========================
        // BUILD DO FORMULÁRIO
        // =========================

        private void BuildForm()
        {
            int x = 30;
            int y = 30;
            int gap = 55;

            txtNome = new GsTextBox { Required = true };
            AddLabeledControlLocal("Nome", txtNome, x, y);
            y += gap;

            txtCpf = new GsMaskedInput
            {
                Mask = "000.000.000-00",
                Required = true
            };
            AddLabeledControlLocal("CPF", txtCpf, x, y);
            y += gap;

            txtIdade = new GsNumericInput { Required = true };
            AddLabeledControlLocal("Idade", txtIdade, x, y);
            y += gap;

            txtNascimento = new GsDateSelector { Required = true };
            AddLabeledControlLocal("Nascimento", txtNascimento, x, y);
        }

        // =========================
        // HELPER LOCAL (DEMO)
        // =========================

        private void AddLabeledControlLocal(
            string labelText,
            Control control,
            int x,
            int y)
        {
            int labelWidth = 120;

            var lbl = new GsLabel
            {
                Text = labelText,
                Location = new Point(x, y),
                Width = labelWidth,
                TextAlign = ContentAlignment.MiddleRight,
                TargetControl = control
            };

            control.Location = new Point(x + labelWidth + 10, y);
            control.Width = 220;

            pnlConteudo.Controls.Add(lbl);
            pnlConteudo.Controls.Add(control);
        }

        // =========================
        // CONTRATOS OBRIGATÓRIOS
        // =========================

        protected override void OnInitialize()
        {
            // DEMO: nada a inicializar
        }

        protected override void OnLoadData()
        {
            // DEMO: não carrega dados reais
        }

        protected override void OnSave()
        {
            // DEMO: simula sucesso
            FormMsg.Success("Cliente cadastrado com sucesso!");
        }

        protected override void OnDelete()
        {
            // DEMO: não implementado
        }
    }
}

using GS.Core.UI.Controls;
using GS.Core.UI.Controls.Inputs;
using GS.Core.UI.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GS.Core.UI.Demo.Forms.Pages
{
    /// <summary>
    /// FrmCadastroCliente
    /// 
    /// Formulário de cadastro de cliente (DEMO).
    /// 
    /// Objetivo:
    /// - Demonstrar uso real do FormBaseCadastro
    /// - Validar inputs GS Core em um cenário concreto
    /// - Exercitar validação global e feedback visual
    /// 
    /// Observações:
    /// - Utiliza Windows Forms Designer
    /// - Layout de campos é criado via código (demo)
    /// - Não persiste dados reais
    /// </summary>
    public partial class FrmCadastroCliente : FormBaseCadastro
    {
        // =========================
        // CONTROLES
        // =========================

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
            BuildForm();
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
        // HELPER LOCAL (APENAS DEMO)
        // =========================

        /// <summary>
        /// Adiciona um label alinhado à direita e um controle GS Core.
        /// 
        /// Importante:
        /// - Helper LOCAL do Demo
        /// - Não faz parte do GS Core
        /// - Evita duplicação visual neste formulário
        /// </summary>
        private void AddLabeledControlLocal(
            string labelText,
            Control control,
            int x,
            int y)
        {
            int labelWidth = 120;

            var lbl = new GS.Core.UI.Controls.Display.GsLabel
            {
                Text = labelText,
                Location = new Point(x, y),
                Width = labelWidth,
                TextAlign = ContentAlignment.MiddleRight,
                TargetControl = control
            };

            control.Location = new Point(x + labelWidth + 10, y);
            control.Width = 220;

            // Usa o painel base do FormBaseCadastro
            ContentPanel.Controls.Add(lbl);
            ContentPanel.Controls.Add(control);
        }

        // =========================
        // SALVAR
        // =========================

        protected override bool OnSalvar()
        {
            // DEMO: simula sucesso
            FormMsg.Success("Cliente cadastrado com sucesso!");
            return true;
        }
    }
}

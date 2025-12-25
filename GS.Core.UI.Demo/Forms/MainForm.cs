using GS.Core.UI.Controls.Display;
using GS.Core.UI.Controls.Inputs;
using GS.Core.UI.Controls.Layout;
using GS.Core.UI.Controls.States;
using GS.Core.UI.Demo.Forms.Pages;
using GS.Core.UI.Forms;
using GS.Core.UI.Theming;
using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Forms.Navigation;


namespace GS.Core.UI.Demo
{
    /// <summary>
    /// MainForm
    /// 
    /// Formulário principal do projeto de DEMO do GS Core UI.
    /// 
    /// RESPONSABILIDADES:
    /// - Servir como HUB de testes dos controles GS Core
    /// - Demonstrar inputs, botões, layout e charts
    /// - Permitir navegação para outros formulários de teste (Consulta, Cadastro, etc.)
    /// 
    /// IMPORTANTE:
    /// - Este formulário utiliza WinForms Designer
    /// - Não deve ter sua estrutura alterada nesta fase
    /// - O objetivo aqui é VALIDAÇÃO, não refatoração
    /// </summary>
    public partial class MainForm : GsBaseForm
    {
        // =========================================================
        // CONSTRUTOR
        // =========================================================

        public MainForm()
        {
            InitializeComponent();
           

            // Permite forçar um tema específico no demo (opcional)
            // CustomTheme = GsThemes.Dark;
            // CustomTheme = GsThemes.Light;

            // Cria o menu de navegação dos formulários de teste
            BuildMenu();
            BuildSideMenu(); // 👈 AQUI
            // Métodos existentes do demo (inalterados)
            BuildInputs();
            BuildButtons();
            BuildLayout();
            BuildCharts();
        }

        // =========================================================
        // MENU DE DEMOS (FASE 10)
        // =========================================================

        /// <summary>
        /// Cria o menu superior para navegação entre formulários de teste.
        /// 
        /// DECISÃO DE ARQUITETURA:
        /// - Menu criado por código (não Designer)
        /// - Evita acoplamento desnecessário
        /// - Não interfere no layout existente (TabControl)
        /// 
        /// ESCOPO:
        /// - Exclusivo do projeto Demo
        /// - Não faz parte do GS Core
        /// </summary>
        /// 

        // =========================================================
        // SIDE MENU (DEMO BLOCO 3)
        // =========================================================
        private void BuildSideMenu()
        {
            var sideMenu = new GsSideMenu
            {
                Dock = DockStyle.Left,
                Width = 220
            };

            // ---- Itens do menu ----
            var itemInputs = new GsSideMenuItem { Text = "Inputs" };
            var itemButtons = new GsSideMenuItem { Text = "Botões" };
            var itemLayout = new GsSideMenuItem { Text = "Layout" };
            var itemCharts = new GsSideMenuItem { Text = "Charts" };

            // Seleção inicial correta (via container)
            sideMenu.AddItem(itemInputs);
            sideMenu.AddItem(itemButtons);
            sideMenu.AddItem(itemLayout);
            sideMenu.AddItem(itemCharts);

            // Força seleção inicial SEM simular clique
            sideMenu.ItemSelected += (_, item) =>
            {
                if (item == itemInputs) tabMain.SelectedIndex = 0;
                if (item == itemButtons) tabMain.SelectedIndex = 1;
                if (item == itemLayout) tabMain.SelectedIndex = 2;
                if (item == itemCharts) tabMain.SelectedIndex = 3;
            };

            // Seleciona o primeiro item manualmente
            // (chamando a lógica do container, não do item)
            typeof(GsSideMenu)
                .GetMethod("SelectItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(sideMenu, new object[] { itemInputs });

            Controls.Add(sideMenu);
            sideMenu.SendToBack(); // 👈 importante
            tabMain.Dock = DockStyle.Fill;
        }     
               
        private void BuildMenu()
        {
            // MenuStrip padrão do WinForms
            var menu = new MenuStrip();

            // Menu principal "Demos"
            var menuDemos = new ToolStripMenuItem("Demos");

            // -----------------------------
            // Consulta de Clientes
            // -----------------------------
            var itemConsulta = new ToolStripMenuItem("Consulta de Clientes");
            itemConsulta.Click += (_, _) =>
            {
                // Abre a consulta via infraestrutura GS Core
                var result = GsNavigationService.OpenModal<FrmConsultaCliente>(this);

                // DEMO:
                // Apenas valida o contrato de navegação
                if (result.HasResult)
                {
                    FormMsg.Info(
                        $"Consulta encerrada com resultado: {result.ResultType}"
                    );
                }
            };



            // -----------------------------
            // Cadastro de Clientes
            // -----------------------------
            var itemCadastro = new ToolStripMenuItem("Cadastro de Clientes");
            itemCadastro.Click += (_, _) =>
            {
                using var frm = new FrmCadastroCliente();
                frm.ShowDialog(this);
            };

            // Adiciona itens ao menu "Demos"
            menuDemos.DropDownItems.Add(itemConsulta);
            menuDemos.DropDownItems.Add(itemCadastro);

            // Adiciona menu "Demos" ao MenuStrip
            menu.Items.Add(menuDemos);

            // Adiciona o MenuStrip ao formulário
            Controls.Add(menu);

            // Garante que o menu fique acima do TabControl
            menu.BringToFront();
        }



        // =========================================================
        // CONSTANTES DE LAYOUT (DEMO)
        // =========================================================

        // Posições e espaçamentos usados nos métodos de build
        private const int X = 20;
        private const int Y_START = 20;
        private const int GAP = 60;

        // =========================================================
        // CAMPOS DE CONTROLES (DEMO)
        // =========================================================

        // Inputs usados no demo
        private GsTextBox txt;
        private GsPasswordTextBox pwd;
        private GsMaskedInput mask;
        private GsNumericInput num;
        private GsDateSelector date;

        // Tooltip apenas para explicar os controles no demo
        private readonly ToolTip _tip = new ToolTip();

        // =========================================================
        // TOOLTIP (DEMO)
        // =========================================================

        /// <summary>
        /// Aplica ToolTips explicativos nos controles do demo.
        /// 
        /// OBSERVAÇÃO:
        /// - Apenas para fins didáticos
        /// - Não faz parte do GS Core
        /// </summary>
        private void ApplyTooltips()
        {
            _tip.SetToolTip(txt, "GsTextBox: input padrão com estilo GS Core");
            _tip.SetToolTip(pwd, "GsPasswordTextBox: campo de senha com botão de visibilidade");
            _tip.SetToolTip(mask, "GsMaskedInput: campo com máscara");
            _tip.SetToolTip(num, "GsNumericInput: aceita apenas números");
            _tip.SetToolTip(date, "GsDateSelector: seleção de data");
        }

        // =========================================================
        // HELPER DE LAYOUT (DEMO)
        // =========================================================

        /// <summary>
        /// Helper para adicionar um controle com label associado.
        /// 
        /// DECISÃO:
        /// - Usado apenas no Demo para reduzir repetição
        /// - Pode virar utilitário no futuro (FASE EXTRA)
        /// </summary>
        private void AddLabeledControl(
            Control parent,
            string labelText,
            Control control,
            int x,
            int y,
            int width = 220)
        {
            var lbl = new GsLabel
            {
                Text = labelText,
                Location = new Point(x, y),
                AutoSize = false,
                TargetControl = control,
                TextAlign = ContentAlignment.MiddleRight
            };

            int labelWidth = 120;

            control.Location = new Point(
                x + labelWidth + 8,
                y
            );
            control.Width = width;

            parent.Controls.Add(lbl);
            parent.Controls.Add(control);
        }

        // =========================================================
        // MÉTODOS BUILD (DEMO)
        // =========================================================
        // IMPORTANTE:
        // Os métodos abaixo já existiam no projeto original
        // e NÃO foram alterados nesta fase.
        //
        // - BuildInputs()
        // - BuildButtons()
        // - BuildLayout()
        // - BuildCharts()
        //
        // Eles são responsáveis apenas por demonstrar
        // o uso dos controles GS Core.

        private void BuildInputs()
        {
            var page = tabMain.TabPages[0];
            var title = new GS.Core.UI.Controls.Display.GsTitleLabel
            {
                Text = "Configuração de Conexão",
                TitleLevel = GsTitleLevel.Title,
                Location = new Point(20, 20)
            };

            var subtitle = new GS.Core.UI.Controls.Display.GsTitleLabel
            {
                Text = "Dados do Banco",
                TitleLevel = GsTitleLevel.Subtitle,
                Location = new Point(20, 60)
            };



            page.Controls.Add(title);
            page.Controls.Add(subtitle);


            txt = new GsTextBox
            {
                //Placeholder = "Digite o texto",
                Required = true,
                RequiredMessage = "Campo obrigatório"
            };

            AddLabeledControl(page, "TextBox", txt, 20, 100);


            pwd = new GsPasswordTextBox
            {
                //Placeholder = "Digite a senha",
                Required = true
            };
            AddLabeledControl(page, "Password", pwd, 20, 160);

            mask = new GS.Core.UI.Controls.Inputs.GsMaskedInput
            {
                Mask = "000.000.000-00",
                Required = true
            };
            AddLabeledControl(page, "CPF", mask, 20, 240);

            //num = new GsNumericInput
            //{
            //    Required = true,
            //    MinValue = 1,
            //    MaxValue = 100,
            //    AllowDecimal = false
            //};
            //
            //AddLabeledControl(page, "Quantidade", num, 20, 300);

            num = new GsNumericInput
            {
                AllowDecimal = true,
                DecimalPlaces = 2,
                MinValue = 0
            };

            AddLabeledControl(page, "Quantidade", num, 20, 300);


            date = new GsDateSelector();
            AddLabeledControl(page, "Date Selector", date, 20, 360);

            ApplyTooltips();
        }



        private void BuildButtons()
        {

            var page = tabMain.TabPages[1];

            var btnSalvar = new GsButton
            {
                Text = "Salvar"
            };

            btnSalvar.Click += btnSalvar_Click;

            AddLabeledControl(page, "Primary Button", btnSalvar, 20, 20);

            var toggle = new GsToggleSwitch
            {
                OnText = "Ativo",
                OffText = "Inativo",
                Location = new Point(20, 40)
            };

            page.Controls.Add(toggle);


            AddLabeledControl(page, "Toggle Button", new GsToggleSwitch { Text = "Ativo" }, 20, 150);

            var chk = new GsCheckBox { Text = "Aceito os termos" };
            chk.Location = new Point(20, 200);
            page.Controls.Add(chk);

            var rbA = new GsRadioOption
            {
                Text = "Opção A",
                Location = new Point(20, 40)
            };

            var rbB = new GsRadioOption
            {
                Text = "Opção B",
                Location = new Point(20, 70)
            };           

            page.Controls.Add(rbA);
            page.Controls.Add(rbB);

            var btnTheme = new GsButton
            {
                Text = "Alternar Tema",
                Location = new Point(300, 20),
                Width = 160
            };

            bool dark = false;

            btnTheme.Click += (s, e) =>
            {
                dark = !dark;
                ThemeManager.ApplyTheme(this, dark ? GsThemes.Dark : GsThemes.Light);
            };

            page.Controls.Add(btnTheme);

            var combo = new GS.Core.UI.Controls.Inputs.GsComboBox
            {
                Required = true,
                Placeholder = "Selecione o estado"
            };
            combo.Items.Add("SP");
            combo.Items.Add("RJ");
            combo.Items.Add("MG");

            AddLabeledControl(page, "Estado", combo, 20, 320);


        }


        private void BuildLayout()
        {
            var page = tabMain.TabPages[2];

            var panelLabel = new GsLabel
            {
                Text = "GsPanel",
                Location = new Point(20, 20),
                AutoSize = true
            };

            var panel = new GsPanel
            {
                Location = new Point(20, 45),
                Size = new Size(300, 150)
            };

            var innerLabel = new GsLabel
            {
                Text = "Conteúdo dentro do painel",
                Location = new Point(10, 10)
            };

            panel.Controls.Add(innerLabel);

            page.Controls.Add(panelLabel);
            page.Controls.Add(panel);

            page.Controls.Add(new GsSeparator
            {
                Location = new Point(20, 220),
                Width = 300
            });

            page.Controls.Add(new GsLabel
            {
                Text = "Separadores",
                Location = new Point(20, 200),
                AutoSize = true
            });
        }


        private void BuildCharts()
        {
            var page = tabMain.TabPages[3];

            page.Controls.Add(new GsLabel
            {
                Text = "Chart 1",
                Location = new Point(20, 20),
                AutoSize = true
            });

            page.Controls.Add(new GS.Core.UI.Controls.Data.GsChart1
            {
                Location = new Point(20, 45),
                Size = new Size(300, 180)
            });

            page.Controls.Add(new GsLabel
            {
                Text = "Chart 2",
                Location = new Point(340, 20),
                AutoSize = true
            });

            page.Controls.Add(new GS.Core.UI.Controls.Data.GsChart2
            {
                Location = new Point(340, 45),
                Size = new Size(300, 180)
            });
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                MessageBox.Show(
                    "Existem campos obrigatórios não preenchidos.",
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Aqui entra a lógica real (salvar, conectar, etc.)
        }
    }
}

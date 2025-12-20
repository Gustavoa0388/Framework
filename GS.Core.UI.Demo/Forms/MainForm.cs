using GS.Core.UI.Controls;
using System.Drawing;
using GS.Core.UI.Theming;
using GS.Core.UI.Forms;


namespace GS.Core.UI.Demo

{
    public partial class MainForm : GsBaseForm

    {
        public MainForm()
        {
            InitializeComponent();

            //CustomTheme = GsThemes.Dark; // opcional
            //CustomTheme = GsThemes.Light; // opcional

            BuildInputs();
            BuildButtons();
            BuildLayout();
            BuildCharts();
        }

        
        private const int X = 20;
        private const int Y_START = 20;
        private const int GAP = 60;
        private GsTextBox txt;
        private GsPasswordTextBox pwd;
        private GsMaskedTextBox mask;
        private GsNumericBox num;
        private GsDateSelector date;

        private readonly ToolTip _tip = new ToolTip();

        private void ApplyTooltips()
        {
            _tip.SetToolTip(txt, "GsTextBox: input padrão com estilo GS");
            _tip.SetToolTip(pwd, "GsPasswordTextBox: senha com botão de visibilidade");
            _tip.SetToolTip(mask, "GsMaskedTextBox: campo com máscara");
            _tip.SetToolTip(num, "GsNumericBox: aceita apenas números");
            _tip.SetToolTip(date, "GsDateSelector: seleção de data");
        }


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
                AutoSize = true
            };

            control.Location = new Point(x, y + 22);
            control.Width = width;

            parent.Controls.Add(lbl);
            parent.Controls.Add(control);
        }



        private void BuildInputs()
        {
            var page = tabMain.TabPages[0];
            var title = new GsTitleLabel
            {
                Text = "Configuração de Conexão",
                TitleSize = GsTitleSize.Title,
                Location = new Point(20, 20)
            };

            var subtitle = new GsTitleLabel
            {
                Text = "Dados do Banco",
                TitleSize = GsTitleSize.Subtitle,
                Location = new Point(20, 60)
            };

            

            page.Controls.Add(title);
            page.Controls.Add(subtitle);


            //var hint = new GsHintLabel
            // {
            //    Text = "Informe o endereço do servidor MySQL",
            //Location = new Point(20, 120)
            // };

            // page.Controls.Add(hint);

            txt = new GsTextBox
            {
                Placeholder = "Digite o texto",
                Required = true,
                RequiredMessage = "Campo obrigatório"
            };

            AddLabeledControl(page, "TextBox", txt, 20, 100);


            pwd = new GsPasswordTextBox
            {
                Placeholder = "Digite a senha",
                Required = true
            };
            AddLabeledControl(page, "Password", pwd, 20, 160);
                       
            mask = new GsMaskedTextBox
            {
                Mask = "000.000.000-00"
            };
            AddLabeledControl(page, "Masked Text", mask, 20, 240);

            num = new GsNumericBox();
            AddLabeledControl(page, "Numeric", num, 20, 300);

            date = new GsDateSelector();
            AddLabeledControl(page, "Date Selector", date, 20, 360);

            ApplyTooltips();
        }



        private void BuildButtons()
        {

            var page = tabMain.TabPages[1];

            AddLabeledControl(page, "Primary Button", new GsButton { Text = "Salvar" }, 20, 20);
            AddLabeledControl(page, "Toggle Button", new GsToggleButton { Text = "Ativo" }, 20, 80);

            var chk = new GsCheckBox { Text = "Aceito os termos" };
            chk.Location = new Point(20, 150);
            page.Controls.Add(chk);

            var radioA = new GsRadioButton { Text = "Opção A", Location = new Point(20, 190) };
            var radioB = new GsRadioButton { Text = "Opção B", Location = new Point(20, 220) };

            page.Controls.Add(radioA);
            page.Controls.Add(radioB);

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

            page.Controls.Add(new GsSeparatorHorizontal
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

            page.Controls.Add(new GsChart1
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

            page.Controls.Add(new GsChart2
            {
                Location = new Point(340, 45),
                Size = new Size(300, 180)
            });
        }    
              
    }
}

// LEGACY CONTROL
// Originado do ECTurbo
// Mantido apenas para compatibilidade
// NÃO segue o padrão GS Core

using System.ComponentModel;
using GS.Core.UI.Utils.Legacy;


namespace GS.Core.UI.Controls.Legacy
{
    [ToolboxItem(false)]
    public class GsMaskedTextBox : MaskedTextBox
    {
        public GsMaskedTextBox()
        {
            Tag = "";
        }

        private bool vLimpeza = true;
        [DisplayName("_Limpeza Automática")]
        public bool Limpeza
        {
            get { return vLimpeza; }
            set
            {

                vLimpeza = value;

                if (value == true)
                    Tag = Tag.ToString().Replace("|nao_limpar", "");
                else
                    Tag += "|nao_limpar";

            }
        }

        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set
            {

                vColuna = value;

                string valor = FuncoesLegacy.PegarTag(this, "col");

                Tag = Tag.ToString().Replace("col=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag = "col=" + value + Tag.ToString();

            }
        }

        private bool vObgt = false;
        [DisplayName("(DB.2 Campo Obrigatório)")]
        public bool Obgt
        {
            get { return vObgt; }
            set
            {

                vObgt = value;

                if (value == false)
                    Tag = Tag.ToString().Replace("|obgt", "");
                else
                    Tag += "|obgt";
            }
        }

        private string vUnico = "";
        [DisplayName("(DB.3 Msg para Duplicidade)")]
        public string Unico
        {
            get { return vUnico; }
            set
            {

                vUnico = value;

                string valor = FuncoesLegacy.PegarTag(this, "unico");

                Tag = Tag.ToString().Replace("|unico=" + valor, "");

                if (string.IsNullOrEmpty(value) == false)
                    Tag += "|unico=" + value;

            }
        }

        private bool vSalvarMascara = true;

        [DisplayName("_Salvar Mascara")]
        [Description("XXX")]
        [Category("_ECTurbo")]
        public bool SalvarMascara
        {
            get { return vSalvarMascara; }
            set { vSalvarMascara = value; }
        }


        protected override void OnTextChanged(EventArgs e)
        {
            FuncoesLegacy.RemoverLabel(this);

            base.OnTextChanged(e);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            //ForeColor = Config.CorPrimaria;
            //Font = Config.FontePadrao;

            if(SalvarMascara== true)
                TextMaskFormat = MaskFormat.IncludeLiterals;
            else
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            BackColor = Config.CorEntrada;
        }


        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            BackColor = Config.CorSaida;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }

        }


        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            FuncoesLegacy.RemoverLabel(this);

            TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (Text == string.Empty)
                return;

            int t = 0;

            foreach (char c in Mask)
            {
                if (c == '0' || c == 'L' || c == 'A')
                    t++;
            }

            if (Text.Replace(" ", "").Length != t)
            {
                FuncoesLegacy.CriarLabel(this, "Incompleto", descricao:"Por favor digite a informação por completo");
                e.Cancel = true;
                return;
            }

            if (SalvarMascara == true)
                TextMaskFormat = MaskFormat.IncludeLiterals;

        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (Text == string.Empty)
            {
                FuncoesLegacy.RemoverLabel(this);
                return;
            }

            int t = 0;

            foreach (char c in Mask)
            {
                if (c == '0' || c == 'L' || c == 'A')
                    t++;
            }

            if (Text.Replace(" ", "").Length == t)
            {
                FuncoesLegacy.RemoverLabel(this);
            }

        }

    }
}

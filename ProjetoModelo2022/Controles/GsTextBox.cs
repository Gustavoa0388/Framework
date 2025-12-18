using System.ComponentModel;

using ECTurbo.Codigos;

namespace GS.Core.UI.Controls
{
    public class GsTextBox : TextBox
    { 

        public GsTextBox()
        {
            Tag = "";
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            //ForeColor = Config.CorPrimaria;
            //Font = Config.FontePadrao;

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

            if (e.KeyCode == Keys.Escape)
            {
                Text = string.Empty;
                e.SuppressKeyPress = true;
            }

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            //if (Text == string.Empty)
            Funcoes.RemoverLabel(this);

        }


        private string vColuna = "";
        [DisplayName("(DB.1 Coluna Tabela)")]
        public string Coluna
        {
            get { return vColuna; }
            set { 
                
                vColuna = value;

                string valor = Funcoes.PegarTag(this, "col");

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
            set { 
                
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
            set { 
                
                vUnico = value;

                string valor = Funcoes.PegarTag(this, "unico");

                Tag = Tag.ToString().Replace("|unico=" + valor, "");

                if(string.IsNullOrEmpty(value)==false)
                    Tag += "|unico=" + value;
            
            }
        }

        private bool vLimpeza = true;
        [DisplayName("_Limpeza Automática")]
        public bool Limpeza
        {
            get { return vLimpeza; }
            set { 
                
                vLimpeza = value;

                if (value == true)
                    Tag = Tag.ToString().Replace("|nao_limpar", "");
                else
                    Tag += "|nao_limpar";

            }
        }


    }
}
// LEGACY CONTROL
// Originado do ECTurbo
// Mantido apenas para compatibilidade
// NÃO segue o padrão GS Core

using System.ComponentModel;



namespace GS.Core.UI.Controls.Legacy
{
    [ToolboxItem(false)]
    public class GsDateSelectorLegacy : GsTextBox
    {
        public GsDateSelectorLegacy() {
            
            Tag = "|data";
        }



        private bool vDataAtual = false;
        [DisplayName("_Iniciar com Data Atual")]
        [Description("Se ativado o controle já será iniciado com a data atual")]
        [Category("_ECTurbo")]
        public bool DataAtual
        {
            get { return vDataAtual; }
            set { 
            
                vDataAtual = value;

                if (value == false)
                    Tag = Tag.ToString().Replace("|data_atual", "");
                else
                    Tag += "|data_atual";



            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (InnerTextBox is TextBox tb)
            {
                tb.TextAlign = HorizontalAlignment.Center;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            FuncoesLegacy.RemoverLabel(this);

            if (Text == string.Empty)
                return;

            try
            {
                Text = Convert.ToDateTime(Text).ToShortDateString();
            }
            catch (Exception)
            {
                FuncoesLegacy.CriarLabel(this, "Data inválida");
                e.Cancel = true;
                return;
            }

            string[] q = Text.Split('/');

            if(Convert.ToInt32(q[2]) < 1582)
            {
                FuncoesLegacy.CriarLabel(this, "Ano inválido", descricao: "O Ano aceito deve ser acima de 1582");
                InnerTextBox.SelectionStart = InnerTextBox.Text.Length;
                e.Cancel = true;
            }

            base.OnValidating(e);

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);


            if (InnerTextBox.ReadOnly)
                return;

            //Liberar backspace
            if (e.KeyChar == (char)Keys.Back)
                return;

            if (InnerTextBox.SelectionLength == Text.Length)
                Text = string.Empty;

            int i = InnerTextBox.SelectionStart;

            int t = Text.Length;


            //Bloquear quando a data estiver completa
            if (t == 10) goto Continuar;

            string[] q = Text.Split('/');
            try
            {
                if (Convert.ToInt32(q[0]) > 31)
                    goto Continuar;

                if (Convert.ToInt32(q[1]) > 12)
                    goto Continuar;

            }
            catch (Exception) { }



            //Melhorias ao digitar a barra de forma manual
            if (e.KeyChar.ToString() == "/")
            {

                if (t == 0) goto Continuar;

                if (Text == "0") goto Continuar;

                if (t == 1)
                {
                    Text = "0" + Text + "/";
                }
                else if (t == 2 || t == 5)
                {
                    Text += "/";
                }
                else if (t == 4)
                {
                    Text = q[0] + "/0" + q[1] + "/";
                }

                InnerTextBox.SelectionStart = InnerTextBox.Text.Length;

            }


            //Bloquear caracteres nao numericos
            if (char.IsDigit(e.KeyChar) == false) goto Continuar;

            if (t == 2 || t == 5)
            {
                Text += "/";

                Text = Text.Insert(i+1, e.KeyChar.ToString());
                

                InnerTextBox.SelectionStart = InnerTextBox.Text.Length;

                e.Handled = true;

                return;
            }

            //InnerTextBox.SelectionStart = InnerTextBox.Text.Length;h;

            Text = Text.Insert(i, e.KeyChar.ToString());

            InnerTextBox.SelectionStart = i + 1;

            e.Handled = true;

            return;
        Continuar:

            e.Handled = true;

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (Text == string.Empty)
                return;

            string[] q = Text.Split('/');


            try
            {
                FuncoesLegacy.RemoverLabel(this);

                if (Convert.ToInt32(q[0]) > 31)
                {
                    FuncoesLegacy.CriarLabel(this, "Dia inválido", "alerta", descricao:"O dia válido deve ser menor ou igual a  31");
                }
                else if (Convert.ToInt32(q[1]) > 12)
                {
                    FuncoesLegacy.CriarLabel(this, "Mês inválido", "alerta");
                }
            }
            catch (Exception) { }
        }

    }
}

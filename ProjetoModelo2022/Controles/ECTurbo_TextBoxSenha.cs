using ProjetoModelo2022.Properties;

namespace ECTurbo.Controles
{
    public class ECTurbo_TextBoxSenha : ECTurbo_TextBox
    {
        private PictureBox BtVerSenha;

        public ECTurbo_TextBoxSenha()
        {
            UseSystemPasswordChar = true;
            Layout += ECTurbo_TextBoxSenha_Layout;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CriarBotao();
        }

        private void CriarBotao()
        {
            if (BtVerSenha == null)
            {
                BtVerSenha = new PictureBox
                {
                    Cursor = Cursors.Hand,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Image = Resources.icone_senha_mostrar,
                    BackColor = Color.Transparent
                };

                BtVerSenha.MouseDown += BtVerSenhaMouseDown;
                BtVerSenha.MouseUp += BtVerSenhaMouseUp;

                Parent.Controls.Add(BtVerSenha);
                BtVerSenha.BringToFront();

                AtualizarPosicaoBotao();
            }
        }

        private void ECTurbo_TextBoxSenha_Layout(object sender, EventArgs e)
        {
            AtualizarPosicaoBotao();
        }

        private void AtualizarPosicaoBotao()
        {
            if (BtVerSenha != null)
            {
                BtVerSenha.Top = Top + ((Height - BtVerSenha.Height) / 2);
                BtVerSenha.Left = Right + 5;
            }
        }

        private void BtVerSenhaMouseUp(object sender, MouseEventArgs e)
        {
            BtVerSenha.Image = Resources.icone_senha_mostrar;
            UseSystemPasswordChar = true;
        }

        private void BtVerSenhaMouseDown(object sender, MouseEventArgs e)
        {
            BtVerSenha.Image = Resources.icone_senha_ocultar;
            UseSystemPasswordChar = false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (BtVerSenha != null)
            {
                BtVerSenha.MouseDown -= BtVerSenhaMouseDown;
                BtVerSenha.MouseUp -= BtVerSenhaMouseUp;
                BtVerSenha.Dispose();
            }
        }

    }
}

// ⚠️ ARQUIVO LEGADO (ECTurbo)
// NÃO USAR EM CÓDIGO NOVO
// Manter apenas para compatibilidade
using GS.Core.UI.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GS.Core.UI.Utils.Legacy
{
    /// <summary>
    /// Funções LEGADAS do ECTurbo.
    /// Mantidas apenas para compatibilidade com controles antigos.
    ///
    /// ⚠️ NÃO depende de FormMsg
    /// ⚠️ NÃO cria Forms customizados
    /// ⚠️ NÃO acessa Designer
    /// </summary>
    public static class FuncoesLegacy
    {
        // =========================================================
        // MENSAGENS (LEGACY → MessageBox)
        // =========================================================
        public static string BaseApp = AppDomain.CurrentDomain.BaseDirectory;
        public static void MsgOk(string mensagem)
        {
            MessageBox.Show(mensagem, "Sucesso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MsgAlerta(string mensagem)
        {
            MessageBox.Show(mensagem, "Atenção",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void MsgErro(string mensagem)
        {
            MessageBox.Show(mensagem, "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Pergunta(string mensagem)
        {
            return MessageBox.Show(mensagem, "Confirmação",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // =========================================================
        // FUNÇÕES UTILIZADAS POR CONTROLES LEGADOS
        // =========================================================

        public static Color CorTransparente(Color cor, int alpha)
        {
            return Color.FromArgb(alpha, cor);
        }

        public static string PegarTag(Control ctrl, string chave)
        {
            if (ctrl?.Tag == null)
                return string.Empty;

            var tag = ctrl.Tag.ToString();
            var partes = tag.Split('|');

            foreach (var p in partes)
            {
                if (p.StartsWith(chave))
                    return p;
            }

            return string.Empty;
        }
        public static GraphicsPath CriarPath(RectangleF Base,
                                            float Raio = 1,
                                            float rES = 0,
                                            float rDS = 0,
                                            float rDI = 0,
                                            float rEI = 0)
        {
            if (Raio < 1)
                Raio = 1;

            if (Raio > Base.Width)
                Raio = Base.Width;

            if (Raio > Base.Height)
                Raio = Base.Height;

            if (rES < 1)
                rES = Raio;
            else
            {
                if (rES > Base.Width)
                    rES = Base.Width;

                if (rES > Base.Height)
                    rES = Base.Height;
            }


            if (rDS < 1)
                rDS = Raio;
            else
            {
                if (rDS > Base.Width)
                    rDS = Base.Width;

                if (rDS > Base.Height)
                    rDS = Base.Height;
            }

            if (rDI < 1)
                rDI = Raio;
            else
            {
                if (rDI > Base.Width)
                    rDI = Base.Width;

                if (rDI > Base.Height)
                    rDI = Base.Height;
            }

            if (rEI < 1)
                rEI = Raio;
            else
            {
                if (rEI > Base.Width)
                    rEI = Base.Width;

                if (rEI > Base.Height)
                    rEI = Base.Height;
            }


            RectangleF RectRaio = new RectangleF()
            {
                X = Base.X,
                Y = Base.Y,
                Width = Raio,
                Height = Raio
            };

            GraphicsPath path = new GraphicsPath();

            //Arco superior esquerda
            RectRaio.Width = rES;
            RectRaio.Height = rES;
            path.AddArc(RectRaio, 180, 90);


            //Arco superior Direita
            RectRaio.Width = rDS;
            RectRaio.Height = rDS;
            RectRaio.X = Base.Width + Base.X - RectRaio.Width;
            path.AddArc(RectRaio, 270, 90);


            //Arco inferior direita
            RectRaio.Width = rDI;
            RectRaio.Height = rDI;
            RectRaio.X = Base.Width + Base.X - RectRaio.Width;
            RectRaio.Y = Base.Height + Base.Y - RectRaio.Height;
            path.AddArc(RectRaio, 0, 90);


            //Arco inferior esquerda
            RectRaio.Width = rEI;
            RectRaio.Height = rEI;
            RectRaio.X = Base.X;
            RectRaio.Y = Base.Height + Base.Y - RectRaio.Height;
            path.AddArc(RectRaio, 90, 90);

            path.CloseFigure();

            return path;
        }

        public static string NormalizarNumero(string Valor)
        {
            return Regex.Replace(Valor, "[^0-9,-]", string.Empty);
        }
        public static bool CompararImagens(Image img1, Image img2)
        {
            // Verificar se as imagens são nulas
            if (img1 == null || img2 == null)
                return false;

            // Verificar se as imagens têm o mesmo tamanho
            if (img1.Width != img2.Width || img1.Height != img2.Height)
                return false;

            // Converter as imagens para Bitmap para acessar os pixels
            Bitmap bmp1 = new Bitmap(img1);
            Bitmap bmp2 = new Bitmap(img2);

            // Comparar pixel a pixel
            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        return false; // Os pixels são diferentes
                    }
                }
            }

            // Se todos os pixels são iguais, as imagens são iguais
            return true;
        }

        public static string MiniaturaImagem(PictureBox PictureBox,
                                           string SalvarEm = "",
                                           int Altura = 0,
                                           int Largura = 0,
                                           string Nome = "Miniatura",
                                           string Extensao = "jpg",
                                           bool FundoBranco = false)
        {
            if (SalvarEm == "")
                SalvarEm = BaseApp;

            if (Altura == 0)
            {
                Altura = PictureBox.Height;
                Largura = PictureBox.Width;
            }

            using (Bitmap bmp = new Bitmap(Largura, Altura))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    if (FundoBranco == true)
                        g.Clear(Color.White);

                    g.DrawImage(PictureBox.Image, 0, 0, Largura, Altura);
                }

                bmp.Save(SalvarEm + "\\" + Nome + "." + Extensao);

                return SalvarEm + "\\" + Nome + "." + Extensao;
            }
        }


        public static bool LimparControles(Control Obj, bool PerguntarAntes = true, string Msg = "")
        {
            if (PerguntarAntes == true)
            {
                if (Msg == "")
                    Msg = "Os dados não salvos serão descartados, confirmar?";

                if (Pergunta(Msg) == false)
                    return false;
            }


            foreach (Control Ctr in Obj.Controls)
            {
                if (Ctr.Controls.Count > 0)
                    LimparControles(Ctr, false);

                RemoverLabel(Ctr);

                if (Ctr.Tag != null)
                {
                    string tag = Ctr.Tag.ToString().ToLower();

                    if (tag.Contains("nao_limpar"))
                        continue;

                    if (tag.Contains("data_atual"))
                    {
                        Ctr.Text = DateTime.Today.ToShortDateString();
                        continue;
                    }

                    if (Ctr is PictureBox pc)
                    {
                        if (tag.Contains("|padrao"))
                        {
                            string ft = PegarTag(pc, "padrao");
                            Image FotoPadrao = (Image)Resources.ResourceManager.GetObject(ft);

                            pc.Image = FotoPadrao;
                        }

                        continue;
                    }

                    if (Ctr is CheckBox ckb)
                    {
                        if (tag.Contains("valor_padrao"))
                            ckb.Checked = true;
                        else
                            ckb.Checked = false;

                        continue;
                    }

                    if (Ctr is RadioButton rbb)
                    {
                        if (tag.Contains("valor_padrao"))
                            rbb.Checked = true;
                        else
                            rbb.Checked = false;

                        continue;
                    }

                }

                if (Ctr is TextBox || Ctr is ComboBox || Ctr is MaskedTextBox)
                    Ctr.Text = string.Empty;

                if (Ctr is ComboBox cb)
                    cb.SelectedIndex = -1;

                if (Ctr is CheckBox ck)
                    ck.Checked = false;

                if (Ctr is RadioButton rb)
                    rb.Checked = false;
            }

            return true;
        }
        public static void PegarFotoBanco(PictureBox Obj, object FotoBanco)
        {
            if (FotoBanco == DBNull.Value || FotoBanco == null)
            {
                string ft = PegarTag(Obj, "padrao");
                Image FotoPadrao = (Image)Resources.ResourceManager.GetObject(ft);

                Obj.Image = FotoPadrao;
            }
            else
            {
                byte[] imageData = (byte[])FotoBanco;

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Obj.Image = Image.FromStream(ms);
                }
            }
        }

        public static string PegarEstadoUF(string UF)
        {
            if (EstadosUFs.TryGetValue(UF, out string Estado))
                return Estado;
            else
                return "";
        }

        public static string PegarUFEstado(string Estado)
        {
            var uf = EstadosUFs.FirstOrDefault(e => e.Value.Equals(Estado, StringComparison.OrdinalIgnoreCase)).Key;

            if (string.IsNullOrEmpty(uf))
                return "";
            else
                return uf;
        }
        public static Dictionary<string, string> EstadosUFs = new Dictionary<string, string>
        {
            { "AC", "Acre" },
            { "AL", "Alagoas" },
            { "AM", "Amazonas" },
            { "AP", "Amapá" },
            { "BA", "Bahia" },
            { "CE", "Ceará" },
            { "DF", "Distrito Federal" },
            { "ES", "Espírito Santo" },
            { "GO", "Goiás" },
            { "MA", "Maranhão" },
            { "MG", "Minas Gerais" },
            { "MS", "Mato Grosso do Sul" },
            { "MT", "Mato Grosso" },
            { "PA", "Pará" },
            { "PB", "Paraíba" },
            { "PE", "Pernambuco" },
            { "PI", "Piauí" },
            { "PR", "Paraná" },
            { "RJ", "Rio de Janeiro" },
            { "RN", "Rio Grande do Norte" },
            { "RO", "Rondônia" },
            { "RR", "Roraima" },
            { "RS", "Rio Grande do Sul" },
            { "SC", "Santa Catarina" },
            { "SE", "Sergipe" },
            { "SP", "São Paulo" },
            { "TO", "Tocantins" }
        };


        // ⚠️ PLACEHOLDERS PARA NÃO QUEBRAR CONTROLES
        // Esses métodos existiam no ECTurbo, mas agora são NO-OP

        public static void CriarLabel(Control ctrl, string texto, string tipo = null, string descricao = null)
        {
            // LEGACY: não faz nada no GS Core
        }

        public static void RemoverLabel(Control ctrl)
        {
            // LEGACY: não faz nada no GS Core
        }
    }
}

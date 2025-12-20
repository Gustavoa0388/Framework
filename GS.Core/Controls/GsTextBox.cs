using System.Drawing;
using System.Windows.Forms;
using GS.Core.UI.Controls.Base;

namespace GS.Core.UI.Controls
{
    /// <summary>
    /// TextBox padrão GS.
    /// Responsável por desenhar o ícone de erro interno
    /// e reservar espaço para ele corretamente.
    /// </summary>
    public class GsTextBox : GsInputBase
    {
        // Tamanho do ícone de erro
        private const int ErrorIconSize = 14;

        // Espaço entre texto e ícone
        private const int ErrorIconPadding = 6;

        // ======================================================
        // CRIAÇÃO DO TEXTBOX INTERNO
        // ======================================================
        protected override TextBox CreateInnerTextBox()
        {
            return new TextBox();
        }

        // ======================================================
        // AJUSTE DE PADDING QUANDO EXISTE ERRO
        // ======================================================
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (InnerTextBox == null)
                return;

            // Se houver erro, reserva espaço à direita
            if (HasError)
            {
                InnerTextBox.Padding = new Padding(
                    InnerTextBox.Padding.Left,
                    InnerTextBox.Padding.Top,
                    ErrorIconSize + ErrorIconPadding,
                    InnerTextBox.Padding.Bottom
                );
            }
            else
            {
                // Remove o padding extra quando o erro some
                InnerTextBox.Padding = new Padding(
                    InnerTextBox.Padding.Left,
                    InnerTextBox.Padding.Top,
                    0,
                    InnerTextBox.Padding.Bottom
                );
            }
        }

        // ======================================================
        // DESENHO DO ÍCONE DE ERRO
        // ======================================================
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!HasError)
                return;

            Graphics g = e.Graphics;

            int x = Width - Padding.Right - ErrorIconSize - ErrorIconPadding;
            int y = (Height - ErrorIconSize) / 2;

            g.DrawImage(
                Properties.Resources.error,
                new Rectangle(x, y, ErrorIconSize, ErrorIconSize)
            );
        }
    }
}

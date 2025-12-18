using System.Data;
using System.Reflection;

namespace ECTurbo.Codigos
{
    public class GRID
    {
        private static Color CorMovimento = Color.Lavender;

        public static int CalcularTotalPaginas(DataTable Dt, int QtdLinhasGrid)
        {
            return (int)Math.Ceiling((decimal)Dt.Rows.Count / QtdLinhasGrid);
        }
        public static void MontarGrid(FlowLayoutPanel AreaGrid, 
                                      UserControl LinModelo, 
                                      int Qtd,
                                      Color CorPrincipal = default,
                                      Color CorAlternativa = default,
                                      Color CorMov = default,
                                      bool Ver = true)
        {

            if (CorPrincipal == default)
                CorPrincipal = Color.White;

            if (CorAlternativa == default)
                CorAlternativa = Color.AliceBlue;

            if (CorMov != default)
                CorMovimento = CorMov;

            AreaGrid.Controls.Clear();

            for (int i = 1; i <= Qtd; i++)
            {
                UserControl lst = (UserControl)Activator.CreateInstance(LinModelo.GetType());

                lst.Name = "lst" + i;

                if (i % 2 == 0)
                    lst.BackColor = CorAlternativa;
                else
                    lst.BackColor = CorPrincipal;

                lst.Tag = lst.BackColor;

                lst.MouseEnter += Lst_MouseEnter;
                lst.MouseLeave += Lst_MouseLeave;

                lst.Visible = Ver;

                AreaGrid.Controls.Add(lst);
            }
        }

        private static void Lst_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is UserControl lst)
            {
                Point mouseP = lst.PointToClient(Control.MousePosition);

                if(lst.ClientRectangle.Contains(mouseP))
                    return;

                lst.BackColor = (Color)lst.Tag;

            }
        }

        private static void Lst_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is UserControl lst)
            {
                foreach (UserControl lin in lst.Parent.Controls)
                {
                    lin.BackColor = (Color)lin.Tag;
                }

                lst.BackColor = CorMovimento;
            }
        }

        public static void Colorir(UserControl LinModelo, Color Cor)
        {
            foreach (Control Ctr in LinModelo.Controls)
            {
                if (Ctr.Tag == null)
                    Ctr.ForeColor = Cor;
                else
                {
                    if (Ctr.Tag.ToString().Contains("nao_colorir") == false)
                        Ctr.ForeColor = Cor;
                }
            }
        }


        public static void ExecutarFuncao(Control ctr, string nomeFuncao, object[]? argumentos = null)
        {
            Control? controleAtual = ctr;

            if (argumentos == null)
            {
                argumentos = [0];
                argumentos[0] = ctr;
            }

            while (controleAtual != null && !(controleAtual is Form))
            {
                controleAtual = controleAtual.Parent;
            }

            if (controleAtual is Form formularioAtual)
            {
                MethodInfo? metodo = formularioAtual.GetType().GetMethod(nomeFuncao, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                metodo?.Invoke(formularioAtual, argumentos);
            }
        }

    }
}

using GS.Core.UI.Forms;

public partial class FrmCadastroCliente : FormBaseCadastro
{
    public FrmCadastroCliente()
    {
        InitializeComponent();
    }

    protected override bool OnSalvar()
    {
        // lógica de salvar cliente
        return true;
    }
}

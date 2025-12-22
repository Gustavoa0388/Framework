using GS.Core.UI.Utils.Legacy;


namespace GS.Core.UI.Services.Api

{
    public class BuscaCEP
    {
        public string Logradouro = "";
        public string Complemento = "";
        public string Unidade = "";
        public string Bairro = "";
        public string Localidade = "";
        public string UF = "";
        public string IBGE = "";
        public string GIA = "";
        public string DDD = "";
        public string SIAFI = "";
        public string Estado = "";

        public void Buscar(string Cep)
        {
            //Validar quantidade de caracteres 8
            Cep = Cep.Replace(".", "")
                     .Replace(",", "")
                     .Replace("-", "")
                     .Replace(" ", "");

            if (Cep.Length == 0)
                return;

            if(Cep.Length != 8)
            {
                FuncoesLegacy.MsgErro("O CEP informado deve ter 8 números");
                return;
            }

            string url = $"https://viacep.com.br/ws/{Cep}/json/";

            dynamic Retorno = APIs.Requisicao(url);

            if(Retorno == null)
            {
                FuncoesLegacy.MsgErro("Erro no site ou na sua conexão");
                return;
            }

            if(Retorno.erro != null)
            {
                FuncoesLegacy.MsgErro("O CEP informado não existe");
                return;
            }

            //Captura dos dados
            Logradouro = Retorno.logradouro.ToString();
            Complemento = Retorno.complemento.ToString();
            Unidade = Retorno.unidade.ToString();
            Bairro = Retorno.bairro.ToString();
            Localidade= Retorno.localidade.ToString();
            UF = Retorno.uf.ToString();
            IBGE = Retorno.ibge.ToString();
            DDD = Retorno.ddd.ToString();
            GIA = Retorno.gia.ToString();
            SIAFI = Retorno.siafi.ToString();

            Estado = FuncoesLegacy.PegarEstadoUF(UF);
        }
    }
}

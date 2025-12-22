namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Indica que o controle possui conceito de obrigatoriedade.
    /// </summary>
    public interface IGsRequiredAware
    {
        /// <summary>
        /// Indica se o campo é obrigatório.
        /// 
        /// REGRAS:
        /// - Quando Required == true, a validação deve falhar
        ///   se o valor estiver vazio.
        /// - A regra visual (asterisco, cor, hint) é responsabilidade do controle.
        /// </summary>
        bool Required { get; set; }
    }
}

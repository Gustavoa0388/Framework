namespace GS.Core.UI.Controls
{
    /// <summary>
    /// Contrato que indica que um controle possui
    /// o conceito de obrigatoriedade (Required).
    ///
    /// OBJETIVO:
    /// Permitir que inputs e outros controles de entrada
    /// informem se um valor é obrigatório, de forma padronizada
    /// e previsível para o framework.
    ///
    /// ESTE CONTRATO:
    /// - NÃO executa validação sozinho
    /// - NÃO define regra de negócio
    /// - NÃO define comportamento visual
    ///
    /// ELE APENAS EXPÕE O CONCEITO DE "OBRIGATÓRIO".
    ///
    /// A validação efetiva normalmente é feita em conjunto
    /// com IGsValidatable.
    /// </summary>
    public interface IGsRequiredAware
    {
        /// <summary>
        /// Indica se o campo é obrigatório.
        ///
        /// REGRAS FUNCIONAIS:
        /// - Quando Required == true:
        ///   - O valor vazio deve ser considerado inválido
        /// - Quando Required == false:
        ///   - O campo pode ficar vazio sem erro
        ///
        /// RESPONSABILIDADE DO CONTROLE:
        /// - Decidir COMO o Required é apresentado visualmente
        ///   (asterisco, borda, hint, ícone, etc.)
        ///
        /// RESPONSABILIDADE DO FORMULÁRIO:
        /// - Acionar validações em lote
        /// - Decidir quando impedir salvamento
        ///
        /// OBS:
        /// Required NÃO substitui validação de formato,
        /// apenas valida presença de valor.
        /// </summary>
        bool Required { get; set; }
    }
}

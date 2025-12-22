namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Contrato para controles que possuem validação.
    /// </summary>
    public interface IGsValidatable
    {
        /// <summary>
        /// Indica se o controle está válido após a última validação.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Mensagem de erro associada à última validação.
        /// Deve ser nula ou vazia quando IsValid == true.
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Executa a validação do controle.
        /// 
        /// REGRAS:
        /// - Deve atualizar IsValid e ErrorMessage.
        /// - Não deve lançar exceções.
        /// - Não deve exibir MessageBox.
        /// - A responsabilidade visual é do controle.
        /// </summary>
        void Validate();
    }
}

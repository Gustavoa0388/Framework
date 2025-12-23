namespace GS.Core.UI.Controls.Base
{
    /// <summary>
    /// Contrato para controles que possuem validação própria.
    ///
    /// OBJETIVO:
    /// Padronizar como inputs e controles interativos
    /// expõem seu estado de validação para formulários,
    /// containers e fluxos de UX.
    ///
    /// ESTE CONTRATO DEFINE:
    /// - Se o controle está válido ou inválido
    /// - Qual a mensagem de erro associada
    /// - Como a validação deve ser disparada
    ///
    /// ESTE CONTRATO **NÃO DEFINE**:
    /// - Regras de negócio
    /// - Persistência de dados
    /// - Exibição de mensagens globais (MessageBox, Toast, etc.)
    ///
    /// IMPORTANTE:
    /// A validação aqui é SEMPRE:
    /// - Local
    /// - Visual
    /// - Não intrusiva
    /// </summary>
    public interface IGsValidatable
    {
        /// <summary>
        /// Indica se o controle está válido após a última validação.
        ///
        /// REGRAS:
        /// - Deve retornar TRUE quando não houver erro.
        /// - Deve retornar FALSE quando houver erro ativo.
        /// - Não deve executar validação automaticamente.
        ///
        /// OBS:
        /// O valor reflete o ÚLTIMO estado validado,
        /// não necessariamente o estado atual do texto.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Mensagem de erro associada à última validação.
        ///
        /// REGRAS:
        /// - Deve ser NULL ou string vazia quando IsValid == true.
        /// - Deve conter texto amigável quando IsValid == false.
        /// - Nunca deve lançar exceções.
        ///
        /// USO TÍPICO:
        /// - Exibição de erro abaixo do controle
        /// - Consolidação de erros no formulário
        /// </summary>
        string ErrorMessage { get; }

        /// <summary>
        /// Executa a validação do controle.
        ///
        /// RESPONSABILIDADE:
        /// - Avaliar o estado atual do input
        /// - Atualizar IsValid e ErrorMessage
        /// - Atualizar o estado visual do controle (borda, ícone, etc.)
        ///
        /// REGRAS OBRIGATÓRIAS:
        /// - NÃO deve lançar exceções
        /// - NÃO deve exibir MessageBox
        /// - NÃO deve acessar banco de dados
        /// - NÃO deve depender de regra de negócio externa
        ///
        /// QUANDO USAR:
        /// - LostFocus do input
        /// - Ação "Salvar" no formulário
        /// - Validação em lote (FormBase, Wizard, etc.)
        ///
        /// OBS:
        /// Containers (FormBaseCadastro, etc.)
        /// são responsáveis por CHAMAR Validate(),
        /// não por implementar lógica de validação.
        /// </summary>
        void Validate();
    }
}

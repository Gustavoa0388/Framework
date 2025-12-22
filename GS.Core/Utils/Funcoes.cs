using System;
using System.Globalization;
using System.Text;
using GS.Core.UI.Forms;

namespace GS.Core.UI.Utils
{
    /// <summary>
    /// Classe utilitária do GS Core.
    /// Contém funções genéricas, SEM dependência de UI complexa.
    /// 
    /// Regra de ouro:
    /// - NÃO cria Forms
    /// - NÃO acessa controles visuais
    /// - NÃO depende de Designer
    /// 
    /// Mensagens são delegadas ao FormMsg.
    /// </summary>
    public static class Funcoes
    {
        // =========================================================
        // MENSAGENS PADRONIZADAS (WRAPPERS DO FormMsg)
        // =========================================================

        /// <summary>
        /// Exibe mensagem de sucesso padronizada.
        /// </summary>
        public static void MsgOk(string mensagem)
        {
            FormMsg.Success(mensagem);
        }

        /// <summary>
        /// Exibe mensagem de alerta/atenção.
        /// </summary>
        public static void MsgAlerta(string mensagem)
        {
            FormMsg.Warning(mensagem);
        }

        /// <summary>
        /// Exibe mensagem de erro.
        /// </summary>
        public static void MsgErro(string mensagem)
        {
            FormMsg.Error(mensagem);
        }

        /// <summary>
        /// Exibe pergunta de confirmação.
        /// Retorna true se o usuário confirmar.
        /// </summary>
        public static bool Pergunta(string mensagem)
        {
            return FormMsg.Confirm(mensagem);
        }

        // =========================================================
        // STRINGS / TEXTO
        // =========================================================

        /// <summary>
        /// Remove acentos de uma string.
        /// Útil para comparações, buscas e normalização.
        /// </summary>
        public static string RemoverAcentos(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            var normalized = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Retorna apenas números de uma string.
        /// Ex: CPF, CNPJ, telefone.
        /// </summary>
        public static string SomenteNumeros(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return string.Empty;

            var sb = new StringBuilder();

            foreach (char c in texto)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }

        // =========================================================
        // VALIDAÇÕES SIMPLES
        // =========================================================

        /// <summary>
        /// Valida se a string possui apenas números.
        /// </summary>
        public static bool EhNumerico(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto) && long.TryParse(texto, out _);
        }

        /// <summary>
        /// Valida se a string é uma data válida.
        /// </summary>
        public static bool EhDataValida(string texto)
        {
            return DateTime.TryParse(texto, out _);
        }

        // =========================================================
        // CONVERSÕES SEGURAS
        // =========================================================

        /// <summary>
        /// Converte string para int com segurança.
        /// Retorna valor padrão se falhar.
        /// </summary>
        public static int ToInt(string valor, int padrao = 0)
        {
            return int.TryParse(valor, out int result) ? result : padrao;
        }

        /// <summary>
        /// Converte string para decimal com segurança.
        /// Usa cultura invariante.
        /// </summary>
        public static decimal ToDecimal(string valor, decimal padrao = 0m)
        {
            return decimal.TryParse(
                valor,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decimal result
            ) ? result : padrao;
        }

        /// <summary>
        /// Converte string para DateTime com segurança.
        /// </summary>
        public static DateTime? ToDate(string valor)
        {
            return DateTime.TryParse(valor, out DateTime data)
                ? data
                : (DateTime?)null;
        }

        // =========================================================
        // UTILITÁRIOS GERAIS
        // =========================================================

        /// <summary>
        /// Gera um GUID curto (útil para identificadores visuais).
        /// </summary>
        public static string GuidCurto()
        {
            return Guid.NewGuid().ToString("N")[..8].ToUpper();
        }
    }
}

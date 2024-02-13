using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ASP.NET_Core_Login.Helper.Validation
{
    /// <summary>
    /// Esta classe fornece métodos de validação de campos para HTML Helpers.
    /// </summary>
    public static class FieldsValidation
    {
        /// <summary>
        /// Obtém a classe de erro para um campo especificado.
        /// </summary>
        /// <param name="htmlHelper">O HTML Helper.</param>
        /// <param name="fieldName">O nome do campo.</param>
        /// <returns>A classe de erro CSS correspondente.</returns>
        public static string GetErrorClassFor(this IHtmlHelper htmlHelper, string fieldName)
        {
            ModelErrorCollection? fieldErrors = htmlHelper.ViewData.ModelState[fieldName]?.Errors;
            bool isEmpty = string.IsNullOrEmpty(
                htmlHelper.ViewData.ModelState[fieldName]?.AttemptedValue
            );
            bool hasValidationError = fieldErrors?.Count > 0;
            bool isConfirmationField = fieldName.IsConfirmationField();

            if (isEmpty || hasValidationError)
                return hasValidationError ? "is-invalid" : "";

            if (isConfirmationField)
                return "is-valid"; // Campos de confirmação são tratados de forma especial

            return "is-valid"; // Campos regulares são válidos se não estiverem vazios e não tiverem erros
        }

        /// <summary>
        /// Obtém a classe de erro para o campo de confirmação de senha.
        /// </summary>
        /// <param name="htmlHelper">O HTML Helper.</param>
        /// <returns>A classe de erro CSS correspondente.</returns>
        public static string GetConfirmationErrorClass(this IHtmlHelper htmlHelper)
        {

            // Verifica se há erros de validação para o campo de confirmação de senha.
            bool hasConfirmationError = htmlHelper.ViewData.ModelState["confirmPass"]?.Errors.Count > 0;

            // Verifica se houve uma tentativa de entrada de senha e de confirmação de senha,
            // e se as senhas coincidem ou não.
            bool passwordsMismatch = !string.IsNullOrEmpty(htmlHelper.ViewData.ModelState["Password"]?.AttemptedValue) && !string.IsNullOrEmpty(htmlHelper.ViewData.ModelState["confirmPass"]?.AttemptedValue) && htmlHelper.ViewData.ModelState["Password"]?.AttemptedValue != htmlHelper.ViewData.ModelState["confirmPass"]?.AttemptedValue;

            if (hasConfirmationError || passwordsMismatch)
                return "is-invalid";

            return ""; // Sem erro de confirmação
        }

        /// <summary>
        /// Verifica se o nome do campo é um campo de confirmação de senha.
        /// </summary>
        /// <param name="fieldName">O nome do campo.</param>
        /// <returns>Verdadeiro se o campo for um campo de confirmação de senha; caso contrário, falso.</returns>
        public static bool IsConfirmationField(this string fieldName)
        {
            return fieldName.Equals("confirmPass", StringComparison.OrdinalIgnoreCase);
        }
    }
}
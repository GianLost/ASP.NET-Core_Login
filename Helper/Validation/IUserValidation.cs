using ASP.NET_Core_Login.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Login.Helper.Validation;

/// <summary>
/// Interface que fornece os métodos de validação dos campos de entrada para usuários.
/// </summary>
public interface IUserValidation
{
    /// <summary>
    /// Verifica se um determinado valor existe em um campo específico na base de dados.
    /// </summary>
    /// <param name="fieldName">Nome do campo que corresponde ao atributo do usuário a ser verificado.</param>
    /// <param name="value">Valor correspondente a um atributo do objeto de "Users" a ser verificado.</param>
    /// <returns>True se o valor existir no campo especificado, False caso contrário.</returns>
    Task<bool> CheckIfFieldValueExists(string fieldName, object value);

    /// <summary>
    /// Valida os campos de um usuário com o objetivo de impedir duplicatas.
    /// </summary>
    /// <param name="user">Usuário cujos campos devem ser validados.</param>
    /// <returns>Uma lista de mensagens de erro, indicando campos duplicados.</returns>
    Task<List<(string FieldName, string Message)>> ValidateUserFields(Users user);

    /// <summary>
    /// Valida se as senhas no momento do cadastro de um usuário são correspondentes.
    /// </summary>
    /// <param name="pass">Senha a ser validada.</param>
    /// <param name="confirmPass">Confirmação da senha.</param>
    /// <param name="controller">Controlador que pode ser usado para fornecer feedback ao usuário.</param>
    /// <returns>True se as senhas corresponderem, False caso contrário.</returns>
    bool ValidatePassword(string pass, string confirmPass, Controller controller);
}
using ASP.NET_Core_Login.Database;
using ASP.NET_Core_Login.Helper.Messages;
using ASP.NET_Core_Login.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Login.Helper.Validation;

/// <summary>
/// Classe que implementa a interface "IUserValidation" responsável pela validação de usuários.
/// </summary>
public class UserValidation : IUserValidation
{
    private readonly LoginContext _database;

    public UserValidation(LoginContext database)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
    }

    public async Task<bool> CheckIfFieldValueExists(string fieldName, object value)
    {
        try
        {
            return await _database.Users.AnyAsync(u => EF.Property<string>(u, fieldName) == value.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar a existência do valor no campo {fieldName}: {ex.Message}");
            throw;
        }
    }

    public async Task<List<(string FieldName, string Message)>> ValidateUserFields(Users user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        List<(string FieldName, string Message)> duplicateErrors = new();

        List<(string FieldName, object? Value)> fieldsToValidate =
        new()
        {
            ("Name", user.Name),
            ("Login", user.Login),
            ("Email", user.Email),
            ("CellPhone", user.CellPhone),
        };

        foreach (var (fieldName, value) in fieldsToValidate)
        {
            if (value != null && await CheckIfFieldValueExists(fieldName, value))
                duplicateErrors.Add((fieldName, $"O {fieldName.ToLower()} informado já está em uso."));
        }

        return duplicateErrors;
    }

    public virtual bool ValidatePassword(string pass, string confirmPass, Controller controller)
    {
        if (string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(confirmPass))
        {
            controller.TempData["ErrorPass"] = FeedbackMessages.UnMatchedPassords;
            return false;
        }

        bool passwordsMatch = pass == confirmPass;

        if (!passwordsMatch)
            controller.TempData["ErrorPass"] = FeedbackMessages.UnMatchedPassords;

        return passwordsMatch;
    }

    public void LoginFieldsValidation(string fields, string errorMessage, Controller controller)
    {
        controller.TempData[fields] = errorMessage;
    }
    
}
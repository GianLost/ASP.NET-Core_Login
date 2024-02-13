using System.Security.Cryptography;

namespace ASP.NET_Core_Login.Helper.Authentication;

/// <summary>
/// Classe responsável por gerar o token de sessão para o usuário.
/// </summary>
public class SessionTokenGenerator
{
    /// <summary>
    /// Gera um token de sessão único e aleatório de forma segura para autenticar a sessão do usuário.
    /// </summary>
    /// <returns>Uma string que representa o token de sessão gerado unicamente para cada usuário.</returns>
    public static string GenerateSessionToken()
    {
        byte[] randomBytes = new byte[32]; // 256 bits

        // Utiliza "RNGCryptoServiceProvider" paga gerar bytes aleátorios
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        // Converte os bytes aleatórios em uma string Base64
        return Convert.ToBase64String(randomBytes);
    }
}
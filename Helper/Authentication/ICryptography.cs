namespace ASP.NET_Core_Login.Helper.Authentication
{
    /// <summary>
    /// Interface responsável por fornecer os métodos de criptografia e verificação das senhas.
    /// </summary>
    public interface ICryptography
    {
        /// <summary>
        /// Método utilizado para critografar dados utilizados na validação do usuário.
        /// </summary>
        /// <param name="pass">Corresponde a chave fornecida em texto simples para ser criptografada.</param>
        /// <returns>A senha criptografada.</returns>
        string EncryptKey(string? pass);

        /// <summary>
        /// Método para validar o "salt" gerado pela criptografia.
        /// </summary>
        /// <param name="password">Corresponde a um texto simples fornecido para que seja comparado ao salt da hash gerada.</param>
        /// <param name="hashedPassword">Corresponde a versão criptografada da chave armazenada no banco de dados.</param>
        /// <returns>True se a chave fornecida corresponder à versão criptografada; False caso contrário.</returns>
        bool VerifyKeyEncrypted(string? password, string? hashedPassword);
    }
}

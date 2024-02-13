namespace ASP.NET_Core_Login.Helper.Authentication
{
    /// <summary>
    /// Classe responsável por implementar a interface "ICryptography".
    /// </summary>
    public class Cryptography : ICryptography
    {
        public string EncryptKey(string? pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }

        public bool VerifyKeyEncrypted(string? password, string? hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

namespace ASP.NET_Core_Login.Helper.Messages;
public static class FeedbackMessages
{
    public const string ErrorConnectionString = "A string de conexão com o banco de dados não foi encontrada !";
    public const string ErrorDatabaseConnection = "Não foi possível conectar-se ao banco de dados !";
    public const string ExpiredSession = "A sessão foi encerrada. você precisa se reconectar!";
    public const string SuccessUserRegister = "Usuário cadastrado com sucesso";
    public const string ConfirmPassword = "Confirme sua senha !";
    public const string UnMatchedPassords = "As senhas não são idênticas !";
    public const string ErrorAccountDisable = "O usuário fornecido está com o acesso desabilitado! Contate um administrador !";
    public const string LogInDataNull = "Os dados informados não correspondem à um usuário cadastrado !";
}

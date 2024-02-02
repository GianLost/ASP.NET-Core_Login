using ASP.NET_Core_Login.Keys;

namespace ASP.NET_Core_Login.Models;

public abstract class Users
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? CellPhone { get; set; }
    public string? Workplace { get; set; }
    public string? Position { get; set; }
    public string? SessionToken { get; set; }
    public DateTime RegisterDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public UsersTypeEnum UserType { get; set; }
    public UsersStatsEnum UserStats { get; set; }
}
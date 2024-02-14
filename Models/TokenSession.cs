using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NET_Core_Login.Models;

[Table("TokenSession")]
public class TokenSession
{
    [Key, Required(ErrorMessage = "O campo id é obrigatório !")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O token de sessão é obrigatório!")]
    [StringLength(150), MaxLength(150, ErrorMessage = "O token não pode conter mais de 150 caractéres.")]
    [MinLength(10, ErrorMessage = "O token não pode conter menos de 10 caractéres.")]
    public string? SessionToken { get; set; }

    [Required(ErrorMessage = "informe uma data de registro !")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RegisterDate { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }
    public Users? User { get; set; }
}

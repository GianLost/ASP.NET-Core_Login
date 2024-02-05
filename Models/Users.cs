using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASP.NET_Core_Login.Keys;

namespace ASP.NET_Core_Login.Models;

public abstract class Users
{
    [Key, Required(ErrorMessage = "O campo id é obrigatório !")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo nome é obrigatório !")]
    [StringLength(50), MaxLength(50, ErrorMessage = "O nome não pode conter mais de 50 caractéres.")]
    [MinLength(3, ErrorMessage = "O nome não pode conter menos de 3 caractéres.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Informe um nome de login !")]
    [StringLength(35), MaxLength(35, ErrorMessage = "O login não pode conter mais de 35 caractéres.")]
    [MinLength(5, ErrorMessage = "O login não pode conter menos de 5 caractéres.")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Informe uma senha !")]
    [StringLength(150), MaxLength(150, ErrorMessage = "A senha pode conter no máximo 150 caracteres.")]
    [MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres.")]
    public string? Password { get; set; }

    [StringLength(100), Required(ErrorMessage = "Informe um e-mail !")]
    [EmailAddress(ErrorMessage = "O e-mail informado é inválido.")]
    public string? Email { get; set; }

    [StringLength(15), Required(ErrorMessage = "Informe um telefone !")]
    [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[0-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "O número de telefone é inválido. Tente:(XX) XXXXX-XXXX")]
    public string? CellPhone { get; set; }

    [Required(ErrorMessage = "Informe seu local de trabalho !")]
    [StringLength(35), MaxLength(35, ErrorMessage = "O local de trabalho não pode conter mais de 35 caractéres.")]
    [MinLength(3, ErrorMessage = "O local de trabalho não pode conter menos de 3 caractéres.")]
    public string? Workplace { get; set; }

    [Required(ErrorMessage = "Informe seu cargo !")]
    [StringLength(35), MaxLength(35, ErrorMessage = "O cargo não pode conter mais de 35 caractéres.")]
    [MinLength(3, ErrorMessage = "O cargo não pode conter menos de 3 caractéres.")]
    public string? Position { get; set; }

    [Required(ErrorMessage = "O token de sessão é obrigatório!")]
    [StringLength(150), MaxLength(150, ErrorMessage = "O token não pode conter mais de 35 caractéres.")]
    [MinLength(3, ErrorMessage = "O token não pode conter menos de 3 caractéres.")]
    public string? SessionToken { get; set; }

    [Required(ErrorMessage = "informe uma data de registro !")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime RegisterDate { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime? ModifyDate { get; set; }

    [StringLength(35)]
    public string? LastModifiedBy { get; set; }

    [Required(ErrorMessage = "Informe o tipo de usuário !")]
    public UsersTypeEnum UserType { get; set; }

    [Required(ErrorMessage = "Informe o status do usuário !")]
    public UsersStatsEnum UserStats { get; set; }
}
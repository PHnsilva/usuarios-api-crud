using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Application.Contracts.Requests;

public class CreateUsuarioRequest
{
    [Required]
    [MaxLength(120)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Senha { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string CodigoPessoa { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string LembreteSenha { get; set; } = string.Empty;

    [Range(0, 130)]
    public int Idade { get; set; }

    [Required]
    [MaxLength(30)]
    public string Sexo { get; set; } = string.Empty;
}

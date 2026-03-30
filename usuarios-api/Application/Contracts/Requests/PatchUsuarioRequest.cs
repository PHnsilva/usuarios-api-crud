using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Application.Contracts.Requests;

public class PatchUsuarioRequest
{
    [MaxLength(120)]
    public string? Nome { get; set; }

    [MaxLength(255)]
    public string? Senha { get; set; }

    [MaxLength(20)]
    public string? CodigoPessoa { get; set; }

    [MaxLength(120)]
    public string? LembreteSenha { get; set; }

    [Range(0, 130)]
    public int? Idade { get; set; }

    [MaxLength(30)]
    public string? Sexo { get; set; }
}

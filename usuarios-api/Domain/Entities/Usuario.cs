using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Domain.Entities;

[BsonIgnoreExtraElements]
public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }

    [BsonElement("nome")]
    [Required]
    [MaxLength(120)]
    public string Nome { get; private set; } = string.Empty;

    [BsonElement("email")]
    [Required]
    [EmailAddress]
    [MaxLength(120)]
    public string Email { get; private set; } = string.Empty;

    [BsonElement("senha")]
    [Required]
    [MaxLength(255)]
    public string Senha { get; private set; } = string.Empty;

    [BsonElement("codigoPessoa")]
    [Required]
    [MaxLength(20)]
    public string CodigoPessoa { get; private set; } = string.Empty;

    [BsonElement("lembreteSenha")]
    [Required]
    [MaxLength(120)]
    public string LembreteSenha { get; private set; } = string.Empty;

    [BsonElement("idade")]
    [Range(0, 130)]
    public int Idade { get; private set; }

    [BsonElement("sexo")]
    [Required]
    [MaxLength(30)]
    public string Sexo { get; private set; } = string.Empty;

    public Usuario() { }

    public Usuario(
        string nome,
        string email,
        string senha,
        string codigoPessoa,
        string lembreteSenha,
        int idade,
        string sexo)
    {
        Nome = nome.Trim();
        Email = email.Trim().ToLowerInvariant();
        Senha = senha.Trim();
        CodigoPessoa = codigoPessoa.Trim();
        LembreteSenha = lembreteSenha.Trim();
        Idade = idade;
        Sexo = sexo.Trim();
    }

    public void Atualizar(
        string nome,
        string senha,
        string codigoPessoa,
        string lembreteSenha,
        int idade,
        string sexo)
    {
        Nome = nome.Trim();
        Senha = senha.Trim();
        CodigoPessoa = codigoPessoa.Trim();
        LembreteSenha = lembreteSenha.Trim();
        Idade = idade;
        Sexo = sexo.Trim();
    }

    public void AtualizarParcial(
        string? nome,
        string? senha,
        string? codigoPessoa,
        string? lembreteSenha,
        int? idade,
        string? sexo)
    {
        Nome = string.IsNullOrWhiteSpace(nome) ? Nome : nome.Trim();
        Senha = string.IsNullOrWhiteSpace(senha) ? Senha : senha.Trim();
        CodigoPessoa = string.IsNullOrWhiteSpace(codigoPessoa) ? CodigoPessoa : codigoPessoa.Trim();
        LembreteSenha = string.IsNullOrWhiteSpace(lembreteSenha) ? LembreteSenha : lembreteSenha.Trim();
        Idade = idade ?? Idade;
        Sexo = string.IsNullOrWhiteSpace(sexo) ? Sexo : sexo.Trim();
    }
}

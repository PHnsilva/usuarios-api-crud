using System.Collections.Concurrent;
using UsuariosApi.Domain.Contracts;
using UsuariosApi.Domain.Entities;

namespace UsuariosApi.Infrastructure.Repositories;

public class InMemoryUsuarioRepository : IUsuarioRepository
{
    private readonly ConcurrentDictionary<string, Usuario> _usuarios;

    public InMemoryUsuarioRepository()
    {
        _usuarios = new ConcurrentDictionary<string, Usuario>(StringComparer.OrdinalIgnoreCase);
        Seed();
    }

    public Task<IReadOnlyCollection<Usuario>> GetAllAsync()
        => Task.FromResult((IReadOnlyCollection<Usuario>)_usuarios.Values.OrderBy(x => x.Nome).ToList());

    public Task<Usuario?> GetByEmailAsync(string email)
    {
        _usuarios.TryGetValue(NormalizarEmail(email), out var usuario);
        return Task.FromResult(usuario);
    }

    public Task<bool> ExistsByEmailAsync(string email)
        => Task.FromResult(_usuarios.ContainsKey(NormalizarEmail(email)));

    public Task<bool> ExistsByCodigoPessoaAsync(string codigoPessoa, string? ignoreEmail = null)
    {
        var existe = _usuarios.Values.Any(x =>
            x.CodigoPessoa.Equals(codigoPessoa.Trim(), StringComparison.OrdinalIgnoreCase) &&
            !x.Email.Equals(ignoreEmail ?? string.Empty, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(existe);
    }

    public Task AddAsync(Usuario usuario)
    {
        _usuarios.TryAdd(usuario.Email, usuario);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Usuario usuario)
    {
        _usuarios[usuario.Email] = usuario;
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(string email)
        => Task.FromResult(_usuarios.TryRemove(NormalizarEmail(email), out _));

    private void Seed()
    {
        var pedro = new Usuario(
            "Pedro Henrique",
            "pedro.vargas@sga.pucminas.br",
            "Pedro@123",
            "1525997",
            "nome da universidade",
            21,
            "Masculino");

        var ana = new Usuario(
            "Ana Clara Souza",
            "ana.clara@email.com",
            "Ana@123",
            "2026002",
            "nome do primeiro pet",
            22,
            "Feminino");

        _usuarios.TryAdd(pedro.Email, pedro);
        _usuarios.TryAdd(ana.Email, ana);
    }

    private static string NormalizarEmail(string email)
        => email.Trim().ToLowerInvariant();
}

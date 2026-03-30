using UsuariosApi.Domain.Entities;

namespace UsuariosApi.Domain.Contracts;

public interface IUsuarioRepository
{
    Task<IReadOnlyCollection<Usuario>> GetAllAsync();
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByCodigoPessoaAsync(string codigoPessoa, string? ignoreEmail = null);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task<bool> DeleteAsync(string email);
}

using MongoDB.Driver;
using UsuariosApi.Domain.Contracts;
using UsuariosApi.Domain.Entities;
using UsuariosApi.Infrastructure.Persistence;

namespace UsuariosApi.Infrastructure.Repositories;

public class MongoUsuarioRepository : IUsuarioRepository
{
    private readonly IMongoCollection<Usuario> _collection;

    public MongoUsuarioRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<Usuario>(settings.CollectionName);
    }

    public async Task<IReadOnlyCollection<Usuario>> GetAllAsync()
    {
        var usuarios = await _collection
            .Find(_ => true)
            .SortBy(x => x.Nome)
            .ToListAsync();

        return usuarios;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _collection
            .Find(x => x.Email == NormalizarEmail(email))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var total = await _collection.CountDocumentsAsync(
            x => x.Email == NormalizarEmail(email));

        return total > 0;
    }

    public async Task<bool> ExistsByCodigoPessoaAsync(string codigoPessoa, string? ignoreEmail = null)
    {
        var filtro = Builders<Usuario>.Filter.Eq(x => x.CodigoPessoa, codigoPessoa.Trim());

        if (!string.IsNullOrWhiteSpace(ignoreEmail))
            filtro &= Builders<Usuario>.Filter.Ne(x => x.Email, NormalizarEmail(ignoreEmail));

        var total = await _collection.CountDocumentsAsync(filtro);
        return total > 0;
    }

    public Task AddAsync(Usuario usuario)
        => _collection.InsertOneAsync(usuario);

    public Task UpdateAsync(Usuario usuario)
        => _collection.ReplaceOneAsync(x => x.Email == usuario.Email, usuario);

    public async Task<bool> DeleteAsync(string email)
    {
        var result = await _collection.DeleteOneAsync(
            x => x.Email == NormalizarEmail(email));

        return result.DeletedCount > 0;
    }

    private static string NormalizarEmail(string email)
        => email.Trim().ToLowerInvariant();
}

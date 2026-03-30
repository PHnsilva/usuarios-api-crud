using MongoDB.Driver;
using UsuariosApi.Domain.Entities;

namespace UsuariosApi.Infrastructure.Persistence;

public class MongoIndexInitializer
{
    private readonly IMongoCollection<Usuario> _collection;

    public MongoIndexInitializer(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<Usuario>(settings.CollectionName);
    }

    public async Task CreateIndexesAsync()
    {
        var emailIndex = new CreateIndexModel<Usuario>(
            Builders<Usuario>.IndexKeys.Ascending(x => x.Email),
            new CreateIndexOptions { Unique = true });

        var codigoPessoaIndex = new CreateIndexModel<Usuario>(
            Builders<Usuario>.IndexKeys.Ascending(x => x.CodigoPessoa),
            new CreateIndexOptions { Unique = true });

        await _collection.Indexes.CreateManyAsync(new[] { emailIndex, codigoPessoaIndex });
    }
}

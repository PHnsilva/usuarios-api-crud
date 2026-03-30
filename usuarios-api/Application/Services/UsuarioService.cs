using MongoDB.Driver;
using UsuariosApi.Application.Contracts.Requests;
using UsuariosApi.Application.Contracts.Responses;
using UsuariosApi.Domain.Contracts;
using UsuariosApi.Domain.Entities;

namespace UsuariosApi.Application.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<Usuario>> ListarAsync()
        => _repository.GetAllAsync();

    public Task<Usuario?> ObterPorEmailAsync(string email)
        => _repository.GetByEmailAsync(email);

    public async Task<ServiceResult<Usuario>> CriarAsync(CreateUsuarioRequest request)
    {
        if (await _repository.ExistsByEmailAsync(request.Email))
            return ServiceResult<Usuario>.Fail("Já existe um usuário cadastrado com este e-mail.");

        if (await _repository.ExistsByCodigoPessoaAsync(request.CodigoPessoa))
            return ServiceResult<Usuario>.Fail("Já existe um usuário cadastrado com este código de pessoa.");

        var usuario = new Usuario(
            request.Nome,
            request.Email,
            request.Senha,
            request.CodigoPessoa,
            request.LembreteSenha,
            request.Idade,
            request.Sexo);

        try
        {
            await _repository.AddAsync(usuario);
            return ServiceResult<Usuario>.Ok(usuario);
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
        {
            return ServiceResult<Usuario>.Fail("Já existe um usuário com e-mail ou código de pessoa cadastrado.");
        }
    }

    public async Task<ServiceResult> AtualizarAsync(string email, UpdateUsuarioRequest request)
    {
        var usuario = await _repository.GetByEmailAsync(email);
        if (usuario is null)
            return ServiceResult.Fail("Usuário não encontrado.");

        if (await _repository.ExistsByCodigoPessoaAsync(request.CodigoPessoa, email))
            return ServiceResult.Fail("Já existe um usuário cadastrado com este código de pessoa.");

        usuario.Atualizar(
            request.Nome,
            request.Senha,
            request.CodigoPessoa,
            request.LembreteSenha,
            request.Idade,
            request.Sexo);

        try
        {
            await _repository.UpdateAsync(usuario);
            return ServiceResult.Ok();
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
        {
            return ServiceResult.Fail("Já existe um usuário com e-mail ou código de pessoa cadastrado.");
        }
    }

    public async Task<ServiceResult<Usuario>> AtualizarParcialAsync(string email, PatchUsuarioRequest request)
    {
        var usuario = await _repository.GetByEmailAsync(email);
        if (usuario is null)
            return ServiceResult<Usuario>.Fail("Usuário não encontrado.");

        var novoCodigo = request.CodigoPessoa ?? usuario.CodigoPessoa;
        if (await _repository.ExistsByCodigoPessoaAsync(novoCodigo, email))
            return ServiceResult<Usuario>.Fail("Já existe um usuário cadastrado com este código de pessoa.");

        usuario.AtualizarParcial(
            request.Nome,
            request.Senha,
            request.CodigoPessoa,
            request.LembreteSenha,
            request.Idade,
            request.Sexo);

        try
        {
            await _repository.UpdateAsync(usuario);
            return ServiceResult<Usuario>.Ok(usuario);
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
        {
            return ServiceResult<Usuario>.Fail("Já existe um usuário com e-mail ou código de pessoa cadastrado.");
        }
    }

    public Task<bool> RemoverAsync(string email)
        => _repository.DeleteAsync(email);
}

using CSharpFunctionalExtensions;
using SorokChatServer.Application.Database.Entities;
using SorokChatServer.Application.Interfaces;
using SorokChatServer.Application.Models;

namespace SorokChatServer.Application.Services;

public class UsersService : IUsersService
{
    private readonly IPasswordService _passwordService;
    private readonly IUsersRepository _repository;

    public UsersService(IUsersRepository repository, IPasswordService passwordService)
    {
        _repository = repository;
        _passwordService = passwordService;
    }

    public async Task<Result<User, string>> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetOneById(id, cancellationToken);
        return ToModelResult(result);
    }

    public async Task<Result<User, string>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var result = await _repository.GetOneByEmail(email, cancellationToken);
        return ToModelResult(result);
    }

    public async Task<Result<User, string>> Create(User user, CancellationToken cancellationToken)
    {
        var entity = user.ToEntity();
        var hashedPassword = _passwordService.Hash(user.Password.Value);
        entity.Password = hashedPassword;
        var result = await _repository.Create(entity, cancellationToken);
        return ToModelResult(result);
    }

    public async Task<Result<User, string>> Update(long id, User updatedUser, CancellationToken cancellationToken)
    {
        var updatedEntity = updatedUser.ToEntity();
        if (!string.IsNullOrEmpty(updatedEntity.Password))
            updatedEntity.Password = _passwordService.Hash(updatedEntity.Password);
        var result = await _repository.Update(x => x.Id == id, updatedEntity, cancellationToken);
        return ToModelResult(result);
    }

    public async Task<Result<User, string>> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await _repository.Delete(x => x.Id == id, cancellationToken);
        return ToModelResult(result);
    }

    private static Result<User, string> ToModelResult(Result<UserEntity, string> modelResult)
    {
        if (modelResult.IsFailure) return Result.Failure<User, string>(modelResult.Error);
        var model = User.FromEntity(modelResult.Value);
        return Result.Success<User, string>(model);
    }
}
namespace Architecture.Application;

public sealed record ListUserHandler : IHandler<ListUserRequest, IEnumerable<UserModel>>
{
    private readonly IUserRepository _userRepository;

    public ListUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserModel>>> HandleAsync(ListUserRequest request)
    {
        var users = await _userRepository.ListModelAsync();

        return Result<IEnumerable<UserModel>>.Success(users);
    }
}

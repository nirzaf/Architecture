namespace Architecture.Application;

public sealed record AddUserHandler : IHandler<AddUserRequest, long>
{
    private readonly IAuthRepository _authRepository;
    private readonly IHashService _hashService;
    private readonly IStringLocalizer _stringLocalizer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public AddUserHandler
    (
        IAuthRepository authRepository,
        IHashService hashService,
        IStringLocalizer stringLocalizer,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository
    )
    {
        _authRepository = authRepository;
        _hashService = hashService;
        _stringLocalizer = stringLocalizer;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<Result<long>> HandleAsync(AddUserRequest request)
    {
        if (await _userRepository.EmailExistsAsync(request.Email)) return Result<long>.Error(_stringLocalizer["EmailExists"]);

        if (await _authRepository.LoginExistsAsync(request.Login)) return Result<long>.Error(_stringLocalizer["LoginExists"]);

        var user = new User(request.Name, request.Email);

        var auth = new Auth(request.Login, request.Password, user);

        var password = _hashService.Create(auth.Password, auth.Salt.ToString());

        auth.UpdatePassword(password);

        await _userRepository.AddAsync(user);

        await _authRepository.AddAsync(auth);

        await _unitOfWork.SaveChangesAsync();

        return Result<long>.Success(user.Id);
    }
}

namespace Architecture.Database;

public sealed class UserRepository : EFRepository<User>, IUserRepository
{
    public UserRepository(Context context) : base(context) { }

    public static Expression<Func<User, UserModel>> Model => user => new UserModel
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email
    };

    public Task<bool> EmailExistsAsync(string email) => Queryable.AnyAsync(user => user.Email == email);

    public Task<UserModel> GetModelAsync(long id) => Queryable.Where(user => user.Id == id).Select(Model).SingleOrDefaultAsync();

    public Task<Grid<UserModel>> GridAsync(GridParameters parameters) => Queryable.Select(Model).GridAsync(parameters);

    public async Task<IEnumerable<UserModel>> ListModelAsync() => await Queryable.Select(Model).ToListAsync();
}

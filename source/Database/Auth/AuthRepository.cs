namespace Architecture.Database;

public sealed class AuthRepository : EFRepository<Auth>, IAuthRepository
{
    public AuthRepository(Context context) : base(context) { }

    public Task DeleteByUserIdAsync(long userId) => DeleteAsync(auth => auth.User.Id == userId);

    public Task<Auth> GetByLoginAsync(string login) => Queryable.SingleOrDefaultAsync(auth => auth.Login == login);

    public Task<bool> LoginExistsAsync(string login) => Queryable.AnyAsync(auth => auth.Login == login);
}

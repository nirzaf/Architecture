namespace Architecture.Database;

public interface IUserRepository : IRepository<User>
{
    Task<bool> EmailExistsAsync(string email);

    Task<UserModel> GetModelAsync(long id);

    Task<Grid<UserModel>> GridAsync(GridParameters parameters);

    Task<IEnumerable<UserModel>> ListModelAsync();
}

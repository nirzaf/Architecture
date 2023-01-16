namespace Architecture.Application;

public sealed record UpdateUserRequest(long Id, string Name, string Email);

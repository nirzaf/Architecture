namespace Architecture.Application;

public sealed record AddFileHandler : IHandler<AddFileRequest, IEnumerable<BinaryFile>>
{
    public async Task<Result<IEnumerable<BinaryFile>>> HandleAsync(AddFileRequest request)
    {
        var files = await request.Files.SaveAsync("Files");

        return Result<IEnumerable<BinaryFile>>.Success(files);
    }
}

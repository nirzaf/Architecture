namespace Architecture.Web;

[ApiController]
[Route("files")]
public sealed class FileController : BaseController
{
    [DisableRequestSizeLimit]
    [HttpPost]
    public IActionResult Add(AddFileRequest request) => Mediator.HandleAsync<AddFileRequest, IEnumerable<BinaryFile>>(request).ApiResult();

    [HttpGet("{id}")]
    public IActionResult Get(Guid id) => Mediator.HandleAsync<GetFileRequest, BinaryFile>(new GetFileRequest(id)).ApiResult();
}

public class ComputerPartController : ControllerBase
{
    private readonly ExcelUploadContext _context;

    public ComputerPartController(ExcelUploadContext context)
    {
        _context = context;
    }
	// ...
}
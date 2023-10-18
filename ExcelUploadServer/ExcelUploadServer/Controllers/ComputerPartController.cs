using ExcelUploadServer.Data;
using ExcelUploadServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelUploadServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerPartController : ControllerBase
    {
        private readonly ExcelUploadContext _context;

        public ComputerPartController(ExcelUploadContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult CreateEdit(IEnumerable<UploadModel> computerPartList)
        {
            if (computerPartList == null || !computerPartList.Any())
            {
                return new JsonResult(BadRequest("The computer part list is empty."));
            }

            foreach (var computerPart in computerPartList)
            {
                var existingCategory = _context.Category.FirstOrDefault(k => k.CategoryName == computerPart.CategoryName);
                if (existingCategory == null)
                {
                    /*TODO: return the incorrect categories to the user.*/
                    return new JsonResult(BadRequest("Incorrect category"));
                }

                ComputerPart cp = new ComputerPart
                {
                    ComputerPartName = computerPart.ComputerPartName,
                    CategoryId = existingCategory.Id
                };
                _context.Add(cp);
                _context.SaveChanges();
            }
            return new JsonResult(Ok());
        }

    }
}

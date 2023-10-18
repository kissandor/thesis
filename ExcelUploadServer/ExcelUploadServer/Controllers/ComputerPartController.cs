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
        public JsonResult CreateEdit(IEnumerable<ComputerPart> computerPartList)
        {
            if (computerPartList == null || !computerPartList.Any())
            {
                return new JsonResult(BadRequest("Nincs elmentendő érték."));
            }

            foreach (var computerPart in computerPartList)
            {
                if (computerPart.Id == 0)
                {
                    _context.ComputerParts.Add(computerPart);
                }
                else
                {
                    var computerPartInDb = _context.ComputerParts.Find(computerPart.Id);
                    if (computerPartInDb == null)
                        return new JsonResult(NotFound());
                    computerPartInDb = computerPart;
                }
            }

            _context.SaveChanges();

            return new JsonResult(Ok(computerPartList));
        }
    }
}

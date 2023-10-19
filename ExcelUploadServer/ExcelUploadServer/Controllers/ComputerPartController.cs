using ExcelUploadServer.Data;
using ExcelUploadServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExcelUploadServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComputerPartController : ControllerBase
    {
        private readonly ExcelUploadContext _context;

        public ComputerPartController(ExcelUploadContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult ComputerPartsUpload(IEnumerable<UploadModel> computerPartList)
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
        
        [HttpPost]
        public JsonResult CategoryUpload(IEnumerable<Category> categoryList)
        {
            if (categoryList == null || !categoryList.Any())
            {
                return new JsonResult(BadRequest("The computer part list is empty."));
            }

            foreach (var category in categoryList)
            {
                _context.Category.Add(category);
            }
            _context.SaveChanges();
            return new JsonResult(Ok());
        }

        [HttpPost]
        public JsonResult WebshopUpload(IEnumerable<WebShop> webShopList)
        {
            if (webShopList == null || !webShopList.Any())
            {
                return new JsonResult(BadRequest("The computer part list is empty."));
            }

            foreach (var webShop in webShopList)
            {
                _context.Add(webShop);
                _context.SaveChanges();
            }
            return new JsonResult(Ok());
        }
    }
}

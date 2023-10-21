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

        private JsonResult BadRequestResult(string message)
        {
            return new JsonResult(BadRequest(message));
        }

        [HttpPost]
        public JsonResult ComputerPartsUpload(IEnumerable<UploadModel> computerPartList)
        {
            if (computerPartList == null || !computerPartList.Any())
            {
                return BadRequestResult("The computer part list is empty.");
            }

            var invalidCategories = new List<string>();

            foreach (var computerPart in computerPartList)
            {
                var existingCategory = _context.Category.FirstOrDefault(k => k.CategoryName == computerPart.CategoryName);
                if (existingCategory == null)
                {
                    invalidCategories.Add(computerPart.CategoryName);
                }
                else
                {
                    ComputerPart cp = new ComputerPart
                    {
                        ComputerPartName = computerPart.ComputerPartName,
                        CategoryId = existingCategory.Id
                    };
                    _context.Add(cp);
                }

            }

            _context.SaveChanges();

            if (invalidCategories.Any())
            {
                var errorResponse = new
                {
                    Error = "Incorrect categories",
                    InvalidCategories = invalidCategories
                };
                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            return new JsonResult(Ok());
        }

        [HttpPost]
        public JsonResult CategoryUpload(IEnumerable<Category> categoryList)
        {
            if (categoryList == null || !categoryList.Any())
            {
                return BadRequestResult("The category list is empty.");
            }

            foreach (var category in categoryList)
            {
                var existingCategory = _context.Category.FirstOrDefault(k => k.CategoryName == category.CategoryName);
                if (existingCategory == null)
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
                return BadRequestResult("The webShop list is empty.");
            }

            foreach (var webShop in webShopList)
            {
                var existingWebShop = _context.WebShops.FirstOrDefault(w => w.WebshopName == webShop.WebshopName);
                if (existingWebShop == null)
                    _context.Add(webShop);
            }
            _context.SaveChanges();
            return new JsonResult(Ok());
        }

        //getting all Computerparts from the database
        [HttpGet]
        public JsonResult GetAllComputerParts()
        {
            var result = _context.ComputerParts.ToList();
            return new JsonResult(Ok(result));
        }


        [HttpGet]
        public JsonResult GetAllCategories()
        {
            var result = _context.Category.ToList();
            return new JsonResult(Ok(result));
        }

        [HttpGet]
        public JsonResult GetAllWebShops()
        {
            var result = _context.WebShops.ToList();
            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult DeleteAll()
        {
            try
            {
                _context.ComputerParts.RemoveRange(_context.ComputerParts.ToList());
                _context.SaveChanges();
                _context.Category.RemoveRange(_context.Category.ToList());
                _context.WebShops.RemoveRange(_context.WebShops.ToList());
                _context.SaveChanges();
                return new JsonResult("All computer parts have been deleted successfully.")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                // Hiba esetén hibás válasz küldése
                return new JsonResult($"Error: {ex.Message}")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}

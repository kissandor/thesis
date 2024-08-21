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
            try
            {
                foreach (var computerPart in computerPartList)
                {
                    var existingPart = _context.ComputerParts.FirstOrDefault(k => k.ComputerPartName == computerPart.ComputerPartName);
                    var existingCategory = _context.Categories.FirstOrDefault(k => k.CategoryName == computerPart.CategoryName);

                    if (existingPart == null && existingCategory != null)
                    {
                        ComputerPart cp = new ComputerPart
                        {
                            ComputerPartName = computerPart.ComputerPartName,
                            CategoryId = existingCategory.Id
                        };

                        _context.ComputerParts.Add(cp);
                    }
                    else if ((existingPart == null && existingCategory == null) || (existingPart != null && existingCategory == null))
                    {
                        invalidCategories.Add(computerPart.CategoryName);
                    }
                    else
                    {
                        //category exist and part exist as well
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
                var sender = new SendEmail();
                sender.emailSender();

                return new JsonResult(Ok());
            } catch (Exception ex) 
            {
                return BadRequestResult("Error during the upload of parts.");
            }
        }

        [HttpPost]
        public JsonResult CategoryUpload(IEnumerable<Category> categoryList)
        {
            if (categoryList == null || !categoryList.Any())
            {
                return BadRequestResult("The category list is empty.");
            }
            try
            {
                foreach (var category in categoryList)
                {
                    var existingCategory = _context.Categories.FirstOrDefault(k => k.CategoryName == category.CategoryName);
                    if (existingCategory == null)
                        _context.Categories.Add(category);
                }
                _context.SaveChanges();
                return new JsonResult(Ok());
            } catch (Exception ex) 
            {
                return BadRequestResult("Error during the upload of categories.");
            }
        }

        [HttpPost]
        public JsonResult WebShopUpload(IEnumerable<WebShop> webShopList)
        {
            if (webShopList == null || !webShopList.Any())
            {
                return BadRequestResult("The webShop list is empty.");
            }

            try
            {
                foreach (var webShop in webShopList)
                {
                    var existingWebShop = _context.WebShops.FirstOrDefault(w => w.WebShopName == webShop.WebShopName);
                    if (existingWebShop == null)
                        _context.Add(webShop);
                }
                _context.SaveChanges();
                return new JsonResult(Ok());
            } catch (Exception ex) 
            { 
                return BadRequestResult("Error during the upload of webshops.");
            }
        }

        //getting all Computerparts from the database
        [HttpGet]
        public JsonResult GetAllComputerParts()
        {
            try
            {
                var result = _context.ComputerParts.ToList();
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequestResult("Error occurred while retrieving computer parts.");
            }
        }


        [HttpGet]
        public JsonResult GetAllCategories()
        {
            try
            {
                var result = _context.Categories.ToList();
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequestResult("Error occurred while retrieving categories.");
            }
        }

        [HttpGet]
        public JsonResult GetAllWebShops()
        {
            try
            {
                var result = _context.WebShops.ToList();
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return BadRequestResult("Error occurred while retrieving webshops.");
            }
        }

        [HttpDelete]
        public JsonResult DeleteAll()
        {
            try
            {
                _context.ComputerParts.RemoveRange(_context.ComputerParts.ToList());
                _context.SaveChanges();
                _context.Categories.RemoveRange(_context.Categories.ToList());
                _context.WebShops.RemoveRange(_context.WebShops.ToList());
                _context.SaveChanges();
                return new JsonResult("All computer parts have been deleted successfully.")
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}

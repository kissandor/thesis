using ExcelUploadServer.Data;
using ExcelUploadServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExcelUploadServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComputerPartController : ControllerBase
    {
        private readonly ExcelUploadContext context;

        public ComputerPartController(ExcelUploadContext context)
        {
            this.context = context;
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
                    var existingPart = context.ComputerParts.FirstOrDefault(k => k.ComputerPartName == computerPart.ComputerPartName);
                    var existingCategory = context.Categories.FirstOrDefault(k => k.CategoryName == computerPart.CategoryName);

                    if (existingPart == null && existingCategory != null)
                    {
                        ComputerPart cp = new ComputerPart
                        {
                            ComputerPartName = computerPart.ComputerPartName,
                            CategoryId = existingCategory.Id
                        };

                        context.ComputerParts.Add(cp);
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

                context.SaveChanges();

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
            } 
            catch (Exception ex) 
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
                    var existingCategory = context.Categories.FirstOrDefault(k => k.CategoryName == category.CategoryName);
                    if (existingCategory == null)
                        context.Categories.Add(category);
                }
                context.SaveChanges();
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
                    var existingWebShop = context.WebShops.FirstOrDefault(w => w.WebShopName == webShop.WebShopName);
                    if (existingWebShop == null)
                        context.Add(webShop);
                }
                context.SaveChanges();
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
                // var result = context.ComputerParts.ToList();

                var result = context.ComputerParts
                            .Include(cp => cp.Category) //
                            .Select(cp => new
                            {
                                cp.Id,
                                cp.ComputerPartName,
                                CategoryName = cp.Category.CategoryName 
                            })
                            .ToList();

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
                var result = context.Categories.ToList();
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
                var result = context.WebShops.ToList();
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
                context.ComputerParts.RemoveRange(context.ComputerParts.ToList());
                context.SaveChanges();
                context.Categories.RemoveRange(context.Categories.ToList());
                context.WebShops.RemoveRange(context.WebShops.ToList());
                context.SaveChanges();
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

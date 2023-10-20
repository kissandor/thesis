﻿using ExcelUploadServer.Data;
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

            foreach (var computerPart in computerPartList)
            {
                var existingCategory = _context.Category.FirstOrDefault(k => k.CategoryName == computerPart.CategoryName);
                if (existingCategory == null)
                {
                    /*TODO: return the incorrect categories to the user.*/
                    return BadRequestResult("Incorrect category");
                }

                ComputerPart cp = new ComputerPart
                {
                    ComputerPartName = computerPart.ComputerPartName,
                    CategoryId = existingCategory.Id
                };
                _context.Add(cp);
            }

            _context.SaveChanges();
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
    }
}

public JsonResult GetAllCategories()
{
    try { var result = _context.Categories.ToList();
            return new JsonResult(result);}
    catch (Exception ex){ return BadRequestResult("Error occurred while retrieving categories."); }
}
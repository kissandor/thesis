using ExcelUploadServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelUploadServer.Data
{
    public class ExcelUploadContext : DbContext
    {
        protected ExcelUploadContext()
        {
        }
        public ExcelUploadContext(DbContextOptions<ExcelUploadContext> options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<WebShop> WebShops { get; set; }   
        public DbSet<ComputerPart> ComputerParts { get; set; }
    }
}

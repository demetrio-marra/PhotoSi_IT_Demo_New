using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Recapiti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Recapiti.Data
{
    public class RecapitiDbContext : DbContext
    {
        public RecapitiDbContext(DbContextOptions<RecapitiDbContext> options) : base(options)
        {

        }

        public DbSet<RecapitoEntity> Recapiti { get; set; } = null!;
    }
}
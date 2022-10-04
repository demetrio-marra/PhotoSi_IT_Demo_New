using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Ordini.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Ordini.Data
{
    public class OrdiniDbContext : DbContext
    {
        public OrdiniDbContext(DbContextOptions<OrdiniDbContext> options) : base(options)
        {

        }

        public DbSet<OrdineEntity> Ordini { get; set; } = null!;
    }
}
using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Prodotti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Prodotti.Data
{
    public class ProdottiDbContext : DbContext
    {
        public ProdottiDbContext(DbContextOptions<ProdottiDbContext> options) : base(options)
        {

        }

        public DbSet<ProdottoEntity> Prodotti { get; set; } = null!;
        public DbSet<ProdottoOrdinatoEntity> ProdottiOrdinati { get; set; } = null!;
    }
}
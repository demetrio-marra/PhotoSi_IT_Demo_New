using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Services.Utenti.Data.Entities;

namespace PhotoSi_IT_Demo_New.Services.Utenti.Data
{
    public class UtentiDbContext : DbContext
    {
        public UtentiDbContext(DbContextOptions<UtentiDbContext> options) : base(options)
        {

        }

        public DbSet<UtenteEntity> Utenti { get; set; } = null!;
    }
}
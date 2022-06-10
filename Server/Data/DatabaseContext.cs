using Models;

namespace ProgettoIDS.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Ordine> Ordini { get; set; }
        public DbSet<Utente> Utenti { get; set; }
    }
}

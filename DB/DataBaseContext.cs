using EjemploPracticaNetCore.Model;
using Microsoft.EntityFrameworkCore;

namespace EjemploPracticaNetCore.DB
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}

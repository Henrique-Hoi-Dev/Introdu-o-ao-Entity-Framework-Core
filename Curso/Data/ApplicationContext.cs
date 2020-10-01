using Microsoft.EntityFrameworkCore;
using CursoEFCore.Domain;
using Microsoft.Extensions.Logging;

namespace CursoEFCore.Data
{
    
    public class ApplicationContext : DbContext
    {
      private static readonly ILoggerFactory _logger = LoggerFactory.Create(p=>p.AddConsole());
      public DbSet<Pedido> Pedido { get; set; }
      public DbSet<Produto> Produto { get; set; }
      public DbSet<Cliente> Cliente { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
        optionsBuilder
              .UseLoggerFactory(_logger)
              .EnableSensitiveDataLogging()
              .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=newTeste;Integrated Security=true");
      }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
            
    }
}
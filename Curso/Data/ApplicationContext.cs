using Microsoft.EntityFrameworkCore;
using CursoEFCore.Domain;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;

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
              .UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=newTeste;Integrated Security=true",
              p=>p.EnableRetryOnFailure(
              maxRetryCount: 2, 
              maxRetryDelay: 
              TimeSpan.FromSeconds(5),
              errorNumbersToAdd: null)
              .MigrationsHistoryTable("curso_ef_core")
              );
      }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
          foreach(var entity in modelBuilder.Model.GetEntityTypes())
          {
            var properteis = entity.GetProperties().Where(p=>p.ClrType == typeof(string));

            foreach(var property in properteis)
            {
              if(string.IsNullOrEmpty(property.GetColumnType())
                && !property.GetMaxLength().HasValue)
                {
                  property.SetColumnType("VARCHAR(100)");
                }
            }
          }
        }
            
    }
}
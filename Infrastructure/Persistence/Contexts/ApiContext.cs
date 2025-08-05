using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Persistence.Contexts
{
	public class ApiContext : DbContext
	{
		public ApiContext()
		{

		}

		public ApiContext(DbContextOptions<ApiContext> options) : base(options)
		{

		}

		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Detail> Details { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(act => act.Log(RelationalEventId.PendingModelChangesWarning));
		}
	}
}

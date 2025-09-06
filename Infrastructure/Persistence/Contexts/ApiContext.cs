using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Persistence.Contexts
{
	public class ApiContext : IdentityDbContext<User,Role, Guid>
	{
		
		public ApiContext(DbContextOptions<ApiContext> options) : base(options)
		{

		}

		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Detail> Details { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<CategoryProduct> CategoryProducts { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(act => act.Log(RelationalEventId.PendingModelChangesWarning));
		}
	}
}

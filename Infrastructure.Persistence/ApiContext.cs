using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
	public class ApiContext : DbContext
	{
		protected override void OnConfiguring
	   (DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
		}

		public DbSet<HouseEntity> Houses { get; set; }
	}
}
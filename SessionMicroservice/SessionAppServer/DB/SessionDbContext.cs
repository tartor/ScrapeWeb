namespace SessionAppServer.DB
{
	using Microsoft.EntityFrameworkCore;

	public class SessionDbContext : DbContext
	{
		public DbSet<Login> Logins { get; set; }
		public DbSet<Session> Sessions { get; set; }

		public SessionDbContext(DbContextOptions<SessionDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Optional seed data
			modelBuilder.Entity<Login>()
				.HasData(
				new Login { Id = 1, Username = "John", Password = "password123" },
				new Login { Id = 2, Username = "Mark", Password = "password123" }
			);
		}

	}
}

namespace ScrapeAppServer.DB
{
	using Microsoft.EntityFrameworkCore;
	using ScrapeAppServer.Interface;
	using SessionAppServer.DB;

	public class ScrapeDbContext : DbContext
	{
		public DbSet<ScrapeRecord> Scrape { get; set; }

		public ScrapeDbContext(DbContextOptions<ScrapeDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			// Optional seed data
			modelBuilder.Entity<ScrapeRecord>()
				.HasData(
				new ScrapeRecord { Id = 1, LoginId = 1, Place = 20, Stamp = DateTime.Now - TimeSpan.FromDays(5), Selector= "land registry searches", Url= "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 2, LoginId = 1, Place = 22, Stamp = DateTime.Now - TimeSpan.FromDays(4), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 3, LoginId = 1, Place = 21, Stamp = DateTime.Now - TimeSpan.FromDays(3), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 4, LoginId = 1, Place = 19, Stamp = DateTime.Now - TimeSpan.FromDays(2), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 5, LoginId = 1, Place = 18, Stamp = DateTime.Now - TimeSpan.FromDays(1), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				
				new ScrapeRecord { Id = 6, LoginId = 2, Place = 22, Stamp = DateTime.Now - TimeSpan.FromDays(5), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 7, LoginId = 2, Place = 24, Stamp = DateTime.Now - TimeSpan.FromDays(4), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 8, LoginId = 2, Place = 23, Stamp = DateTime.Now - TimeSpan.FromDays(3), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 9, LoginId = 2, Place = 22, Stamp = DateTime.Now - TimeSpan.FromDays(2), Selector = "land registry searches", Url = "www.infotrack.co.uk" },
				new ScrapeRecord { Id = 10, LoginId = 2, Place = 20, Stamp = DateTime.Now - TimeSpan.FromDays(1), Selector = "land registry searches", Url = "www.infotrack.co.uk" }
			);
		}

	}
}

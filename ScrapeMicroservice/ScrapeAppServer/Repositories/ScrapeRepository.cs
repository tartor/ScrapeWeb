using Microsoft.EntityFrameworkCore;
using ScrapeAppServer.DB;
using SessionAppServer.DB;
using System;

namespace SessionAppServer.Repositories;

public interface IScrapeRepository
{
	Task<ScrapeRecord> AddScrapeAsync(ScrapeRecord record);
}

public class ScrapeRepository : IScrapeRepository
{
	private readonly ScrapeDbContext _dbContext;

	public ScrapeRepository(ScrapeDbContext dbContext)
	{
		_dbContext = dbContext;
		_dbContext.Database.EnsureCreated();
	}

	public async Task<ScrapeRecord> AddScrapeAsync(ScrapeRecord record)
	{
		await _dbContext.Scrape.AddAsync(record);
		await _dbContext.SaveChangesAsync();
		return record;
	}

}
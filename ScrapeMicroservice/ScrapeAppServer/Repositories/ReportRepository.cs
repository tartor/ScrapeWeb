using Microsoft.EntityFrameworkCore;
using ScrapeAppServer.DB;
using SessionAppServer.DB;
using System;

namespace SessionAppServer.Repositories;

public interface IReportRepository
{
	Task<List<ScrapeRecord>> GetScrapesAsync(int loginId, DateTime? from, DateTime? to);
}

public class ReportRepository : IReportRepository
{
	private readonly ScrapeDbContext _dbContext;

	public ReportRepository(ScrapeDbContext dbContext)
	{
		_dbContext = dbContext;
		_dbContext.Database.EnsureCreated();
	}

	public async Task<List<ScrapeRecord>> GetScrapesAsync(int loginId, DateTime? frm, DateTime? to)
	{
		return await (
			from u in _dbContext.Scrape
			where (!frm.HasValue || u.Stamp >= frm.Value)
				&& (!to.HasValue || u.Stamp <= to.Value) &&
				u.LoginId == loginId
			orderby u.Stamp descending
			select u
		).ToListAsync();
	}

}
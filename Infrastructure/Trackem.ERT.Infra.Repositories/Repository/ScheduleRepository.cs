using Microsoft.EntityFrameworkCore;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.EFCore;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Repositories.Repository;
public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
{
    public ScheduleRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    public async Task<PagedList<Schedule>> GetSchedulesAsync(bool trackChanges, ScheduleParameter scheduleParameter)
    {
        try
        {
            var data = FindAll(trackChanges)
                /* Below belongs RepositoryClaimExtensions
                .FilterClaims(claimParameter.MinId, claimParameter.MaxId)
                .Search(claimParameter.SearchTerm)
                .Sort(claimParameter.OrderBy)
                */
                .Skip((scheduleParameter.PageNumber - 1) * scheduleParameter.PageSize)
               .Take(scheduleParameter.PageSize)
               .AsQueryable();

            var result = data.ToList();

            var count = await FindAll(trackChanges)
                //    .FilterClaims(claimParameter.MinId, claimParameter.MaxId)
                //    .Search(claimParameter.SearchTerm)
                .CountAsync();

            PagedList<Schedule> schedules = new PagedList<Schedule>(result, count, scheduleParameter.PageNumber, scheduleParameter.PageSize);
            return schedules; // Performance Better 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Schedule> GetScheduleAsync(long id, bool trackChanges) => await FindByCondition(e => e.SystemId.Equals(id), trackChanges).SingleOrDefaultAsync();

}
using Microsoft.EntityFrameworkCore;
using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.Contracts;
using Trackem.ERT.Infra.EFCore;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Repositories.Repository
{
    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<Material> GetMaterialAsync(long id, bool trackChanges)
        {
            return await FindByCondition(e => e.SystemId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<PagedList<Material>> GetMaterialsAsync(bool trackChanges, MaterialParameter materialParameter)
        {
            try
            {
                //var data = await FindByCondition(c => c.Schedule).Where( sc => sc.SystemId == (materialParameter.ScheduleId), trackChanges)
                //    .Skip((materialParameter.PageNumber - 1) * materialParameter.PageSize)
                //   .Take(materialParameter.PageSize)
                //   .AsQueryable();

                var data = FindAll(trackChanges).Include(csmp => csmp.Schedule)
                    .ThenInclude(csmp => csmp.Company)
                    .Where(s => s.ScheduleSystemId == materialParameter.ScheduleId && s.Schedule.Company.SystemId == materialParameter.CompanyId)
                    .Skip((materialParameter.PageNumber - 1) * materialParameter.PageSize)
                    .Take(materialParameter.PageSize)
                    .AsQueryable();

                //var data = FindAll(trackChanges).Include(csmp => csmp.Schedule).Where(s => s.ScheduleSystemId == materialParameter.ScheduleId)

                //    .Skip((materialParameter.PageNumber - 1) * materialParameter.PageSize)
                //   .Take(materialParameter.PageSize)
                //   .AsQueryable();

                var result = data.ToList();

                var count = await FindAll(trackChanges).Include(csmp => csmp.Schedule)
                     .ThenInclude(csmp => csmp.Company)
                     .Where(s => s.ScheduleSystemId == materialParameter.ScheduleId && s.Schedule.Company.SystemId == materialParameter.CompanyId)
                     .CountAsync();
                //var count = await FindAll(trackChanges).Include(csmp => csmp.Schedule).Where(s => s.ScheduleSystemId == materialParameter.ScheduleId)
                //                   .CountAsync();

                PagedList<Material> materials = new PagedList<Material>(result, count, materialParameter.PageNumber, materialParameter.PageSize);
                return materials; // Performance Better but can switch to above one too
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<PagedList<Material>> GetMaterialsAsync(bool trackChanges, MaterialParameter materialParameter)
        //{
        //    try
        //    {
        //        var data = FindAll(trackChanges).Include(csmp => csmp.Schedule).Where(s=> s.ScheduleSystemId == materialParameter.ScheduleId )
        //            /* Below belongs RepositoryClaimExtensions
        //            .FilterClaims(claimParameter.MinId, claimParameter.MaxId)
        //            .Search(claimParameter.SearchTerm)
        //            .Sort(claimParameter.OrderBy)
        //            */
        //            .Skip((materialParameter.PageNumber - 1) * materialParameter.PageSize)
        //           .Take(materialParameter.PageSize)
        //           .AsQueryable();

        //        var result = data.ToList();

        //        var count = await FindAll(trackChanges).Include(csmp => csmp.Schedule).Where(s => s.ScheduleSystemId == materialParameter.ScheduleId)
        //        //    .FilterClaims(claimParameter.MinId, claimParameter.MaxId)
        //        //    .Search(claimParameter.SearchTerm)
        //            .CountAsync();

        //        PagedList<Material> materials = new PagedList<Material>(result, count, materialParameter.PageNumber, materialParameter.PageSize);
        //        return materials; // Performance Better but can switch to above one too
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
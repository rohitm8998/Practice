using Trackem.ERT.Infra.Common.Entities.RequestFeatures;
using Trackem.ERT.Infra.Common.Entities.RequestParameters;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Infra.Contracts;
public interface IScheduleRepository
{
    Task<PagedList<Schedule>> GetSchedulesAsync(bool trackChanges, ScheduleParameter scheduleParameter);
    Task<Schedule> GetScheduleAsync(long id, bool trackChanges);
}


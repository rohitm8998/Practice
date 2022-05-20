using AutoMapper;
using Trackem.ERT.Core.DataModels;
using Trackem.ERT.Infra.EFCore.Entities;

namespace Trackem.ERT.Web.Apis;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Claims, ClaimViewModel>();

        CreateMap<Material, MaterialGridDetailResponse>()
            .ForMember(d => d.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(d => d.ScheduleSystemId, opts => opts.MapFrom(src => src.ScheduleSystemId))
            .ForMember(d => d.ScheduleProjectName, opts => opts.MapFrom(src => src.Schedule.ProjectName))
            .ForMember(d => d.CompanySystemId, opts => opts.MapFrom(src => src.Schedule.Company.SystemId))
            .ForMember(d => d.CompanyName, opts => opts.MapFrom(src => src.Schedule.Company.Name));
        
    }
}
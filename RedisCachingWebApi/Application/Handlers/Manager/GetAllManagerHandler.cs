using AutoMapper;
using MediatR;
using RedisCachingWebApi.Application.Models;
using RedisCachingWebApi.Domain;
using RedisCachingWebApi.Interface;

namespace RedisCachingWebApi.Application.Handlers.Manager
{
    public class GetAllManagerHandler
    {
        public class Query : IRequest<Response>
        {
        }

        public class Response
        {
            public ManagerModel[] FormData { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IManagerRepository _managerRepository;
            private readonly IMapper _mapper;

            public Handler(IManagerRepository managerRepository, IMapper mapper)
            {
                _managerRepository = managerRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
            {
                Response response = new();

                var managerData = await _managerRepository.GetAllManagerDatasAsync();
                var salaryHigh3400 = managerData.Where(x => x.Salary >= 3400 && x.ManagerName=="string").ToList();

                var salarySort = managerData.OrderBy(x => x.Salary).Select(x => new { x.Salary, x.ProjectName }).ToList();
                var salarySortDesc = managerData.OrderByDescending(x => x.Salary).ToList();

                var mayurData = managerData.FirstOrDefault(x => x.ManagerName == "MAYUR YEOLE");
                try
                {
                    var mayurExcData = managerData.First(x => x.ManagerName == "MAYUR YEOLE");
                }
                catch (Exception ex)
                {
                }

                var onlyDesignation = managerData.Select(x => x.ManagerDesignation).ToList();

                // If you want to create an anonymous object with both Name and Salary, use:
                var managerInfo = managerData.Select(x => new { x.ManagerName, x.Salary }).ToList();

                var totSalary = managerData.Sum(x => x.Salary);
                var avgSalary = managerData.Average(x => x.Salary);

                var taxProjManager = managerData.Any(x => x.ProjectName == "tax");
                var taxAllManager = managerData.All(x => x.ProjectName == "tax");

                var taxProjManagerDesc = managerData.Where(x => x.ProjectName=="tax").ToList();

                // grp by prject name
                var projectGroup = managerData.GroupBy(x => x.ProjectName).Select(x => x).ToList();
                var projectNamesGroup = managerData.GroupBy(x => x.ProjectName).Select(grp => new { projectname = grp.Key, Count = grp.Count() });

                response.FormData = _mapper.Map<ManagerModel[]>(managerData);
                return response;
            }
        }

        // AutoMapper profile to map ManagerData to ManagerModel
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<ManagerData, ManagerModel>();
            }
        }
    }
}
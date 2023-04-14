using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetEmployeeListByDepId
{
    public class GetEmployeeListByDepIdQueryHandler : IRequestHandler<GetEmployeeListByDepIdQuery, IEnumerable<GetEmployeeListByDepIdVm>>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public GetEmployeeListByDepIdQueryHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetEmployeeListByDepIdVm>> Handle(GetEmployeeListByDepIdQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = await _departmentTeamRepository.GetEmployeeListInDepartmemt(request.BankId, request.UserId);
            IEnumerable<GetEmployeeListByDepIdVm> getAllEmployees = _mapper.Map<IEnumerable<GetEmployeeListByDepIdVm>>(allEmployees);
            return (getAllEmployees);

        }
    }
}

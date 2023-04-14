using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetTeamLeader
{
    public class GetTeamLeaderQueryHandler : IRequestHandler<GetTeamLeaderQuery, GetTeamLeaderQueryVm>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public GetTeamLeaderQueryHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }
        public async Task<GetTeamLeaderQueryVm> Handle(GetTeamLeaderQuery request, CancellationToken cancellationToken)
        {
            var teamLeader = await _departmentTeamRepository.GetTeamLeaderDetails(request.TeamLeadId);
            GetTeamLeaderQueryVm teamLeaderDetails = _mapper.Map<GetTeamLeaderQueryVm>(teamLeader);
            return (teamLeaderDetails);
        }
    }

}

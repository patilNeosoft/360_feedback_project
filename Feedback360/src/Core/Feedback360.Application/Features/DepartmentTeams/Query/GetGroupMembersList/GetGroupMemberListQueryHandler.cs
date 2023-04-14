using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetGroupMembersList
{
    public class GetGroupMemberListQueryHandler : IRequestHandler<GetGroupMemberListQuery, IEnumerable<GetGroupMemberListVm>>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public GetGroupMemberListQueryHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetGroupMemberListVm>> Handle(GetGroupMemberListQuery request, CancellationToken cancellationToken)
        {
            var allMembersInGroup = await _departmentTeamRepository.GetTeamMembers(request.TeamLeadUserId);
            IEnumerable<GetGroupMemberListVm> getAllMembers = _mapper.Map<IEnumerable<GetGroupMemberListVm>>(allMembersInGroup);
            return (getAllMembers);
        }

    }
}

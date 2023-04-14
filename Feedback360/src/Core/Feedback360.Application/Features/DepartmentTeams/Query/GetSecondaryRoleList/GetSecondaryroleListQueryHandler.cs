using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetSecondaryRoleList
{
    
    public class GetSecondaryroleListQueryHandler: IRequestHandler<GetSecondaryroleListQuery, IEnumerable<GetSecondaryRoleListVm>>
    {

        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public GetSecondaryroleListQueryHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetSecondaryRoleListVm>> Handle(GetSecondaryroleListQuery request, CancellationToken cancellationToken)
        {
            var res = await _departmentTeamRepository.ListAllSecondaryRoles();
            var bankList = _mapper.Map<IEnumerable<GetSecondaryRoleListVm>>(res);
            return (bankList);
           

        }
    }

}

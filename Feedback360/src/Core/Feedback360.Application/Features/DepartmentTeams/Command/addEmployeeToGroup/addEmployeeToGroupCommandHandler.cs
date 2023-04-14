using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Command.addEmployeeToGroup
{
    public class addEmployeeToGroupCommandHandler: IRequestHandler<addEmployeeToGroupCommand, Response<bool>>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public addEmployeeToGroupCommandHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(addEmployeeToGroupCommand request, CancellationToken cancellationToken)
        {
            var memberToAdd = _mapper.Map<DepartmentTeam>(request);
            int bannerAdded = await _departmentTeamRepository.AddUserToTeam(memberToAdd);
            if(bannerAdded == 1)
            {
                return new Response<bool>(true);

            }
            return new Response<bool>(false);
        }
    }
  
}

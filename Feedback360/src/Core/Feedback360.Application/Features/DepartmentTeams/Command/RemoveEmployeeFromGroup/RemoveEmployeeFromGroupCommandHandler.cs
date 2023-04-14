using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Exceptions;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Command.RemoveEmployeeFromGroup
{
    public class RemoveEmployeeFromGroupCommandHandler: IRequestHandler<RemoveEmployeeFromGroupCommand, Response<bool>>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public RemoveEmployeeFromGroupCommandHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(RemoveEmployeeFromGroupCommand request,CancellationToken cancellationToken)
        {
            int result = 0;
            int depTeamId = request.DeptTeamId;
            var depTeamMemberToRemove = await _departmentTeamRepository.FindTeamMember(depTeamId);
            if (depTeamMemberToRemove == null)
            {
                throw new NotFoundException(nameof(depTeamMemberToRemove), depTeamId);

            }
            else
            {
                result = await _departmentTeamRepository.RemoveTeamMember(depTeamMemberToRemove);

            }
            if (result == 1)
            {
                return new Response<bool>(true);
            }
            else
            {
                return new Response<bool>(false);
            }
        }

    }
}

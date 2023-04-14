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

namespace Feedback360.Application.Features.DepartmentTeams.Command.DropGroup
{
    public class DropGroupCommandHandler : IRequestHandler<DropGroupCommand, Response<bool>>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public DropGroupCommandHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DropGroupCommand request, CancellationToken cancellationToken)
        {
            int teamLeadId = request.TeamLeadId;
            var depTeamMemberToRemove = await _departmentTeamRepository.DropGroup(teamLeadId);
            return new Response<bool>(depTeamMemberToRemove);
        }
    }

    }

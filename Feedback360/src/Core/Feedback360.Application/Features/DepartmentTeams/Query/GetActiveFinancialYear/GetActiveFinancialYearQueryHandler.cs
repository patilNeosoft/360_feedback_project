using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetActiveFinancialYear
{
    public class GetActiveFinancialYearQueryHandler: IRequestHandler<GetActiveFinancialYearQuery, GetActiveFinancialYearVm>
    {
        private readonly IDepartmentTeamRepository _departmentTeamRepository;
        public IMapper _mapper;
        public GetActiveFinancialYearQueryHandler(IMapper mapper, IDepartmentTeamRepository departmentTeamRepository)
        {
            _departmentTeamRepository = departmentTeamRepository;
            _mapper = mapper;
        }
        public async Task<GetActiveFinancialYearVm> Handle(GetActiveFinancialYearQuery request, CancellationToken cancellationToken)
        {
            var financialYear = await _departmentTeamRepository.GetCurrentFinancialYear();
            GetActiveFinancialYearVm getActiveFinancialYearVm = _mapper.Map<GetActiveFinancialYearVm>(financialYear);
            return (getActiveFinancialYearVm);
        }
    }

}

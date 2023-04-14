using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Queries.GetReporteeByBankId
{
    public class GetReporteeByBankIdQueryHandler : IRequestHandler<GetReporteeByBankIdQuery, List<GetReporteeByBankIdVM>>
    {
        private readonly IUserAuthorityRepository _userAuthorityRepository;
        private IMapper _mapper;
        public GetReporteeByBankIdQueryHandler(IUserAuthorityRepository userAuthorityRepository, IMapper mapper)
        {
            _userAuthorityRepository = userAuthorityRepository;
            _mapper = mapper;
        }

        public async Task<List<GetReporteeByBankIdVM>> Handle(GetReporteeByBankIdQuery request, CancellationToken cancellationToken)
        {
            int bankId = request.BankId;
            var users = await _userAuthorityRepository.GetReporteeByBankId(bankId);
            var mapObject = _mapper.Map<List<GetReporteeByBankIdVM>>(users);
            return (mapObject);
        }
    }
}

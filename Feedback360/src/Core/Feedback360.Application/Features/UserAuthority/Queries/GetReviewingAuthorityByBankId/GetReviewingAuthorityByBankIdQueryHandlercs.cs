using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Queries.GetReviewingAuthorityByBankId
{
    public class GetReviewingAuthorityByBankIdQueryHandlercs : IRequestHandler<GetReviewingAuthorityByBankIdQuery, List<GetReviewingAuthorityByBankIdVM>>
    {
        private readonly IUserAuthorityRepository _userAuthorityRepository;
        private IMapper _mapper;
        public GetReviewingAuthorityByBankIdQueryHandlercs(IUserAuthorityRepository userAuthorityRepository, IMapper mapper)
        {
            _userAuthorityRepository = userAuthorityRepository;
            _mapper = mapper;
        }

        public async Task<List<GetReviewingAuthorityByBankIdVM>> Handle(GetReviewingAuthorityByBankIdQuery request, CancellationToken cancellationToken)
        {
            int bankId = request.BankId;
            var users = await _userAuthorityRepository.GetReviewingAuthorityByBankId(bankId);
            var mapObject = _mapper.Map<List<GetReviewingAuthorityByBankIdVM>>(users);
            return (mapObject);
        }
    }
}

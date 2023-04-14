using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Queries.GetUsersByBankId
{
    public class GetUsersByBankIdQueryHandler : IRequestHandler<GetUsersByBankIdQuery, List<GetUsersByBankIdVM>>
    {
        private readonly IUserAuthorityRepository _userAuthorityRepository;
        private IMapper _mapper;
        public GetUsersByBankIdQueryHandler(IUserAuthorityRepository userAuthorityRepository, IMapper mapper)
        {
            _userAuthorityRepository = userAuthorityRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUsersByBankIdVM>> Handle(GetUsersByBankIdQuery request, CancellationToken cancellationToken)
        {
            int bankId = request.BankId;
            var users = await _userAuthorityRepository.FindUsersByBankId(bankId);
            var mapObject = _mapper.Map<List<GetUsersByBankIdVM>>(users);
            return (mapObject);
        }
    }
}

using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banks.Query
{
    public class GetBankListQueryHandler : IRequestHandler<GetBankListQuery, Response<IEnumerable<GetBankListVM>>>
    {

        private readonly IBankRepository _bankRepository;
        private readonly IMapper _mapper;
        public GetBankListQueryHandler(IBankRepository bankRepository, IMapper mapper)
        {
            _bankRepository = bankRepository;   
            _mapper = mapper;

        }

        public async Task<Response<IEnumerable<GetBankListVM>>> Handle(GetBankListQuery request, CancellationToken cancellationToken)
        {
            var res = await _bankRepository.ListAllAsync();
            var bankList = _mapper.Map<List<GetBankListVM>>(res);
            var response = new Response<IEnumerable<GetBankListVM>>(bankList);
            return response;

        }
    }
}

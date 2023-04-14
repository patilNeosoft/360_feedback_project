using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using Feedback360.Infrastructure.EncryptDecrypt;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackAdmin.Commands.RegisterAdmin
{
    public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, Response<RegisterAdminDto>>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterAdminCommandHandler> _logger;
        public RegisterAdminCommandHandler(IAdminRepository adminRepository, IMapper mapper, ILogger<RegisterAdminCommandHandler> logger)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<RegisterAdminDto>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            request.Password = EncryptionDecryption.EncryptString(request.Password);
            var adminUser = _mapper.Map<User>(request);
            var userExists = await _adminRepository.GetUserByIdAsync(request.EmployeeId);
            if(userExists!=null)
            {
                throw new Exception("User already exists");
            }
            var adminResponse = await _adminRepository.AddAsync(adminUser);
            var adminResponseMap = _mapper.Map<RegisterAdminDto>(adminResponse);
            return new Response<RegisterAdminDto>() { Data = adminResponseMap, Succeeded = true, Message = "A new Admin is Created" };
        }
    }
}

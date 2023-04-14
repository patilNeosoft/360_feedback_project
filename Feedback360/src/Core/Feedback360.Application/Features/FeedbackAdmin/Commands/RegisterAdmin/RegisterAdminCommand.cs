using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackAdmin.Commands.RegisterAdmin
{
    public class RegisterAdminCommand : IRequest<Response<RegisterAdminDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmployeeId { get; set; }
        public int RoleId { get; set; }
        public int BankId { get; set; }
        public string ContactNumber { get; set; }
        public string Organization { get; set; }
        public DateTime JoiningDate { get; set; }

    }
}

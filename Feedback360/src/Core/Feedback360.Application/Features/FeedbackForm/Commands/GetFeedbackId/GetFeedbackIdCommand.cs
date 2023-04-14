using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Commands.GetFeedbackId
{
    public class GetFeedbackIdCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int BankId { get; set; }
        public int FYId { get; set; }

    }
}

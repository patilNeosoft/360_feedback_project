using Feedback360.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Queries.GetRevaUserFeedbacks
{
    public class GetRevaUserFeedbackQuery: IRequest<List<PresentUserFeedbacks>>
    {
        public int UserId { get; set; }
    }
}

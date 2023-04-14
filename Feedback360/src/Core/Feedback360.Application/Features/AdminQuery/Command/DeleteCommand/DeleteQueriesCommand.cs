using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.Command.DeleteCommand
{
    public class DeleteQueriesCommand: IRequest<Response<bool>>
    {
       public List<int>  QueryId { get; set; }
    }
}

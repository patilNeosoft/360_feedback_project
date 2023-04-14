using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Command.DeleteUserQuery
{
    public class DeleteUserQueryCommand: IRequest<bool>
    {
        public int QueryId { get; set; }
    }
    
}

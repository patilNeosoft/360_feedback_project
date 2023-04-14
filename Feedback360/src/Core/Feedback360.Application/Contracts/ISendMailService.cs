using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts
{
    public interface ISendMailService
    {
        public bool SendMail(EmailEntity emailEntity);
    }
}

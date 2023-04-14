using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(ApplicationDbContext dbContext, ILogger<Bank> logger) : base(dbContext, logger)
        {
        }
    }
}

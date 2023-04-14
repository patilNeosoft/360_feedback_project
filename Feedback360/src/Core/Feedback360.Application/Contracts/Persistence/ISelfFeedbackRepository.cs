using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface ISelfFeedbackRepository: IAsyncRepository<UserAuthorityMapping>
    {
        public Task<UserAuthorityMappingVm> FindAuthorityDataByUserId(int id);
        public Task<List<UserFeedbackDetailsVm>> FindFeedbackDetailsByUserId(int id);
        public Task<List<UserFeedbackDetailsVm>> FindAllFeedbacksByUser(int StartYear, int EndYear, int UserId);

        public Task<List<UserFeedbackSummaryVM>> GetUserFeedbackSummary(int UserId,int financialYear);

    }
}

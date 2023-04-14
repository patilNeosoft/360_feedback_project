using Feedback360.Application.Features.UserAuthority.Queries.GetUsersByBankId;
using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IUserAuthorityRepository : IAsyncRepository<UserAuthorityMapping>
    {
        public Task<List<GetUsersByBankIdVM>> FindUsersByBankId(int bankId);
        public Task<bool> AddUserAuthority(UserAuthorityMapping userAuthorityMapList);
        public Task<bool> GetReportingAuthorityRedundant(int userId);
        public Task<bool> GetReviewingAuthorityRedundant(int userId);
        public UserAuthorityMapping FindUserAuthById(int userId);
        public int FindBankIdByUserId(int userId);
        public Task<List<User>> GetReporteeByBankId(int bankId);
        public Task<List<User>> GetReviewingAuthorityByBankId(int bankId);


    }
}

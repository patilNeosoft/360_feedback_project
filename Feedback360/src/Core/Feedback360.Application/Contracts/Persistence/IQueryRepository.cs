using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IQueryRepository:IAsyncRepository<Query>
    {
        public Task<List<Query>> GetAllAdminQuery();
        public Task<Query> GetQueryById(int QuerId);
        public  Task<bool> ResolveQuery(int QueryId, string CommentText,string rolename);
        public Task<bool> DeleteQueries(List<int> QueryId);
        public Task<List<Comment>> GetAllCommentsByQueryId(int QueryId);

        public Task<bool> SetStatus(int queryid);
        
    }
}

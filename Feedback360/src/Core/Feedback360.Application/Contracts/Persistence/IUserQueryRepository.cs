using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IUserQueryRepository: IAsyncRepository<Query>
    {
        public Task<IReadOnlyList<Query>> ListAllUserQueries();
        public List<Comment> FindComment(int id);
        public Task<bool> AddComment(Comment comment);
        public Task<Query> FindQueryByQueryId(int queryid);
        public Task<IEnumerable<Comment>> FindAllCommentsByQueryId(int queryId);
        public Task<bool> RemoveUserQueryAsync(Query query);
    }
}

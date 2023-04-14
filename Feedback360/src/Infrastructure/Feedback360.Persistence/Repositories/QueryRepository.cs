using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Query = Feedback360.Domain.Entities.Query;

namespace Feedback360.Persistence.Repositories
{
    public class QueryRepository : BaseRepository<Query>, IQueryRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        public QueryRepository(ApplicationDbContext dbContext, ILogger<Query> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Query>> GetAllAdminQuery()
        {
            List<Query> queryDetails = await _dbContext.Queries.ToListAsync();
            return queryDetails;
        }

        public async Task<Query> GetQueryById(int QueryId)
        {
            var result = await _dbContext.Queries.Where(x => x.QueryId == QueryId).FirstOrDefaultAsync();
            return result;

        }

        public async Task<bool> ResolveQuery(int QueryId,string CommentText,string rolename)
        {
          
            Comment comment = new Comment();
            comment.CommentText = CommentText;
            comment.QueryId= QueryId;
            comment.RoleName = rolename;
            _dbContext.Comments.AddAsync(comment);

            _dbContext.SaveChangesAsync();
            return true;   

        }

        public async Task<bool> DeleteQueries(List<int> QueryId)
        {
            foreach(var item in QueryId)
            {
               var res= await _dbContext.Queries.Where(x => x.QueryId == item).FirstOrDefaultAsync();
                _dbContext.Queries.Remove(res);
                _dbContext.SaveChanges();

            }
            return true;
        }

        public async Task<List<Comment>> GetAllCommentsByQueryId(int queryid)
        {
          List<Comment> result =  await _dbContext.Comments.Where(x => x.QueryId == queryid).ToListAsync();
            
            return result;
        }

        public async Task<bool> SetStatus(int queryid)
        {
            var result = await _dbContext.Queries.Where(x => x.QueryId == queryid).FirstOrDefaultAsync();
            result.QueryStatus = true;
            _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

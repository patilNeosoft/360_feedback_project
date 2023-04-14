using AutoMapper;
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

namespace Feedback360.Persistence.Repositories
{
    public class UserQueryRepository : BaseRepository<Query>, IUserQueryRepository
    {

        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        public UserQueryRepository(IMapper mapper, ApplicationDbContext dbContext, ILogger<Query> logger) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Query>> ListAllUserQueries()
        {
            List<Query> queries = await _dbContext.Queries.Where(u=> u.IsDeleted == false).ToListAsync();
            return queries;
        }
        public List<Comment> FindComment(int id)
        {
            List<Comment> comments = _dbContext.Comments.Where(u => u.QueryId == id).ToList();
            return comments;
        }
        public async Task<Query> FindQueryByQueryId(int queryid)
        {
            Query query = await _dbContext.Queries.Where(u => u.QueryId == queryid && u.IsDeleted == false).FirstOrDefaultAsync();
            return query;
        }

        public async Task<bool> AddComment(Comment comment)
        {
                await _dbContext.Set<Comment>().AddAsync(comment);
                int result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
                return false;
        }
        public async Task<IEnumerable<Comment>> FindAllCommentsByQueryId(int queryId)
        {
         IEnumerable<Comment> comments = await _dbContext.Comments.Where(u => u.QueryId == queryId).ToListAsync();
         return comments;
        }

        public async Task<bool> RemoveUserQueryAsync(Query query)
        {
            query.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

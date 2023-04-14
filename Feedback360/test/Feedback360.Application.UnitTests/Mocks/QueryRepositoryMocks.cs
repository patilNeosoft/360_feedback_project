using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Query = Feedback360.Domain.Entities.Query;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class QueryRepositoryMocks
    {
        public static Mock<IQueryRepository> GetQueryRepository()
        {
            var mockQueryRepository = new Mock<IQueryRepository>();

            var queries = new List<Query>
            {
                new Query
                {
                  QueryId = 1,
                  QueryTitle = "Title 1",
                  QueryStatus = true,
                  Description = "description",
                  IsDeleted = false,
                  UserId = 1
                }
            };

            var comments = new List<Comment>
            {
                new Comment
                {
                  QueryId = 1,
                  CommentId = 1,
                  CommentText = "text1",
                   RoleName = "User",
                }
            };

          Comment c=  new Comment
            {
                QueryId = 2,
                CommentId = 2,
                CommentText = "text1",
                RoleName = "Admin",
            };
            //mockQueryRepository.Setup(repo => repo.ResolveQuery(<int,string,string>(QueryId,CommentText,rolename)).ReturnsAsync(

            //      (bool res)  =>
            //      {
            //          Comment comment = new Comment();
            //          comment.QueryId = 2;
            //          comment.CommentText = "abc";
            //          comment.RoleName = "User";
            //          comment.CommentId = 2;
            //          comments.Add(comment);
            //          res=true;
            //          return res;


            //      });

            mockQueryRepository.Setup(repo => repo.GetAllAdminQuery()).ReturnsAsync(queries);
            return mockQueryRepository;
        }
    }
}

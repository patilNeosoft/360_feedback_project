using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class UserQueryRepositoryMocks
    {
        public static Mock<IUserQueryRepository> UserQueryRepository()
        {
            var mockUserQueryRepository = new Mock<IUserQueryRepository>();
            var userQueries = new List<Query>
            {
                new Query
                {
                  QueryId = 1,
                  QueryTitle = "Title 1",
                  QueryStatus = true,
                  Description = "description",
                  UserId = 1
                }
            };

            var userComments = new List<Comment>
            {
                new Comment
                {
                  QueryId = 1,
                  CommentId = 1,
                  CommentText = "text1",
                  RoleName = "User"
                }
            };

            //add new user query
            mockUserQueryRepository.Setup(repo => repo.AddAsync(It.IsAny<Query>())).ReturnsAsync(

                (Query query) =>
                {
                    Query newQuery = new Query();
                    newQuery.QueryTitle = "Test1";
                    newQuery.Description = "description1";
                    newQuery.QueryStatus = true;
                    newQuery.UserId = 1;
                    newQuery.QueryId = 2;
                    userQueries.Add(query);
                    return newQuery;
                });

            //get user query by Id
            mockUserQueryRepository.Setup(repo => repo.FindQueryByQueryId(It.IsAny<int>())).ReturnsAsync(
            (int QueryId) =>
            {
                return userQueries.SingleOrDefault(x => x.QueryId == 1);
            });

            //get user queries by user id
            mockUserQueryRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(userQueries);

            //add comment
            mockUserQueryRepository.Setup(repo => repo.AddComment(It.IsAny<Comment>())).ReturnsAsync(
            (bool res) =>
            {
                Comment newComment = new Comment();
                newComment.CommentText = "Text2";
                newComment.RoleName = "User";
                newComment.QueryId = 1;
                userComments.Add(newComment);
                res = true;
                return res;
            });

            //find all comments by query id
            mockUserQueryRepository.Setup(repo => repo.FindAllCommentsByQueryId(It.IsAny<int>())).ReturnsAsync(
            (int QueryId) =>
            {
                return userComments.FindAll(x => x.QueryId == 1);
            });
            return mockUserQueryRepository;
        }
    }
}
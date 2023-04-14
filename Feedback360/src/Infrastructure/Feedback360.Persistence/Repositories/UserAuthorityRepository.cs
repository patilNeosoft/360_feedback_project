using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserAuthority.Queries.GetUsersByBankId;
using Feedback360.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class UserAuthorityRepository : BaseRepository<UserAuthorityMapping>, IUserAuthorityRepository
    {
        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        IConfiguration _configuration;
        ILogger<UserAuthorityMapping> _logger;

        public UserAuthorityRepository(IMapper mapper, ApplicationDbContext dbContext, ILogger<UserAuthorityMapping> logger, IConfiguration configuration) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<List<GetUsersByBankIdVM>> FindUsersByBankId(int bankId)
        {
            List<GetUsersByBankIdVM> userList = new List<GetUsersByBankIdVM>();
            string connectionString = Convert.ToString(_configuration.GetConnectionString("ApplicationConnectionString"));
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            try
            {
                command.Connection = conn;
                command.CommandText = "STP_GetUsersByBankId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BankId", bankId);
                await conn.OpenAsync();
                SqlDataReader sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    GetUsersByBankIdVM userListVM = new GetUsersByBankIdVM();
                    userListVM.UserId = (int) sqlDataReader["Id"];
                    userListVM.FirstName = sqlDataReader["FirstName"].ToString();
                    userListVM.LastName = sqlDataReader["LastName"].ToString();
                    userListVM.EmployeeId = sqlDataReader["EmployeeId"].ToString();
                    userListVM.ReportingAuthority = Convert.IsDBNull(sqlDataReader["ReportingAuthority"]) ? null : (int?)sqlDataReader["ReportingAuthority"];
                    if(userListVM.ReportingAuthority == null || userListVM.ReportingAuthority == 0)
                    {
                        userListVM.ReportingAuthority_FirstName = null;
                        userListVM.ReportingAuthority_LastName = null;
                    }
                    else
                    {
                        var user = FindUserByUserId(userListVM.ReportingAuthority);
                        userListVM.ReportingAuthority_FirstName = user.FirstName;
                        userListVM.ReportingAuthority_LastName = user.LastName;

                    }
                    userListVM.ReviewingAuthority = Convert.IsDBNull(sqlDataReader["ReviewingAuthority"]) ? null : (int?)sqlDataReader["ReviewingAuthority"];
                    if (userListVM.ReviewingAuthority == null || userListVM.ReviewingAuthority == 0)
                    {
                        userListVM.ReviewingAuthority_FirstName = null;
                        userListVM.ReviewingAuthority_LastName = null;
                    }
                    else
                    {
                        var user = FindUserByUserId(userListVM.ReviewingAuthority);
                        userListVM.ReviewingAuthority_FirstName = user.FirstName;
                        userListVM.ReviewingAuthority_LastName = user.LastName;

                    }
                    userList.Add(userListVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            finally
            {
                await conn.CloseAsync();
            }
            return userList;
        }
        public async Task<bool> AddUserAuthority(UserAuthorityMapping userAuthorityMap)
        {
            try
            {
                var reportingAuthorityStatus = GetReportingAuthorityRedundant(userAuthorityMap.UserId);
                var reviewingAuthorityStatus = GetReviewingAuthorityRedundant(userAuthorityMap.UserId);
                if (userAuthorityMap.ReportingAuthority != userAuthorityMap.ReviewingAuthority && userAuthorityMap.UserId != userAuthorityMap.ReportingAuthority && userAuthorityMap.UserId != userAuthorityMap.ReviewingAuthority && await reportingAuthorityStatus == false && await  reviewingAuthorityStatus == false)
                {
                    await _dbContext.AddAsync(userAuthorityMap);
                    _dbContext.SaveChangesAsync();
                    return true;
                }
                else if (await reportingAuthorityStatus == false && await reviewingAuthorityStatus == true)
                {
                    var updateEntity = FindUserAuthById(userAuthorityMap.UserId);
                    updateEntity.ReportingAuthority = userAuthorityMap.ReportingAuthority;
                    _dbContext.Entry(updateEntity).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else if (await reportingAuthorityStatus == true && await reviewingAuthorityStatus == false)
                {
                    var updateEntity = FindUserAuthById(userAuthorityMap.UserId);
                    updateEntity.ReviewingAuthority = userAuthorityMap.ReviewingAuthority;
                    _dbContext.Entry(updateEntity).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else if(await reportingAuthorityStatus == true && await reviewingAuthorityStatus == true)
                {
                    var updateEntity = FindUserAuthById(userAuthorityMap.UserId);
                    updateEntity.ReportingAuthority = userAuthorityMap.ReportingAuthority;
                    updateEntity.ReviewingAuthority = userAuthorityMap.ReviewingAuthority;
                    //_dbContext.Entry(updateEntity).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else                
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<bool> GetReportingAuthorityRedundant(int userId)
        {
            int userBankId = FindBankIdByUserId(userId);
            //List<GetUsersByBankIdVM> userList = new List<GetUsersByBankIdVM>();
            //userList = await FindUsersByBankId(userBankId);
            //change hereeeee access the  list and then find user
            var userList = await FindUsersByBankId(userBankId);
            var user = (GetUsersByBankIdVM) userList.Where(u => u.UserId == userId).FirstOrDefault();
            if (user.ReportingAuthority == null || user.ReportingAuthority == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> GetReviewingAuthorityRedundant(int userId)
        {
            int userBankId = FindBankIdByUserId(userId);
            //List<GetUsersByBankIdVM> userList = new List<GetUsersByBankIdVM>();
            var userList = await FindUsersByBankId(userBankId);
            var user = (GetUsersByBankIdVM) userList.Where(u => u.UserId == userId).FirstOrDefault();
            if (user.ReviewingAuthority == null || user.ReviewingAuthority == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<List<User>> GetReporteeByBankId(int bankId)
        {
            return await _dbContext.Users.Where(u => u.BankId == bankId && u.RoleId == 13).ToListAsync();
            
        }
        public async Task<List<User>> GetReviewingAuthorityByBankId(int bankId)
        {
            return await _dbContext.Users.Where(u => u.BankId == bankId && u.RoleId == 14).ToListAsync();
            
        }
        public UserAuthorityMapping FindUserAuthById(int userId)
        {
            var userExists = _dbContext.UserAuthorityMappings.Where(u => u.UserId == userId).FirstOrDefault();
            if (userExists == null)
            {
                throw new Exception("User does not exist");
            }
            else
            {
                return userExists;
            }
        }
        public int FindBankIdByUserId(int userId)
        {
            var user = (User) _dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();
            if(user == null)
            {
                throw new Exception("User does not exist");
            }
            else
            {
                return user.BankId;
            }
        }
        public User FindUserByUserId(int? userId)
        {
            var user = (User)_dbContext.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            else
            {
                return user;
            }
        }
    }
}

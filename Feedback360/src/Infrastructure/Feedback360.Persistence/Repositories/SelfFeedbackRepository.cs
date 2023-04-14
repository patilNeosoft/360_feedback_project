using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class SelfFeedbackRepository : BaseRepository<UserAuthorityMapping>, ISelfFeedbackRepository
    {
        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        public SelfFeedbackRepository(IMapper mapper, ApplicationDbContext dbContext, ILogger<UserAuthorityMapping> logger) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<UserAuthorityMappingVm> FindAuthorityDataByUserId(int id)
        {
            UserAuthorityMapping userUserAuthority = await _dbContext.UserAuthorityMappings.Where(u => u.UserId == id).Include(x => x.User).ThenInclude(x => x.UserRole).FirstOrDefaultAsync();
            UserAuthorityMappingVm userAuthorityMappingVm = new UserAuthorityMappingVm();
            if(userUserAuthority == null)
            {
                User user = _dbContext.Users.Where(u => u.Id == id).Include(x => x.UserRole).FirstOrDefault();
                userAuthorityMappingVm.Organization = user.Organization;
                userAuthorityMappingVm.ReviewingAuthority ="UnAssigned";
                userAuthorityMappingVm.ReportingAuthority = "UnAssigned";
                userAuthorityMappingVm.PrimaryRole = user.UserRole.RoleName;
                userAuthorityMappingVm.UserId = id;
                FinancialYear yearobj = await _dbContext.FinancialYears.Where(x => x.IsActive == true).FirstOrDefaultAsync();
                if (yearobj != null)
                {
                    userAuthorityMappingVm.FinancialYear = yearobj.Id;
                    userAuthorityMappingVm.StartDate = yearobj.StartDate;
                    userAuthorityMappingVm.EndDate = yearobj.EndDate;
                    userAuthorityMappingVm.EndYear = yearobj.EndYear;
                    userAuthorityMappingVm.StartYear = yearobj.StartYear;
                }
                return userAuthorityMappingVm;
            }
            userAuthorityMappingVm.Organization = userUserAuthority.User.Organization;
            int ReportingAuthId = (int)userUserAuthority.ReportingAuthority;
            int ReviewingAuthId = (int)userUserAuthority.ReviewingAuthority;
            User ReviewingAuth = _dbContext.Users.Where(u => u.Id == ReviewingAuthId).FirstOrDefault();
            User ReportingAuth = _dbContext.Users.Where(u => u.Id == ReportingAuthId).FirstOrDefault();
            userAuthorityMappingVm.ReviewingAuthority = ReviewingAuth.FirstName + " " + ReviewingAuth.LastName;
            userAuthorityMappingVm.ReportingAuthority = ReportingAuth.FirstName + " " + ReportingAuth.LastName;
            userAuthorityMappingVm.PrimaryRole = userUserAuthority.User.UserRole.RoleName;
            userAuthorityMappingVm.UserId = id;
            FinancialYear year = await _dbContext.FinancialYears.Where(x => x.IsActive == true).FirstOrDefaultAsync();
            if (year != null)
            {
                userAuthorityMappingVm.StartDate = year.StartDate;
                userAuthorityMappingVm.EndDate = year.EndDate;
                userAuthorityMappingVm.EndYear = year.EndYear;
                userAuthorityMappingVm.StartYear = year.StartYear;
            }
            return userAuthorityMappingVm;
        }
        public async Task<List<UserFeedbackDetailsVm>> FindFeedbackDetailsByUserId(int id)
        {
            List<UserFeedbackDetailsVm> userFeedbackDetailsVmsList = new List<UserFeedbackDetailsVm>();
            int year = DateTime.Now.Year;
            DateTime date = DateTime.Now;
            FinancialYear fYear = _dbContext.FinancialYears.Where(u => u.StartYear == year || u.EndYear == year).FirstOrDefault(); ;
            FinancialYear fYear1 = _dbContext.FinancialYears.Where(u => u.IsActive == true).FirstOrDefault();

            if (fYear1 != null)
            {
                List<FeedbackAnswerForm> feedbackForm = await _dbContext.FeedbackForms.Where(u => u.UserId == id && (u.SelfCommentDate >= DateTime.Parse(fYear1.StartDate) && u.SelfCommentDate <= DateTime.Parse(fYear1.EndDate))).Include(u => u.Questionnaire).ToListAsync();
                User user = _dbContext.Users.Where(u => u.Id == id).Include(z => z.UserRole).FirstOrDefault();
                foreach (var item in feedbackForm)
                {
                    UserFeedbackDetailsVm userFeedbackDetailsVm = new UserFeedbackDetailsVm();
                    userFeedbackDetailsVm.FeedbackId = item.FeedbackId;
                    userFeedbackDetailsVm.UserName = user.FirstName + " " + user.LastName;
                    userFeedbackDetailsVm.Question = item.Questionnaire.Question;
                    userFeedbackDetailsVm.SubjectDescription = item.SubjectDescription;
                    userFeedbackDetailsVm.SelfScore = item.SelfScore;
                    userFeedbackDetailsVm.SelfComment = item.SelfComment;
                    userFeedbackDetailsVm.RoleName = user.UserRole.RoleName;
                    userFeedbackDetailsVmsList.Add(userFeedbackDetailsVm);

                }
                return userFeedbackDetailsVmsList;
            }
            return userFeedbackDetailsVmsList;
        }
       
        public async Task<List<UserFeedbackDetailsVm>> FindAllFeedbacksByUser(int StartYear, int EndYear,int UserId)
        {
            List<UserFeedbackDetailsVm> userFeedbackDetailsVmsList = new List<UserFeedbackDetailsVm>();
            
            List<FeedbackAnswerForm> userFeedbackDetailsList = await _dbContext.FeedbackForms.Where(u => (u.SelfCommentDate.Value.Year == StartYear || u.SelfCommentDate.Value.Year == EndYear) && (u.UserId == UserId)).Include(u => u.Questionnaire).ToListAsync();
            User user = _dbContext.Users.Where(u => u.Id == UserId).Include(z => z.UserRole).FirstOrDefault();
            foreach (var item in userFeedbackDetailsList)
            {
                UserFeedbackDetailsVm userFeedbackDetailsVm = new UserFeedbackDetailsVm();
                userFeedbackDetailsVm.FeedbackId = item.FeedbackId;
                userFeedbackDetailsVm.UserName = user.FirstName + " " + user.LastName;
                userFeedbackDetailsVm.Question = item.Questionnaire.Question;
                userFeedbackDetailsVm.SubjectDescription = item.SubjectDescription;
                userFeedbackDetailsVm.SelfScore = item.SelfScore;
                userFeedbackDetailsVm.SelfComment = item.SelfComment;
                userFeedbackDetailsVm.RoleName = user.UserRole.RoleName;
                userFeedbackDetailsVmsList.Add(userFeedbackDetailsVm);

            }
            return userFeedbackDetailsVmsList;
        }
        public async Task<List<UserFeedbackSummaryVM>> GetUserFeedbackSummary(int UserId,int financialYear)
        {
            List<UserFeedbackSummaryVM> userFeedbackSummaryVMList = new List<UserFeedbackSummaryVM>();
            User user = _dbContext.Users.Where(u => u.Id == UserId).Include(z => z.UserRole).FirstOrDefault();
            List<FinancialYear> fYear = await _dbContext.FinancialYears.ToListAsync();
            foreach(var item in fYear)
            {
                UserFeedbackSummaryVM userFeedbackSummaryVM = new UserFeedbackSummaryVM();
                DateTime startDate = DateTime.Parse(item.StartDate);
                DateTime endDate = DateTime.Parse(item.EndDate);

                List<FeedbackAnswerForm> feedbackForm = await _dbContext.FeedbackForms.Where(u => u.UserId == UserId && (u.SelfCommentDate >= startDate && u.SelfCommentDate <= endDate)).ToListAsync();
                userFeedbackSummaryVM.UserName = user.FirstName + " " + user.LastName;
                userFeedbackSummaryVM.EmployeeId = user.EmployeeId;
                userFeedbackSummaryVM.StartYear = item.StartYear;
                userFeedbackSummaryVM.EndYear = item.EndYear;
                foreach (var item1 in feedbackForm) {
                 userFeedbackSummaryVM.Status = (bool)item1.FeedbackStatus;
                 break;
                }
                userFeedbackSummaryVMList.Add(userFeedbackSummaryVM);
            }
            if (financialYear != 0)
            {
                return userFeedbackSummaryVMList.Where(u => u.StartYear == financialYear).ToList();
            }
            return userFeedbackSummaryVMList;
        }


    }
}

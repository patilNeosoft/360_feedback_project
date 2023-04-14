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
    public class FeedbackFormRepository : BaseRepository<Questionnaire>, IFeedbackFormRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        public FeedbackFormRepository(ApplicationDbContext dbContext, ILogger<Questionnaire> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Questionnaire>> FindQuestionsByBankId(int bankId)
        {
         return   await _dbContext.Questionnaires.Where(x => x.BankId == bankId && x.IsActive==true).ToListAsync();
        }

        public async Task<bool> AddFeedback(FeedbackAnswerForm feedbackForm)
        {
            await _dbContext.AddAsync(feedbackForm);
           int result= await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        //public async Task<string> FindUserIsRepaOrReva(int UserId)
        //{
        //  string status=string.Empty;
        //  var repo=  await _dbContext.UserAuthorityMappings.Where(x => x.ReportingAuthority==UserId).FirstOrDefaultAsync();
        //    if (repo != null)
        //    {
        //        status= "repo";
        //    }
        //    var reva = await _dbContext.UserAuthorityMappings.Where(x => x.ReviewingAuthority == UserId).FirstOrDefaultAsync();
        //    if(reva != null)
        //    {
        //        status = "reva";
        //    }
        //    return status;

        //}

        //public async Task<List<FeedbackForm>> ReporteeFeedbacks(int userId)
        //{
        //   List<UserAuthorityMapping> userList = await _dbContext.UserAuthorityMappings.Where(x => x.ReportingAuthority == userId).ToListAsync();
        //    List<FeedbackForm> repoFeedbacks = new List<FeedbackForm>();
        //   foreach(var user in userList)
        //    {
        //      var res=await _dbContext.FeedbackForms.Where(x=>x.UserId==user.UserId && x.ApprovedStatus==1).ToListAsync();
        //      repoFeedbacks.AddRange(res);
        //    }
        //    return repoFeedbacks;
        //}

        //public async Task<List<FeedbackForm>> ReviewingFeedbacks(int userId)
        //{
        //    List<UserAuthorityMapping> userList = await _dbContext.UserAuthorityMappings.Where(x => x.ReviewingAuthority == userId).ToListAsync();
        //    List<FeedbackForm> revFeedbacks = new List<FeedbackForm>();
        //    foreach (var user in userList)
        //    {
        //        var res = await _dbContext.FeedbackForms.Where(x => x.UserId == user.UserId && x.ApprovedStatus == 2).Include(x=>x.Questionnaire).ToListAsync();
        //        revFeedbacks.AddRange(res);
        //    }

        //    return revFeedbacks;
        //}


        public async Task<int> AddUserFeedbackMap(FeedbackUserMapping feedbackUserMapping)
        {
           await _dbContext.FeedbackUserMappings.AddAsync(feedbackUserMapping);
       int res=   await _dbContext.SaveChangesAsync();
            if (res == 1)
            {
              FeedbackUserMapping feedbackid=  _dbContext.FeedbackUserMappings.Where(x => x.UserId == feedbackUserMapping.UserId && x.FYId == feedbackUserMapping.FYId).FirstOrDefault();
                return feedbackid.UserFeedbackId;
            }
            else
            {
                return 0;
            }
         


        }

        //you can change here in mappinglistfor sortingwith fyid
        //RepaUserFeedbackList
        public async Task<List<PresentUserFeedbacks>> RepaUserList(int userId)
        {
           List<UserAuthorityMapping> userAuthMapList= _dbContext.UserAuthorityMappings.Where(x => x.ReportingAuthority == userId).ToList();
            List<PresentUserFeedbacks> presentUserFeedbackList = new List<PresentUserFeedbacks>();
            foreach(var i in userAuthMapList)
            {
              var presentUser=  _dbContext.FeedbackUserMappings.Where(x => x.UserId == i.UserId).Include(x => x.User).Include(x=>x.FinancialYear).FirstOrDefault();
                var approvedstatus = await _dbContext.FeedbackForms.Where(x => x.UserId == i.UserId && x.ApprovedStatus == 1).FirstOrDefaultAsync();
                if (approvedstatus != null)
                {
                    PresentUserFeedbacks presentUserFeedbacks = new PresentUserFeedbacks();
                    presentUserFeedbacks.UserId = presentUser.UserId;
                    presentUserFeedbacks.UserFeedbackId = presentUser.UserFeedbackId;
                    presentUserFeedbacks.FirstName = presentUser.User.FirstName;
                    presentUserFeedbacks.LastName = presentUser.User.LastName;
                    presentUserFeedbacks.EmployeeId = presentUser.User.EmployeeId;
                    presentUserFeedbacks.StartYear=presentUser.FinancialYear.StartYear;
                    presentUserFeedbacks.EndYear = presentUser.FinancialYear.EndYear;

                    presentUserFeedbackList.Add(presentUserFeedbacks);
                }
            }
            return presentUserFeedbackList;

        }


        public async Task<List<FeedbackAnswerFormVM>> RepaEditFeedBackForm(int feedbackId)
        {

           List<FeedbackAnswerForm> feedbackslist=  await _dbContext.FeedbackForms.Where(x => x.FeedbackId == feedbackId).Include(x=>x.Questionnaire).ToListAsync();
           List<FeedbackAnswerFormVM> feedbackAnswerFormVMs = new List<FeedbackAnswerFormVM>();
           foreach(var i in feedbackslist)
            {
              FeedbackAnswerFormVM feedbackAnswerFormVM = new FeedbackAnswerFormVM();
              feedbackAnswerFormVM.FeedbackId = i.FeedbackId;
              feedbackAnswerFormVM.UserId = i.UserId;
                feedbackAnswerFormVM.BankId = i.BankId;
                feedbackAnswerFormVM.QuestionId = i.QuestionId;
                feedbackAnswerFormVM.Question = i.Questionnaire.Question;
                feedbackAnswerFormVM.SubjectDescription = i.SubjectDescription;
                feedbackAnswerFormVM.SelfComment = i.SelfComment;
                feedbackAnswerFormVM.SelfScore = i.SelfScore;
                feedbackAnswerFormVM.SelfCommentDate=i.SelfCommentDate; 
                feedbackAnswerFormVM.RepaComment = i.RepaComment;
                feedbackAnswerFormVM.RepaScore=i.RepaScore;
                feedbackAnswerFormVM.RepaCommentDate = i.RepaCommentDate;
                feedbackAnswerFormVM.ApprovedStatus = i.ApprovedStatus;
                feedbackAnswerFormVM.FeedbackStatus = i.FeedbackStatus;
                feedbackAnswerFormVMs.Add(feedbackAnswerFormVM);

            }
           return feedbackAnswerFormVMs;

        }

        public async Task<FeedbackAnswerForm> GetRepaFeedback(int questionId,int feedbackId)
        {
          return await  _dbContext.FeedbackForms.Where(x => x.FeedbackId == feedbackId && x.QuestionId == questionId).FirstOrDefaultAsync();
        }

        public async Task<bool> RepaUpdateAsync( FeedbackAnswerForm feedbackAnswerForm)
        {
           _dbContext.Entry(feedbackAnswerForm).State = EntityState.Modified;
          int res=  await _dbContext.SaveChangesAsync();
            if(res == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        public async Task<List<PresentUserFeedbacks>> RevaUserList(int userId)
        {
            List<UserAuthorityMapping> userAuthMapList = _dbContext.UserAuthorityMappings.Where(x => x.ReviewingAuthority == userId).ToList();
            List<PresentUserFeedbacks> presentUserFeedbackList = new List<PresentUserFeedbacks>();
            foreach (var i in userAuthMapList)
            {
                var presentUser = _dbContext.FeedbackUserMappings.Where(x => x.UserId == i.UserId).Include(x => x.User).Include(x => x.FinancialYear).FirstOrDefault();
              var approvedstatus= await _dbContext.FeedbackForms.Where(x => x.UserId == i.UserId && x.ApprovedStatus == 2).FirstOrDefaultAsync();
                if (approvedstatus != null)
                {
                    PresentUserFeedbacks presentUserFeedbacks = new PresentUserFeedbacks();
                    presentUserFeedbacks.UserId = presentUser.UserId;
                    presentUserFeedbacks.UserFeedbackId = presentUser.UserFeedbackId;
                    presentUserFeedbacks.FirstName = presentUser.User.FirstName;
                    presentUserFeedbacks.LastName = presentUser.User.LastName;
                    presentUserFeedbacks.EmployeeId = presentUser.User.EmployeeId;
                    presentUserFeedbacks.StartYear =presentUser.FinancialYear.StartYear;
                    presentUserFeedbacks.EndYear =presentUser.FinancialYear.EndYear;
                    presentUserFeedbackList.Add(presentUserFeedbacks);
                }
            }
            return presentUserFeedbackList;

        }

        public async Task<List<FeedbackAnswerFormVM>> RevaEditFeedBackForm(int feedbackId)
        {

            List<FeedbackAnswerForm> feedbackslist = await _dbContext.FeedbackForms.Where(x => x.FeedbackId == feedbackId).Include(x => x.Questionnaire).ToListAsync();
            List<FeedbackAnswerFormVM> feedbackAnswerFormVMs = new List<FeedbackAnswerFormVM>();
            foreach (var i in feedbackslist)
            {
                FeedbackAnswerFormVM feedbackAnswerFormVM = new FeedbackAnswerFormVM();
                feedbackAnswerFormVM.FeedbackId = i.FeedbackId;
                feedbackAnswerFormVM.UserId = i.UserId;
                feedbackAnswerFormVM.BankId = i.BankId;
                feedbackAnswerFormVM.QuestionId = i.QuestionId;
                feedbackAnswerFormVM.Question = i.Questionnaire.Question;
                feedbackAnswerFormVM.SubjectDescription = i.SubjectDescription;
                feedbackAnswerFormVM.SelfComment = i.SelfComment;
                feedbackAnswerFormVM.SelfScore = i.SelfScore;
                feedbackAnswerFormVM.SelfCommentDate = i.SelfCommentDate;
                feedbackAnswerFormVM.RepaComment = i.RepaComment;
                feedbackAnswerFormVM.RepaScore = i.RepaScore;
                feedbackAnswerFormVM.RepaCommentDate = i.RepaCommentDate;
                feedbackAnswerFormVM.RevaScore= i.RevaScore;
                feedbackAnswerFormVM.RevaComment = i.RevaComment;
                feedbackAnswerFormVM.RevaCommentDate = i.RevaCommentDate;
                feedbackAnswerFormVM.ApprovedStatus = i.ApprovedStatus;
                feedbackAnswerFormVM.FeedbackStatus = i.FeedbackStatus;
                feedbackAnswerFormVMs.Add(feedbackAnswerFormVM);

            }
            return feedbackAnswerFormVMs;

        }


    }
}

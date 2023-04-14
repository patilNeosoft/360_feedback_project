using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IFeedbackFormRepository: IAsyncRepository<Questionnaire>
    {
        public Task<List<Questionnaire>> FindQuestionsByBankId(int bankId);
        public Task<bool> AddFeedback(FeedbackAnswerForm feedbackForm);

        public Task<int> AddUserFeedbackMap(FeedbackUserMapping feedbackUserMapping);
        public  Task<List<PresentUserFeedbacks>> RepaUserList(int userId);

        public Task<List<FeedbackAnswerFormVM>> RepaEditFeedBackForm(int feedbackId);
        public  Task<FeedbackAnswerForm> GetRepaFeedback(int questionId, int feedbackId);
        public Task<bool> RepaUpdateAsync(FeedbackAnswerForm feedbackAnswerForm);

        public  Task<List<PresentUserFeedbacks>> RevaUserList(int userId);
        public Task<List<FeedbackAnswerFormVM>> RevaEditFeedBackForm(int feedbackId);
        //public Task<string> FindUserIsRepaOrReva(int UserId);
        //public  Task<List<FeedbackForm>> ReporteeFeedbacks(int userId);
        //public Task<List<FeedbackForm>> ReviewingFeedbacks(int userId);
    }
}

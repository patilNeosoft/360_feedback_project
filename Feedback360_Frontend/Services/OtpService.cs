using Feedback360_Frontend.IServices;

namespace Feedback360_Frontend.Services
{
    public class OtpService:IOtpService
    {
        public int GenerateOtp()
        {
            int _min = 100000;
            int _max = 999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public bool ValidateOtp(int userOtpCode,HttpContext context)
        {
            int sessionCode = int.Parse(context.Session.GetString("OtpCode"));
            var isValid = userOtpCode == sessionCode;
            context.Session.Remove("OtpCode");
            return isValid;
        }
    }
}

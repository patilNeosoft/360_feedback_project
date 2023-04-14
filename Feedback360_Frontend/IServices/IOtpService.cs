namespace Feedback360_Frontend.IServices
{
    public interface IOtpService
    {
        public int GenerateOtp();
        public bool ValidateOtp(int userOtpCode, HttpContext context);
    }
}

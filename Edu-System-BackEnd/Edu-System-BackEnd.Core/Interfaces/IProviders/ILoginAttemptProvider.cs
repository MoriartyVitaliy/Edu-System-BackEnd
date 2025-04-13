namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders
{
    public interface ILoginAttemptProvider
    {
        Task<bool> IsAccountLockedAsync(string email);
        Task RecordFailedLoginAsync(string email);
        Task RecordSuccessfulLoginAsync(string email);
    }
}

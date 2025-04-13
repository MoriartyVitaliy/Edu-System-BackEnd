using Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces.IProviders;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Providers
{
    public class LoginAttemptProvider : ILoginAttemptProvider
    {
        private readonly IDistributedCache _cache;
        private const int MaxFailedAttempts = 5;
        private const int LockoutTimeInMinutes = 1;
        private const string FailedAttemptsKeyPrefix = "FailedAttempts_"; // Префикс для ключа
        private const string BasePrefix = "EduSystem_";
        public LoginAttemptProvider(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<bool> IsAccountLockedAsync(string email)
        {
            var failedAttempts = await _cache.GetStringAsync(BasePrefix + FailedAttemptsKeyPrefix + email);

            if (failedAttempts != null)
            {
                int failedCount = int.Parse(failedAttempts);
                if (failedCount >= MaxFailedAttempts)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task RecordFailedLoginAsync(string email)
        {
            var failedAttempts = await _cache.GetStringAsync(FailedAttemptsKeyPrefix + email);

            int failedCount = string.IsNullOrEmpty(failedAttempts) ? 0 : int.Parse(failedAttempts);
            failedCount++;

            await _cache.SetStringAsync(FailedAttemptsKeyPrefix + email, failedCount.ToString());

            if (failedCount >= MaxFailedAttempts)
            {
                var lockoutTime = DateTime.UtcNow.AddMinutes(LockoutTimeInMinutes);
                await _cache.SetStringAsync(
                    FailedAttemptsKeyPrefix + email + "_Lockout",
                    lockoutTime.ToString(),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(LockoutTimeInMinutes) // устанавливаем TTL
                });

            }
        }

        public async Task RecordSuccessfulLoginAsync(string email)
        {
            await _cache.RemoveAsync(FailedAttemptsKeyPrefix + email);
            await _cache.RemoveAsync(FailedAttemptsKeyPrefix + email + "_Lockout");
        }
    }
}

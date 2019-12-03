using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FR.Identity.API.Model;
using Microsoft.AspNetCore.Identity;

namespace FR.Identity.API.Infrastructure
{
    public class AppUserStore: IUserStore<AppUser>, IUserPasswordStore<AppUser>
    {
        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            UserRepository.Users.Add(new AppUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                NormalizeUserName = user.NormalizeUserName,
                PasswordHash = user.PasswordHash,
                CompanyName = user.CompanyName
            });
 
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            var appUser = UserRepository.Users.FirstOrDefault(u => u.Id == user.Id);
 
            if (appUser != null)
            {
                UserRepository.Users.Remove(appUser);
            }
 
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(UserRepository.Users.FirstOrDefault(u => u.Id == userId));
        }

        public Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.FromResult(UserRepository.Users.FirstOrDefault(u => u.NormalizeUserName == normalizedUserName));
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizeUserName);
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizeUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            var appUser = UserRepository.Users.FirstOrDefault(u => u.Id == user.Id);
 
            if (appUser != null)
            {
                appUser.NormalizeUserName = user.NormalizeUserName;
                appUser.UserName = user.UserName;
                appUser.Email = user.Email;
                appUser.PasswordHash = user.PasswordHash;
                appUser.CompanyName = user.CompanyName;
            }
 
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }
    }
}
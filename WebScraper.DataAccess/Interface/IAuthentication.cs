using WebScraper.DataAccess.Model;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface IAuthentication
    {
        Task<UserProfile> GetUserProfileByEmail(string email);

        Task<UserProfile> GetUserProfile(UserProfile user);

        Task<bool> IsUserActive(string email);

        Task<long> Save(UserProfile user);

        Task<UserDetail> GetUserDetailByID(long UserID);
    }
}

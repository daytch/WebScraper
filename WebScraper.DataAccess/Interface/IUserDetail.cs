using WebScraper.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
   public interface IUserDetail
    {
        Task<long> SaveUserDetail(UserDetail detail);
        Task<UserDetail> SelectByUserProfileID(long id);
        Task<bool> Update(UserDetail detail);
    }
}

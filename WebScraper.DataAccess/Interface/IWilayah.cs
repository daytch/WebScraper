using WebScraper.DataAccess.Model;
using WebScraper.DataAccess.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface IWilayah
    {
        Task<List<WilayahViewModel>> GetAllWilayah();

        Task<WilayahViewModel> GetPost(long postId);

        Task<long> AddPost(Wilayah wilayah);

        Task<long> DeletePost(long roleID);

        Task<bool> UpdatePost(Wilayah wilayah);
    }
}

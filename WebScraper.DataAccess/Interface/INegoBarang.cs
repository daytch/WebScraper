using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.DataAccess.Model;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface INegoBarang
    {
        Task<NegoBarangResponse> GetAll();

        Task<NegoBarangResponse> GetPost(NegoBarangRequest model);

        Task<NegoBarangResponse> AddPost(NegoBarangRequest model);

        Task<long> AddPost(NegoBarang model);

        Task<NegoBarang> GetNegoBarang(NegoBarang model);

        Task<NegoBarangResponse> DeletePost(NegoBarangRequest model);

        Task<NegoBarangResponse> UpdatePost(NegoBarangRequest model);

        Task<long> UpdatePost(NegoBarang model);

        Task<NegoBarangResponse> GetAllASK(string search, string order, string orderDir, int startRec, int pageSize, int dra);

        Task<bool> DeletePost(long ID);

        Task<NegoBarangResponse> GetAllBID(string search, string order, string orderDir, int startRec, int pageSize, int draw);
        Task<NegoBarangResponse> GetAllASK(string search, string order, string orderDir, int startRec, int pageSize, int draw, long ID);

        Task<NegoBarangResponse> GetAllBID(string search, string order, string orderDir, int startRec, int pageSize, int draw, long KotaID);
    }
}

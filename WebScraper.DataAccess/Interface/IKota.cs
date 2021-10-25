using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface IKota
    {
        Task<List<Kota>> GetKotaByProvinsiID(int ProvinsiID);

        Task<List<Kota>> GetAll();

        Task<KotaResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw);

        Task<Kota> GetPost(KotaRequest request);

        Task<long> AddPost(Kota request);

        Task<KotaResponse> DeletePost(KotaRequest request);

        Task<bool> UpdatePost(Kota request);
        Task<List<Kota>> GetAllWithoutFilter();
    }
}

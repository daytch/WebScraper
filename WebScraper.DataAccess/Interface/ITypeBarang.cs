using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface ITypeBarang
    {
        Task<List<TypeBarang>> GetAllType();

        Task<TypeBarangResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw);

        Task<TypeBarangResponse> GetAllWithModelID(TypeBarangRequest request);

        Task<TypeBarangResponse> GetPost(TypeBarangRequest request);

        Task<long> Add(TypeBarang request);

        Task<TypeBarangResponse> AddPost(TypeBarang request);

        Task<TypeBarangResponse> DeletePost(TypeBarangRequest request);

        Task<TypeBarangResponse> UpdatePost(TypeBarangRequest request);

        Task<List<SP_TypeByKotaIDMerkIDModelID>> GetTypeByKotaIDMerkIDModelID(TypeBarangRequest request);

        Task<TypeBarang> GetTypeByName(TypeBarang request);
    }
}

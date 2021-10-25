using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.Common.ViewModel;
using WebScraper.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface IBarang
    {
        Task<List<Barang>> GetAllBarang();
        Task<bool> UpdateMany(List<Barang> ListBarang);
        Task<List<Barang>> GetAllBarangSameTypeAndKota(Barang barang);

        Task<BarangResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw);
                
        List<Car> GetHighestBid(string kota,int total);

        List<Car> GetLowestAsk(string kota, int total);

        Task<Position> GetAskPosition(int id, int nominal);

        Task<Position> GetBidPosition(int id, int nominal);

        List<Car> GetRelatedProducts(int id);

        List<CarAsks> GetAllAsksById(BarangRequest req);

        List<CarBids> GetAllBidsById(BarangRequest req);

        Task<CarDetail> GetBarangDetail(int id);        

        List<Car> GetListNormal(string kota, int total);

        Task<Barang> GetBarang(long ID);

        Task<long> AddPost(Barang model);

        Task<long> DeletePost(long ID, string username);

        Task<bool> UpdatePost(Barang model);
        Task<Barang> GetHargaOTR(Barang request);
        Task<Barang> GetIDBarangByTypeAndCOlour(Barang request);
        Task<List<SP_GetBarangByHomeParameter>> GetBarangByHomeParameter(BarangRequest request);
        Task<SP_GetBarangByHomeParameterCount> GetBarangByHomeParameterCount(BarangRequest request);
        Task<List<SP_GetPhotoAndWarnaByBarangID>> GetPhotoAndWarnaByID(BarangRequest request);
        Task<List<SP_GetTypeBarangByBarangID>> GetTypeBarangByBarangID(BarangRequest request);

        Task<Barang> GetHargaOTRTypeBarangID(long TypeBarangID);

        Task<List<Barang>> GetIDBarangByTypeAndColourIDS(long typeBarangID, List<long> listWarnaID);
    }
}

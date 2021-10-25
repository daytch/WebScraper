using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.Common.ViewModel;
using WebScraper.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface ICMS
    {
        Task<List<SP_CMSMaster>> GetAllByCategory(string Category);
        Task<long> SubmitCMS(CMSRequest model);
        Task<List<SP_CMSMaster>> GetAllCMSMaster();
        Task<List<CmsmasterData>> GetAll();
    }
}

using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface ITransaction
    {
        Task<Transaction> Select(Transaction transaction);
        Task<bool> Update(Transaction transaction);
        Task<TransactionResponse> GetAll(string search, string order, string orderDir, int startRec, int pageSize, int draw);

        Task<TransactionResponse> GetPost(TransactionRequest model);

        Task<TransactionResponse> AddPost(TransactionRequest model);

        Task<long> SaveTransaction(Transaction model);

        Task<TransactionResponse> DeletePost(TransactionRequest model);

        Task<TransactionResponse> UpdatePost(TransactionRequest model);
        Task<TransactionResponse> CancelTransaction(TransactionRequest req);
        Task<TransactionResponse> ApproveTransaction(TransactionRequest req);

        Task<TransactionResponse> GetJournalByTransaction(TransactionRequest req);

        Task<List<TransactionStatus>> GetAllStatus();

        Task<List<SP_ReportByStatusID>> GetReportByStatusID(long id, string endDate);
        Task<List<SP_GetAllBidByUserProfileID>> GetAllBidByUserProfileID(long UserProfileID);
        Task<List<SP_GetAllAskByUserProfileID>> GetAllAskByUserProfileID(long UserProfileID);
        Task<List<sp_GetAllBidAndBuyByUserProfileID>> GetAllBidAndBuyByUserProfileID(long UserProfileID);
        Task<UserProfile> GetUserProfile(string UserName);
        Task<UserDetail> GetUserDetail(long id);
    }
}

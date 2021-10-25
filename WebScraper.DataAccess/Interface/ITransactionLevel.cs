using WebScraper.Common.Requests;
using WebScraper.Common.Responses;
using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface ITransactionLevel
    {
        Task<TransactionLevelResponse> GetAll();

        Task<TransactionLevelResponse> GetCurrentLevel(TransactionLevelRequest request);

        Task<TransactionLevelResponse> GetNextLevel(TransactionLevelRequest request);

      
    }
}

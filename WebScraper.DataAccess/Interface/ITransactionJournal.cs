using WebScraper.Common.Responses;
using WebScraper.DataAccess.Model;
using WebScraper.DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.DataAccess.Interface
{
    public interface ITransactionJournal
    {

        Task<long> SaveTransactionJournal(TransactionJournal model);
        Task<TransactionResponse> GetHistory(long ID);
    }
}

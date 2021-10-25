using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class TransactionStatusResponse
    {
        public List<TransactionStatusViewModel> ListTransactionStatus { get; set; }

        public TransactionStatusResponse()
        {
            ListTransactionStatus = new List<TransactionStatusViewModel>();
        }
    }
}

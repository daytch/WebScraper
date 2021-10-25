using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class NegoBarangResponse : ResponseBase
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<NegoBarangViewModel> ListModel { get; set; }
        public NegoBarangResponse()
        {
            IsSuccess = true;
            ListModel = new List<NegoBarangViewModel>();
        }
    }
}

using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class ProvinsiResponse : ResponseBase
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ProvinsiViewModel> ListProvinsi { get; set; }

        public ProvinsiViewModel Model { get; set; }

        public List<Dropdown> ListProvinces { get; set; }

        public ProvinsiResponse()
        {
            ListProvinsi = new List<ProvinsiViewModel>();
        }
    }
}

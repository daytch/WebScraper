using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class KotaResponse : ResponseBase
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public KotaViewModel Model { get; set; }
        public List<KotaViewModel> ListKota { get; set; }
        public List<ProvinsiViewModel> ListProvinsi { get; set; }
        public List<Dropdown> ListKotas { get; set; }
        public KotaResponse()
        {
            Model = new KotaViewModel();
            ListKota = new List<KotaViewModel>();
            ListProvinsi = new List<ProvinsiViewModel>();
        }
    }
}

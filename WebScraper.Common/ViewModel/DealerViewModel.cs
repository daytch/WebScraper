using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.ViewModel
{
    public class DealerViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Kode { get; set; }
        public string Alamat { get; set; }
        public long KotaID { get; set; }
        public string KotaName { get; set; }
        public string Telepon { get; set; }
        public string PejabatDealer { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }
        public bool RowStatus { get; set; }
    }
}

using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Requests
{
    public class TypeBarangRequest : RequestBase
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long ModelBarangID { get; set; }
        public long KotaID { get; set; }
        public long MerkID { get; set; }
        public ModelBarangViewModel ModelBarang { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string Modifiedby { get; set; }
        public bool RowStatus { get; set; }
    }
}

using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class MerkResponse : BaseResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<MerkViewModel> ListModel { get; set; }

        public List<SP_MerkByKotaID> ListSP_MerkByKotaID { get; set; }
        public MerkViewModel Model { get; set; }

        public List<SP_MerkRank> ListSP_MerkRank { get; set; }


        public MerkResponse()
        {
            ListModel = new List<MerkViewModel>();
            ListSP_MerkRank = new List<SP_MerkRank>();
        }
    }

    public class SP_MerkByKotaID
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SP_MerkRank
    {
        public long ID { get; set; }
        public string MerkName { get; set; }
        public int MerkRank { get; set; }
        public int Total { get; set; }
    }
}

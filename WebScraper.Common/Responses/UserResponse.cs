using WebScraper.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class UserResponse:BaseResponse
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<SalesmanViewModel> datasalesman { get; set; }
        public List<BuyerViewModel> databuyer { get; set; }
        public UserProfileViewModel userProfileModel { get; set; }

        public UserResponse()
        {
            userProfileModel = new UserProfileViewModel();
        }
    }
}

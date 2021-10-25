using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class BaseResponse
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

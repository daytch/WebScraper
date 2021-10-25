using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class SignInResponse
    {
        public bool Succeeded { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsNotAllowed { get; set; }
        public bool RequiresTwoFactor { get; set; }
    }
}

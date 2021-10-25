using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Common.Responses
{
    public class RegisterResponse
    {
        public bool Succeeded { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}

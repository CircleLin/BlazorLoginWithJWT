using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BlazorLoginWithJWT.Shared
{
    public class UserToken
    {
        public HttpStatusCode StatusCode { get; set; }
        public string token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
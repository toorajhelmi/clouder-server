using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;

namespace Clouder.Server.Api.Util
{
    public static class HTTPIO
    {
        public static void AddDefaultCORSResponseHeaders(HttpRequest req)
        {
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET,OPTIONS");
        }

        
    }
}

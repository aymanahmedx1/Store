using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandelReponse
{
    public class Response
    {
        public Response(int stautsCode, string? message = null)
        {
            StautsCode = stautsCode;
            Message = message ?? GetStatusCodeMessage(stautsCode);
        }

        private string GetStatusCodeMessage(int stautsCode)
           => stautsCode switch
           {
               200 => "OK",
               201 => "Created",
               202 => "Accepted",
               204 => "No Content",
               301 => "Moved Permanently",
               302 => "Found",
               304 => "Not Modified",
               400 => "Bad Request",
               401 => "Unauthorized",
               403 => "Forbidden",
               404 => "Not Found",
               405 => "Method Not Allowed",
               409 => "Conflict",
               500 => "Internal Server Error",
               501 => "Not Implemented",
               502 => "Bad Gateway",
               503 => "Service Unavailable",
               _ => "Unknown"
           };

        public int StautsCode { get; set; }
        public string Message { get; set; }

    }
}

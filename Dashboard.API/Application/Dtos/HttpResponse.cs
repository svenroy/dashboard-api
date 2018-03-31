using System.Collections.Generic;
using System.Net;

namespace Dashboard.API.Application.Dtos
{
    public class HttpResponse<T>
    {
        public T Payload { get; set; }

        public List<string> Errors = new List<string>();

        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

        public HttpResponse() { }

        public HttpResponse(T payload)
        {
            Payload = payload;
        }
    }

    public class HttpResponse
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    }
}
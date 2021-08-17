using System.Net;

namespace Manager.API.ViewModels
{
    public class ResultViewModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get;set; }
        public dynamic Data { get; set; }
    }
}

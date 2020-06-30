using Microsoft.AspNetCore.Mvc;

namespace UrlsAndRoutes.Models
{
    public class HeaderModel
    {
        [FromHeader]
        public string Accept { get; set; }
        [FromHeader(Name = "Accept-Language")]
        public string AcceptLanguage { get; set; }
        [FromHeader(Name = "Accept-Encoding")]
        public string AcceptEncoding { get; set; }
    }
}

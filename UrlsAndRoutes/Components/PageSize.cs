﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace UrlsAndRoutes.Components
{
    public class PageSize : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://apress.com");

            return View(response.Content.Headers.ContentLength);
        }
    }
}

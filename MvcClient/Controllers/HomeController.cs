using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcClient.Models;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Net;

namespace MvcClient.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["refresh_token"] = this.HttpContext.GetTokenAsync("refresh_token");
            ViewData["access_token"] = this.HttpContext.GetTokenAsync("access_token");
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:6001")
            };
            //httpClient.DefaultRequestHeaders.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.cgzl.hateoas+json")
            //);

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            ViewData["accessToken"] = accessToken;
            httpClient.SetBearerToken(accessToken);

            var res = await httpClient.GetAsync("api/employee").ConfigureAwait(false);
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
                var objects = JsonConvert.DeserializeObject<dynamic>(json);
                ViewData["json"] = objects;
                return View();
            }
            if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }

            throw new Exception($"Error Occurred: ${res.ReasonPhrase}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

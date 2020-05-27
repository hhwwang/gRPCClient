using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UtilityApplications.Models;

namespace UtilityApplications.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TrialLink()
        {
            return View(new TrialLinkModel());
        }

        [HttpPost]
        public IActionResult TrialLink(TrialLinkModel model)
        {
            var channel = GrpcChannel.ForAddress(_configuration.GetSection("ApiAddress").Value.ToString());
            var client = new ShoppingCart.ShoppingCartClient(channel);

            var request = new InsuranceTrialLinkGetRequest
            {
                State = model.State,
                Server = model.Server
            };

            try
            {
                var response = client.InsuranceTrialLinkGet(request, deadline: DateTime.UtcNow.AddSeconds(180));
                if (response.IsValid.GetValueOrDefault())
                {
                    model.TrialURL = response.TrialURL;
                }
                else
                {
                    ModelState.AddModelError("", response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.DependencyInjection;

namespace MiniMvc
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(
            IHttpContextAccessor httpContextAccessor,
            ICompositeViewEngine viewEngines,
            IRazorViewEngine viewEngine
        )
        {
            _httpContextAccessor = httpContextAccessor;
            var foo = viewEngines.ViewEngines;
        }

        public ViewResult Index()
        {
            var abx = HttpContext.Items["ABC"];
            var httpContextAccessor = HttpContext.RequestServices.GetService(typeof(IHttpContextAccessor));
            var actionContext = HttpContext.RequestServices.GetService(typeof(ActionContext));
            var url = Url.Action("Index");
            var fooHeader = Request.Headers["foo"];
            var fooCookie = Request.Cookies["foo"];
            Trace.WriteLine($"Scheme is: {Request.Scheme}");
            Trace.WriteLine($"Protocol is: {Request.Protocol}");
            return View(new FormModel());
        }

        public ViewResult Submit(FormModel model)
        {
            return View("Index", model);
        }

        public void Foo()
        {
            Response.Headers.Add("X-Foo", "Bar");
        }

        public object Bar()
        {
            return new
            {
                Id = 1,
                Name = "Bobo the Clown",
                Favorite = Characters.Cassandra
            };
        }

        public HttpResponseMessage RM()
        {
            return new HttpResponseMessage()
            {
                Content = JsonContent.Create(new { Id = 2, Name = "Chewy" }),
                ReasonPhrase = "Wacka wacka",
                StatusCode = HttpStatusCode.NonAuthoritativeInformation
            };
        }

        public ActionResult Html()
        {
            return Content("<html><head></head><body><p>the data</p></body></html>", "text/html");
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Characters
    {
        Cassandra,
        Garth,
        Wayne
    }

    public class Moo : IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
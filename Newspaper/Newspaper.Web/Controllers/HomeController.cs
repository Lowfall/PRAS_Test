using System.Diagnostics;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newspaper.Application.CQRS.Queries.News;
using Newspaper.Web.Models;
using Newspaper.Web.Models.Home;

namespace Newspaper.Web.Controllers;

public class HomeController(IMediator mediator) : Controller
{
    private const int NEWS_AMOUNT = 3;
    
    [HttpPost]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(
                new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }
    
    public async Task<IActionResult> Index()
    {
        
        var query = new GetLatestNewsQuery(NEWS_AMOUNT);
        
        var result = await mediator.Send(query);
        
        var viewModel = result.Adapt<List<NewsViewModel>>();
        
        return View(viewModel);
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
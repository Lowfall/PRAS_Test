using System.Security.Claims;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newspaper.Application.CQRS.Commands.News;
using Newspaper.Application.CQRS.Queries.News;
using Newspaper.Application.DTOs.News.Request;
using Newspaper.Web.Models.Home;

namespace Newspaper.Web.Controllers;

public class NewsController(IMediator mediator) : Controller
{
    private const int PAGE_SIZE = 6;
    private const int START_PAGE = 1;
    
    public async Task<IActionResult> Index(CancellationToken сancellationToken)
    {
        var query = new GetPaginatedNewsQuery(START_PAGE , PAGE_SIZE);
        
        var response = await mediator.Send(query, сancellationToken);
        
        var news = response.newsList.Adapt<List<NewsViewModel>>();
        
        int totalPages = (int)Math.Ceiling((double)response.amount / PAGE_SIZE);

        ViewData["TotalPages"] = totalPages;
        
        return View(news);
    }

    public IActionResult CreationPage()
    {
        return View();
    }
    
    public async Task<IActionResult> UpdatePage(int id, CancellationToken cancellationToken)
    {
        var query = new GetNewsArticleQuery(id);
        
        var response = await mediator.Send(query, cancellationToken);
        
        var news = response.Adapt<NewsViewModel>();
        
        return View(news);
    }
    
    [HttpGet]
    public async Task<IActionResult> LoadMore(CancellationToken сancellationToken, int page = 1)
    {
        var query = new GetPaginatedNewsQuery(page, PAGE_SIZE);
        
        var response = await mediator.Send(query, сancellationToken);
        
        var news = response.newsList.Adapt<List<NewsViewModel>>();
        
        return PartialView("_NewsListPartial", news);
    }
    
    public async Task<IActionResult> Article(int id, CancellationToken cancellationToken)
    {
        var query = new GetNewsArticleQuery(id);
        
        var response = await mediator.Send(query, cancellationToken);
        
        var news = response.Adapt<NewsViewModel>();
        
        return View(news);
    }
    
    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Create(CreateNewsDTO model, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var command = new CreateNewsCommand(userId, model);
        
        await mediator.Send(command, cancellationToken);
        
        return Redirect("Index");
    }

    [HttpDelete]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Delete(int newsId, CancellationToken cancellationToken)
    {
        var command = new DeleteNewsCommand(newsId);
        
        await mediator.Send(command, cancellationToken);
        
        return NoContent();
    }
    
    [HttpPatch]
    [Authorize (Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateNewsDTO model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }
        
        var command = new UpdateNewsCommand(model);
        
        await mediator.Send(command, cancellationToken);
        
        return Redirect("Index");
    }
}
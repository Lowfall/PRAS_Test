using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newspaper.Application.CQRS.Commands.News;
using Newspaper.Application.DTOs.News.Request;

namespace Newspaper.Web.Controllers;

public class NewsController(IMediator mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Create(CreateNewsDTO model, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
        var command = new CreateNewsCommand(userId, model);
        
        await mediator.Send(command, cancellationToken);
        
        return View("Home");
    }
}
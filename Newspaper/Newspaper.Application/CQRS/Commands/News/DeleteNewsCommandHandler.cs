using MediatR;
using Newspaper.Application.Exceptions;
using Newspaper.Domain.Interfaces.Repositories;
using Newspaper.Domain.Interfaces.Services;

namespace Newspaper.Application.CQRS.Commands.News;

public record DeleteNewsCommand(int NewsId) : IRequest;

public class DeleteNewsCommandHandler(
    INewsRepository newsRepository, 
    IImageService imageService) : IRequestHandler<DeleteNewsCommand>
{
    public async Task Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await newsRepository.GetByIdAsync(request.NewsId, cancellationToken);

        if (news is null)
        {
            throw new NotFoundException("There is no news with such id");
        }
        
        await imageService.DeleteAsync(news.ImageUrl);
        
        await newsRepository.DeleteAsync(news, cancellationToken);
    }
}
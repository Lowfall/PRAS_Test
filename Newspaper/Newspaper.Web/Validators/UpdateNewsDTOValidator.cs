using FluentValidation;
using Newspaper.Application.DTOs.News.Request;

namespace Newspaper.Web.Validators;

public class UpdateNewsDTOValidator : AbstractValidator<UpdateNewsDTO>
{
    public UpdateNewsDTOValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.SubTitle)
            .NotEmpty().WithMessage("Subtitle is required.")
            .MaximumLength(200).WithMessage("Subtitle cannot exceed 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required.");

        RuleFor(x => x.Image)
            .Must(file => file == null || file.Length <= 5 * 1024 * 1024) // max 5 MB
            .WithMessage("Image size must be less than 5 MB.");
    }
}
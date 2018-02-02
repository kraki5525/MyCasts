using System;
using FluentValidation;
using MyCasts.Domain.Commands;
using MyCasts.Domain.Models;

namespace MyCasts.Domain.Validation
{
    public class CreatePodcastCommandValidator : AbstractValidator<CreatePodcastCommand>
    {   
        public CreatePodcastCommandValidator()
        {
            RuleFor(cmd => cmd.Name).NotEmpty();
            RuleFor(cmd => cmd.FeedUri).NotEmpty();
        }        
    }
}
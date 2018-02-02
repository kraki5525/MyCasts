using System;
using FluentValidation;
using MyCasts.Domain.Commands;
using MyCasts.Domain.Models;

namespace MyCasts.Domain.Validation
{
    public class UpdatePodcastCommandValidator : AbstractValidator<UpdatePodcastCommand>
    {
        public UpdatePodcastCommandValidator()
        {
            RuleFor(cmd => cmd.Id).GreaterThan(0);
            RuleFor(cmd => cmd.Name).NotEmpty();
            RuleFor(cmd => cmd.FeedUri).NotEmpty();
        }
    }
}
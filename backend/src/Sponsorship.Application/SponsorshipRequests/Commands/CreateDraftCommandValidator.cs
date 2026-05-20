using FluentValidation;
using MediatR;
using Sponsorship.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.Commands
{
    public class CreateDraftCommandValidator: AbstractValidator<CreateDraftCommand>
    {
        public CreateDraftCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Department)
                .NotEmpty();

            RuleFor(x => x.EventName)
                .NotEmpty();

            RuleFor(x => x.RequestedAmount)
                .GreaterThan(0);

            RuleFor(x => x.Purpose)
                .NotEmpty();

            RuleFor(x => x.EventDate)
                .GreaterThan(DateTime.UtcNow.Date);
        }
    }
}

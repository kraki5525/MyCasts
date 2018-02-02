using System;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCasts.Domain.Commands;

namespace MyCasts.Web.Controllers
{
    [Route("/api/[controller]")]
    public class FeedsController
    {
        private readonly IMediator _mediator;

        public FeedsController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        [HttpGet("{id}")]
        public Task<string> Get(GetFeedCommand command)
        {
            return _mediator.Send(command);
        }
    } 
}
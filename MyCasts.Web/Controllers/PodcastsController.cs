using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyCasts.Domain.Commands;

namespace MyCasts.Web.Controllers
{
    [Route("api/[controller]")]
    public class PodcastsController : Controller
    {
        private readonly IMediator _mediator;
        public PodcastsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<IEnumerable<GetPodcastsCommand.PodcastsInfo>> Get()
        {
            return _mediator.Send(new GetPodcastsCommand());
        }

        [HttpGet("{id}")]
        public Task<GetPodcastCommand.PodcastInfo> Get(GetPodcastCommand query)
        {
            return _mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> Create(CreatePodcastCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Id;
        }

        [HttpPost("{id}")]
        public async Task<int> Update(UpdatePodcastCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Id;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Services;

namespace MyCasts.Domain.Commands
{
    public class GetFeedCommand : IRequest<string>
    {
        public int Id { get; set; }
    }

    public class GetFeedHandler : ICancellableAsyncRequestHandler<GetFeedCommand, string>
    {
        private readonly Db _db;
        private readonly FeedSource _feedSource;
        private readonly IMediator _mediator;

        public GetFeedHandler(IMediator mediator, Db db, FeedSource feedSource)
        {
            this._mediator = mediator;
            this._db = db;
            this._feedSource = feedSource;
        }

        public async Task<string> Handle(GetFeedCommand message, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<GetPodcastCommand>(message);
            var podcast = await _mediator.Send(command);
            var feedData = await _feedSource.Get(podcast.FeedUri);

            return feedData;
        }
    }
}
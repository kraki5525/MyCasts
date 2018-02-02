using System;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Caching;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public class GetPodcastCommand : IRequest<GetPodcastCommand.PodcastInfo>, INeedCaching
    {
        public int Id { get; set; }

        public class PodcastInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Uri FeedUri { get; set; }
        }
    }

    public class GetPodcastHandler : BaseCommandHandler<GetPodcastCommand, GetPodcastDbAction, Podcast, GetPodcastCommand.PodcastInfo>
    {
        public GetPodcastHandler(Db db) : base(db)
        { }
    }
}
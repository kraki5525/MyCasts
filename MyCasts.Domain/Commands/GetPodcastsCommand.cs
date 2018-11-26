using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;

namespace MyCasts.Domain.Commands
{
    public class GetPodcastsCommand : IRequest<IEnumerable<GetPodcastsCommand.PodcastsInfo>>
    {
        public class PodcastsInfo
        {
            public string Name { get; set; }
        }
    }

    public class GetPodcastsHandler : BaseCommandHandler<GetPodcastsCommand, QueryPodcastDbAction,  IEnumerable<Podcast>, IEnumerable<GetPodcastsCommand.PodcastsInfo>>
    {
        public GetPodcastsHandler(IDb db) : base(db)
        { }
    }
}
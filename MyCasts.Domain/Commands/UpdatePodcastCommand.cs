
using System;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;
using MyCasts.Domain.Validation;

namespace MyCasts.Domain.Commands
{
    public class UpdatePodcastCommand : IRequest<UpdatePodcastCommand.PodcastInfo>
    {
        public int Id { get;set; }
        public string Name { get;set; }
        public Uri FeedUri { get; set; }

        public class PodcastInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Uri FeedUri { get; set; }
        }
    }

    public class UpdatePodcastHandler : BaseChangeCommandHandler<UpdatePodcastCommand, UpdatePodcastDbAction, Podcast, UpdatePodcastCommand.PodcastInfo>, INeedValidation
    {
        public UpdatePodcastHandler(IDb db) : base(db)
        { }
    }
}

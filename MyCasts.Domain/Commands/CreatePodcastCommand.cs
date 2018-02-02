using System;
using System.Threading.Tasks;
using BatMap;
using MediatR;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;
using MyCasts.Domain.Validation;

namespace MyCasts.Domain.Commands
{
    public class CreatePodcastCommand : IRequest<CreatePodcastCommand.PodcastInfo>
    {
        public string Name {get;set;}
        public Uri FeedUri {get;set;}

        public class PodcastInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Uri FeedUri { get; set; }
        }
    }

    public class CreatePodcastHandler : BaseChangeCommandHandler<CreatePodcastCommand, InsertPodcastDbAction, Podcast, CreatePodcastCommand.PodcastInfo>, INeedValidation
    {
        public CreatePodcastHandler(Db db) : base(db)
        { }
    }
}
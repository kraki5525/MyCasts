using System;

namespace MyCasts.Domain.Models
{
    public class Podcast
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public Uri FeedUri { get; set; }
    }
}
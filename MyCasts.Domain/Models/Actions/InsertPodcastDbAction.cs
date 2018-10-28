using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace MyCasts.Domain.Models.Commands
{
    public class InsertPodcastDbAction : ADbChangeAction<Podcast>
    {
        public override Podcast Execute(DbConnection connection)
        {
            var id = connection.QuerySingle<int>("insert into podcast(name, feedUri) values (@Name, @FeedUri);select last_insert_rowid();", new {Name = Model.Name, FeedUri = Model.FeedUri.ToString()});
            Model.Id = id;
            return Model;
        }

        public async override Task<Podcast> ExecuteAsync(DbConnection connection, CancellationToken token)
        {
            var id = await connection.QuerySingleAsync<int>("insert into podcast(name, feedUri) values (@Name, @FeedUri);select last_insert_rowid();", new {Name = Model.Name, FeedUri = Model.FeedUri.ToString()});
            Model.Id = id;
            return Model;
        }
    }
}
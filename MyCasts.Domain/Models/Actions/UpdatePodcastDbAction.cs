using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace MyCasts.Domain.Models.Commands
{
    public class UpdatePodcastDbAction : ADbChangeAction<Podcast>
    {
        public override Podcast Execute(DbConnection connection)
        {
            connection.Execute("update podcast set name = @Name where id = @Id", new {Id = Model.Id, Name = Model.Name, FeedUri = Model.FeedUri.ToString()});
            return Model;
        }

        public async override Task<Podcast> ExecuteAsync(DbConnection connection, CancellationToken token)
        {
            await connection.ExecuteAsync("update podcast set name = @Name where id = @Id", new {Id = Model.Id, Name = Model.Name, FeedUri = Model.FeedUri.ToString()});
            return Model;
        }
    }
}
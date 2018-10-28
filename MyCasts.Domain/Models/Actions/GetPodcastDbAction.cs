using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace MyCasts.Domain.Models.Commands
{
    public class GetPodcastDbAction : IDbAction<Podcast>
    {
        public int Id { get; set; }

        public Podcast Execute(DbConnection connection)
        {
            return connection.QueryFirstOrDefault<Podcast>("select * from podcast where id = @Id", new {Id = Id});
        }
        public Task<Podcast> ExecuteAsync(DbConnection connection, CancellationToken token)
        {
            return connection.QueryFirstOrDefaultAsync<Podcast>("select * from podcast where id = @Id", new {Id = Id});
        }
    }
}
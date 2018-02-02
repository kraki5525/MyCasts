using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyCasts.Domain.Models;

namespace MyCasts.Domain.Models.Commands
{
    public class QueryPodcastDbAction : IQueryDbAction<Podcast>
    {
        public override IEnumerable<Podcast> Execute(DbConnection connection)
        {
            return connection.Query<Podcast>("select * from podcast");
        }

        public override Task<IEnumerable<Podcast>> ExecuteAsync(DbConnection connection)
        {
            return connection.QueryAsync<Podcast>("select * from podcast");
        }
    }
}
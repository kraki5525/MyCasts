using System;
using System.Threading.Tasks;
using FakeItEasy;
using MyCasts.Domain.Commands;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;
using Shouldly;
using Xunit;

namespace MyCasts.Tests
{
    public class MyTests
    {
        [Fact]
        public async Task CreatePodcast_Calls_InsertPodcastDbCommand()
        {
            // var db = A.Fake<Db>();
            // A.CallTo(() => db.ExecuteAsync(A<InsertPodcastDbCommand>._)).Returns(Task.FromResult(1));

            // var command = new CreatePodcastCommand() { Name = "Test", FeedUri = new Uri("http://www.google.com")};
            // var handler = new CreatePodcastHandler(db);
            // var id = await handler.Handle(command);
            // id.ShouldBe(1);
        }
    }
}

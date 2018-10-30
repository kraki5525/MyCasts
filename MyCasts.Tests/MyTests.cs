using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using MyCasts.Domain;
using MyCasts.Domain.Commands;
using MyCasts.Domain.Models;
using MyCasts.Domain.Models.Commands;
using Shouldly;
using SimpleInjector;
using Xunit;

namespace MyCasts.Tests
{
    public class MyTests
    {
        public MyTests()
        {
            var container = new Container();
            (new Module()).Initialize(container, null);
        }

        [Fact]
        public async Task CreatePodcast_Calls_InsertPodcastDbCommand()
        {
            var db = A.Fake<IDb>();
            var model = new Podcast() {Id = 1};
            var token = new CancellationToken();

            A.CallTo(() => db.ExecuteAsync(A<InsertPodcastDbAction>._, token)).Returns(Task.FromResult(model));

            var command = new CreatePodcastCommand() { Name = "Test", FeedUri = new Uri("http://www.google.com")};
            var handler = new CreatePodcastHandler(db);
            var result = await handler.Handle(command, token);
            result.Id.ShouldBe(1);
        }
    }
}

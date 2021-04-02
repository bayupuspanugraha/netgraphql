using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NetGraphQL.GraphQL.Services
{
    public interface IGraphQLUserEventService
    {
        void CreateNewUserAddedEvent(string data);

        IObservable<string> OnCreateNewUserAddedObservable();
    }

    public class GraphQLUserEventService : IGraphQLUserEventService
    {
        private readonly ISubject<string> _onNewUserAdded = new ReplaySubject<string>();

        public void CreateNewUserAddedEvent(string data) => _onNewUserAdded.OnNext(data);

        public IObservable<string> OnCreateNewUserAddedObservable() => _onNewUserAdded.AsObservable();
    }
}

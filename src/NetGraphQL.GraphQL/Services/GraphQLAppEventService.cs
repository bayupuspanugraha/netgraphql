using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NetGraphQL.GraphQL.Services
{
    public interface IGraphQLAppEventService
    {
        void CreateNewLangAddedEvent(string data);

        IObservable<string> OnCreateNewLangAddedObservable();
    }

    public class GraphQLAppEventService : IGraphQLAppEventService
    {
        private readonly ISubject<string> _onNewLangAdded = new ReplaySubject<string>();

        public void CreateNewLangAddedEvent(string data) => _onNewLangAdded.OnNext(data);

        public IObservable<string> OnCreateNewLangAddedObservable() => _onNewLangAdded.AsObservable();
    }
}

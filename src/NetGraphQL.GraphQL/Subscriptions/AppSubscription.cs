using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using NetGraphQL.GraphQL.Services;
using System;

namespace NetGraphQL.GraphQL.Subscriptions
{
    public class AppSubscription : ObjectGraphType
    {
        private readonly IGraphQLAppEventService _appEventService;

        public AppSubscription(IGraphQLAppEventService appEventService)
        {
            _appEventService = appEventService;

            Name = nameof(AppSubscription);

            AddField(new EventStreamFieldType
            {
                Name = "newLangAdded",
                Type = typeof(StringGraphType),
                Resolver = new FuncFieldResolver<string>(ResolveNewLangAddedRequested),
                Subscriber = new EventStreamResolver<string>(SubscribeNewLangAddedRequested)
            });
        }

        private string ResolveNewLangAddedRequested(IResolveFieldContext context)
        {
            return context.Source as string;
        }

        private IObservable<string> SubscribeNewLangAddedRequested(IResolveEventStreamContext context)
        {
            return _appEventService.OnCreateNewLangAddedObservable();
        }
    }
}

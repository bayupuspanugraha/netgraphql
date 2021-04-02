using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Subscription;
using GraphQL.Types;
using NetGraphQL.GraphQL.Services;
using System;

namespace NetGraphQL.GraphQL.Subscriptions
{
    public class UserSubscription : ObjectGraphType
    {
        private readonly IGraphQLUserEventService _userEventService;

        public UserSubscription(IGraphQLUserEventService userEventService)
        {
            _userEventService = userEventService;

            Name = nameof(UserSubscription);

            AddField(new EventStreamFieldType
            {
                Name = "newUserAdded",
                Type = typeof(StringGraphType),
                Resolver = new FuncFieldResolver<string>(ResolveNewUserAddedRequested),
                Subscriber = new EventStreamResolver<string>(SubscribeNewUserAddedRequested)
            });
        }

        private string ResolveNewUserAddedRequested(IResolveFieldContext context)
        {
            return context.Source as string;
        }

        private IObservable<string> SubscribeNewUserAddedRequested(IResolveEventStreamContext context)
        {
            return _userEventService.OnCreateNewUserAddedObservable();
        }
    }
}

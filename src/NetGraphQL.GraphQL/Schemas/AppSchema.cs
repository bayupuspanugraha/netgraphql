using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using NetGraphQL.GraphQL.Mutations;
using NetGraphQL.GraphQL.Queries;
using NetGraphQL.GraphQL.Subscriptions;
using System;

namespace NetGraphQL.GraphQL.Schemas
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider service) : base(service)
        {
            Query = service.GetRequiredService<AppQuery>();
            Mutation = service.GetRequiredService<AppMutation>();
            Subscription = service.GetRequiredService<AppSubscription>();
        }
    }
}

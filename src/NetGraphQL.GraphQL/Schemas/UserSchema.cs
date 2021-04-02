using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using NetGraphQL.GraphQL.Mutations;
using NetGraphQL.GraphQL.Queries;
using NetGraphQL.GraphQL.Subscriptions;
using System;

namespace NetGraphQL.GraphQL.Schemas
{
    public class UserSchema : Schema
    {
        public UserSchema(IServiceProvider service) : base(service)
        {
            Query = service.GetRequiredService<UserQuery>();
            Mutation = service.GetRequiredService<UserMutation>();
            Subscription = service.GetRequiredService<UserSubscription>();
        }
    }
}

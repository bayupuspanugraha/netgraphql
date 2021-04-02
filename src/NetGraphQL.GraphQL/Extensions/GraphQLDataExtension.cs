using GraphQL;
using GraphQL.Execution;
using GraphQL.Language.AST;
using GraphQL.Server;
using Microsoft.Extensions.DependencyInjection;
using NetGraphQL.GraphQL.Mutations;
using NetGraphQL.GraphQL.Queries;
using NetGraphQL.GraphQL.Schemas;
using NetGraphQL.GraphQL.Services;
using NetGraphQL.GraphQL.Subscriptions;
using NetGraphQL.GraphQL.Types.Response;
using NetGraphQL.GraphQL.Types.ViewModel;
using System;

namespace NetGraphQL.GraphQL.Extensions
{
    public class SerialDocumentExecuter : DocumentExecuter
    {
        //** Based on undertanding on the links below I conclude to have this setting in order subscription running properly with the version 5.*:
        // - https://github.com/graphql-dotnet/graphql-dotnet/blob/master/src/GraphQL/Execution/DocumentExecuter.cs
        // - https://fiyazhasan.me/graphql-with-net-core-part-x-execution-strategies/

        //private static IExecutionStrategy ParallelExecutionStrategy = new ParallelExecutionStrategy();
        //private static IExecutionStrategy SerialExecutionStrategy = new SerialExecutionStrategy();
        private static IExecutionStrategy SubscriptionExecutionStrategy = new SubscriptionExecutionStrategy();

        protected override IExecutionStrategy SelectExecutionStrategy(ExecutionContext context)
        {
            return context.Operation.OperationType switch
            {
                ////OperationType.Query => ParallelExecutionStrategy,
                ////OperationType.Mutation => SerialExecutionStrategy,
                ////_ => throw new InvalidOperationException($"Unexpected OperationType {context.Operation.OperationType}"), 
                ///

                OperationType.Subscription => SubscriptionExecutionStrategy,
                _ => base.SelectExecutionStrategy(context)
            };
        }
    }

    public static class GraphQLDataExtension
    {
        public static void AddGraphQLData(this IServiceCollection services)
        {
            RegisterDataToGraphQL(services);

            // Is used to make subscription work
            services.AddSingleton<IDocumentExecuter, SerialDocumentExecuter>();

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.UnhandledExceptionDelegate = context =>
                {
                    Console.WriteLine($"Error: {context.OriginalException.Message}");
                };
            })
           //.AddGraphQLAuthorization(options =>
           //{
           //    options.AddPolicy("Authorized", p => p.RequireAuthenticatedUser());
           //    //var policy = new AuthorizationPolicyBuilder()
           //    //                    .
           //    //options.AddPolicy("Authorized", p => p.RequireClaim(ClaimTypes.Name, "Tom"));
           //})
           .AddWebSockets()
           .AddDataLoader()
           .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
           .AddSystemTextJson() // Info: if you want to know which one to use => SystemTextJson vs NewtonSoftJson https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to?pivots=dotnet-5-0
           .AddGraphTypes(ServiceLifetime.Scoped);           
        }

        private static void RegisterDataToGraphQL(this IServiceCollection services)
        {
            //** Type
            services.AddScoped<LanguageViewType>();
            services.AddScoped<AddNewLanguageResponseType>();

            services.AddScoped<UserViewType>();
            services.AddScoped<AddNewUserResponseType>();

            services.AddSingleton<IGraphQLAppEventService, GraphQLAppEventService>();
            services.AddSingleton<IGraphQLUserEventService, GraphQLUserEventService>();

            //**Queries
            services.AddScoped<UserQuery>();
            services.AddScoped<AppQuery>();

            //**Mutations
            services.AddScoped<UserMutation>();
            services.AddScoped<AppMutation>();

            //**Schemas
            services.AddScoped<UserSchema>();
            services.AddScoped<AppSchema>();

            //**Subscriptions
            services.AddScoped<UserSubscription>();
            services.AddScoped<AppSubscription>();
        }
    }
}

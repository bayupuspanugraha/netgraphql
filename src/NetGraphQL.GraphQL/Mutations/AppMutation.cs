using GraphQL;
using GraphQL.Types;
using NetGraphQL.GraphQL.Services;
using NetGraphQL.GraphQL.Types.Response;
using NetGraphQL.Shared;
using System;
using System.Threading.Tasks;

namespace NetGraphQL.GraphQL.Mutations
{
    public class AppMutation : ObjectGraphType
    {
        private readonly IGraphQLAppEventService _appEventService;

        public AppMutation(IGraphQLAppEventService appEventService)
        {
            _appEventService = appEventService;

            Name = nameof(AppMutation);

            FieldAsync<AddNewLanguageResponseType>(
                name: "addNewLanguage",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "lang" }
                ),
                resolve: async context =>
                {
                    return await AddNewLanguage(context);
                }
            ); 
        }

        #region Private Method
        private async Task<ServiceResponse<int>> AddNewLanguage(IResolveFieldContext<object> context)
        {
            var newLang = context.GetArgument<string>("lang");

            var response = new ServiceResponse<int> {
                Result = new Random().Next()
            };
            _appEventService.CreateNewLangAddedEvent(newLang);
            return await Task.FromResult(response);
        }

        #endregion
    }
}

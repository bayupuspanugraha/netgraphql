using GraphQL;
using GraphQL.Types;
using NetGraphQL.GraphQL.Services;
using NetGraphQL.GraphQL.Types.Response;
using NetGraphQL.Shared;
using System;
using System.Threading.Tasks;

namespace NetGraphQL.GraphQL.Mutations
{
    public class UserMutation : ObjectGraphType
    {
        private readonly IGraphQLUserEventService _userEventService;
        public UserMutation(IGraphQLUserEventService userEventService)
        {
            _userEventService = userEventService;

            Name = nameof(UserMutation);

            FieldAsync<AddNewUserResponseType>(
                name: "addNewUser",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" }
                ),
                resolve: async context =>
                {
                    return await AddNewUser(context);
                }
            );
        }

        #region Private Method
        private async Task<ServiceResponse<int>> AddNewUser(IResolveFieldContext<object> context)
        {
            var newUser = context.GetArgument<string>("name");

            var response = new ServiceResponse<int>
            {
                Result = new Random().Next()
            };
            _userEventService.CreateNewUserAddedEvent(newUser);
            return await Task.FromResult(response);
        }

        #endregion
    }
}

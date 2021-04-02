using GraphQL.Types;
using NetGraphQL.Shared;

namespace NetGraphQL.GraphQL.Types.Response
{
    public class AddNewUserResponseType : ObjectGraphType<ServiceResponse<int>>
    {
        public AddNewUserResponseType()
        {
            Name = nameof(AddNewUserResponseType);

            Field(f => f.Messages);
            Field(f => f.ErrorMessages);
            Field(f => f.Result);
            Field(f => f.IsError);
        }
    }
}

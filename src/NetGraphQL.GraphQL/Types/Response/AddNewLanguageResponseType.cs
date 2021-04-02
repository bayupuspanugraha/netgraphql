using GraphQL.Types;
using NetGraphQL.Shared;

namespace NetGraphQL.GraphQL.Types.Response
{
    public class AddNewLanguageResponseType : ObjectGraphType<ServiceResponse<int>>
    {
        public AddNewLanguageResponseType()
        {
            Name = nameof(AddNewLanguageResponseType);

            Field(f => f.Messages);
            Field(f => f.ErrorMessages);
            Field(f => f.Result);
            Field(f => f.IsError);
        }
    }
}

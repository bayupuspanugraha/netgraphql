using GraphQL.Types;
using NetGraphQL.Shared.ViewModel;

namespace NetGraphQL.GraphQL.Types.ViewModel
{
    public class UserViewType : ObjectGraphType<UserViewModel>
    {
        public UserViewType()
        {
            Name = nameof(UserViewType);

            Field(f => f.Id);
            Field(f => f.Name);            
        }
    }
}

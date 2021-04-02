using GraphQL.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetGraphQL.GraphQL.Queries
{
    public class UserQuery : ObjectGraphType
    {
        public UserQuery()
        {
            Name = nameof(UserQuery);

            FieldAsync<ListGraphType<StringGraphType>>(
                name: "getUsers",
                resolve: async context =>
                {
                    return await Task.FromResult(new List<string> { 
                        "System",
                        "App",
                        "Michael"
                    });
                }
            );
        }
    }
}

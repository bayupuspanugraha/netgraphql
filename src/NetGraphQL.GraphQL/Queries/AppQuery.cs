using GraphQL.Types;
using System.Threading.Tasks;

namespace NetGraphQL.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery()
        {
            Name = nameof(AppQuery);

            FieldAsync<StringGraphType>(
                name: "ping",                
                resolve: async context =>
                {
                    return await Task.FromResult("PONG");
                }
            );
        } 
    }
}

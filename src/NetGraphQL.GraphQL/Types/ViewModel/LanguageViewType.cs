using GraphQL.Types;
using NetGraphQL.Shared.ViewModel;

namespace NetGraphQL.GraphQL.Types.ViewModel
{
    public class LanguageViewType : ObjectGraphType<LanguageViewModel>
    {
        public LanguageViewType()
        {
            Name = nameof(LanguageViewType);

            Field(f => f.Id);
            Field(f => f.Name);            
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using GraphQL.Language.AST;

namespace S360.GraphQL.Query
{
    public static class FieldAstExtensions
    {
        public static IEnumerable<Field> GetQueryFields(this Field asd)
        {
            return asd.Children.Single(x => x is SelectionSet).Children.Select(x => x as Field);
        }
    }
}
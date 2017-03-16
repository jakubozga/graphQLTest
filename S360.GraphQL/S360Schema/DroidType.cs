using GraphQL.Types;
using S360.GraphQL.Controllers;
using S360.GraphQL.Dtos;

namespace S360.GraphQL.S360Schema
{
    public class DroidType : ObjectGraphType<DroidDto>
    {
        public DroidType()
        {
            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name, nullable: true).Description("The name of the Droid.");

            Field<ListGraphType<EpisodeType>>("episodes",resolve: context => context.Source.Episodes);
        }
    }
}
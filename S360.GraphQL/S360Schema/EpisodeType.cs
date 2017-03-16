using GraphQL.Types;
using S360.GraphQL.Controllers;
using S360.GraphQL.Dtos;

namespace S360.GraphQL.S360Schema
{
    public class EpisodeType : ObjectGraphType<EpisodeDto>
    {
        public EpisodeType()
        {
            Field(x => x.Id).Description("The Id of the Episode.");
            Field(x => x.Title).Description("The title of the Episode.");

            Field<ListGraphType<CharacterType>>("characters", resolve: context => context.Source.Characters);
        }
    }
}
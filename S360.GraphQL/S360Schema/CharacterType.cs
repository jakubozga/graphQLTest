using GraphQL.Types;
using S360.GraphQL.Controllers;
using S360.GraphQL.Dtos;

namespace S360.GraphQL.S360Schema
{
    public class CharacterType : ObjectGraphType<CharacterDto>
    {
        public CharacterType()
        {
            Field(x => x.Id).Description("The Id of the Episode.");
            Field(x => x.Name).Description("The title of the Episode.");

            Field<PlanetType>("planet", resolve: context => context.Source.Planet);
        }
    }
}
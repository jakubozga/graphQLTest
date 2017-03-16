using GraphQL.Types;
using S360.GraphQL.Controllers;
using S360.GraphQL.Dtos;

namespace S360.GraphQL.S360Schema
{
    public class HumanType : ObjectGraphType<HumanDto>
    {
        public HumanType()
        {
            Field(x => x.Id).Description("The Id of the Droid.");
            Field(x => x.Name, nullable: true).Description("The name of the Droid.");
        }
    }
}
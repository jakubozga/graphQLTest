using System.Collections.Generic;

namespace S360.GraphQL.Dtos
{
    public class PlanetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HumanDto Human { get; set; }

        public IEnumerable<HumanDto> Humans { get; set; }
    }
}
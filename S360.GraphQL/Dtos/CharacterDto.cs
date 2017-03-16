using System.Collections.Generic;

namespace S360.GraphQL.Dtos
{
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EpisodeId { get; set; }

        public PlanetDto Planet { get; set; }
    }
}
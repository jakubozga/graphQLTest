using System.Collections.Generic;

namespace S360.GraphQL.Dtos
{
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<CharacterDto> Characters { get; set; }
    }
}
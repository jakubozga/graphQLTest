using System.Collections.Generic;

namespace S360.GraphQL.Dtos
{
    public class DroidDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<EpisodeDto> Episodes { get; set; }
    }
}
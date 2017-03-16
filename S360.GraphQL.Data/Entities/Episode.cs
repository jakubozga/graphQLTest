using System.Collections.Generic;

namespace S360.GraphQL.Data.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
    }
}
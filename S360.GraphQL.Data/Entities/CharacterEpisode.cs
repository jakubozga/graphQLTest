namespace S360.GraphQL.Data.Entities
{
    public class CharacterEpisode
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
    }
}
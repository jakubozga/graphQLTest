using System.Collections.Generic;

namespace S360.GraphQL.Data.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Planet Planet { get; set; }
        public virtual ICollection<CharacterEpisode> CharacterEpisodes { get; set; }
        public virtual ICollection<CharacterFriend> CharacterFriends { get; set; }
        public virtual ICollection<CharacterFriend> FriendCharacters { get; set; }
    }
}
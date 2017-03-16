using System.Data.Entity;
using S360.GraphQL.Data.Entities;

namespace S360.GraphQL.Data
{
    public class StarWarsContext : DbContext
    {
        public StarWarsContext() : base("Default")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // episodes
            modelBuilder.Entity<Episode>().HasKey(c => c.Id);

            // planets
            modelBuilder.Entity<Planet>().HasKey(c => c.Id);

            modelBuilder.Entity<Character>().HasOptional(x => x.Planet);

            // characters
            modelBuilder.Entity<Character>().HasKey(c => c.Id);

            // characters-friends
            modelBuilder.Entity<CharacterFriend>().HasKey(t => new { t.CharacterId, t.FriendId });

            modelBuilder.Entity<CharacterFriend>()
                .HasRequired(cf => cf.Character)
                .WithMany(c => c.CharacterFriends)
                .HasForeignKey(cf => cf.CharacterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CharacterFriend>()
                .HasRequired(cf => cf.Friend)
                .WithMany(t => t.FriendCharacters)
                .HasForeignKey(cf => cf.FriendId)
                .WillCascadeOnDelete(false);

            // characters-episodes
            modelBuilder.Entity<CharacterEpisode>().HasKey(t => new { t.CharacterId, t.EpisodeId });

            modelBuilder.Entity<CharacterEpisode>()
                .HasRequired(cf => cf.Character)
                .WithMany(c => c.CharacterEpisodes)
                .HasForeignKey(cf => cf.CharacterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CharacterEpisode>()
                .HasRequired(cf => cf.Episode)
                .WithMany(t => t.CharacterEpisodes)
                .HasForeignKey(cf => cf.EpisodeId)
                .WillCascadeOnDelete(false);

            
        }

        // ...

        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterFriend> CharacterFriends { get; set; }
        public virtual DbSet<CharacterEpisode> CharacterEpisodes { get; set; }
        public virtual DbSet<Droid> Droids { get; set; }
        public virtual DbSet<Human> Humans { get; set; }
    }
}

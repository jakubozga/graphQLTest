using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S360.GraphQL.Data.Entities;

namespace S360.GraphQL.Data
{
    public static class StarWarsSeedData
    {
        public static void EnsureSeedData(this StarWarsContext db)
        {
            // episodes
            var newhope = new Episode { Title = "NEWHOPE" };
            var empire = new Episode { Title = "EMPIRE" };
            var jedi = new Episode { Title = "JEDI" };
            var episodes = new List<Episode>
            {
                newhope,
                empire,
                jedi,
            };
            if (!db.Episodes.Any())
            {
                db.Episodes.AddRange(episodes);
                db.SaveChanges();
            }

            // planets
            var tatooine = new Planet { Name = "Tatooine" };
            var alderaan = new Planet { Name = "Alderaan" };
            var planets = new List<Planet>
            {
                tatooine,
                alderaan
            };
            if (!db.Planets.Any())
            {
                db.Planets.AddRange(planets);
                db.SaveChanges();
            }

            // humans
            var luke = new Human
            {
                Name = "Luke Skywalker",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                Planet = tatooine
            };
            var vader = new Human
            {
                Name = "Darth Vader",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                Planet = tatooine
            };
            var han = new Human
            {
                Name = "Han Solo",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                Planet = tatooine
            };
            var leia = new Human
            {
                Name = "Leia Organa",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                Planet = alderaan
            };
            var tarkin = new Human
            {
                Name = "Wilhuff Tarkin",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope }
                },
                Planet = alderaan
            };
            var humans = new List<Human>
            {
                luke,
                vader,
                han,
                leia,
                tarkin
            };
            if (!db.Humans.Any())
            {
                db.Humans.AddRange(humans);
                db.SaveChanges();
            }

            // droids
            var threepio = new Droid
            {
                Name = "C-3PO",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                PrimaryFunction = "Protocol",
            };
            var artoo = new Droid
            {
                Name = "R2-D2",
                CharacterEpisodes = new List<CharacterEpisode>
                {
                    new CharacterEpisode { Episode = newhope },
                    new CharacterEpisode { Episode = empire },
                    new CharacterEpisode { Episode = jedi }
                },
                PrimaryFunction = "Astromech"
            };
            var droids = new List<Droid>
            {
                threepio,
                artoo
            };
            if (!db.Droids.Any())
            {
                db.Droids.AddRange(droids);
                db.SaveChanges();
            }

            // update character's friends
            luke.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = threepio },
                new CharacterFriend { Friend = artoo }
            };
            vader.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = tarkin }
            };
            han.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = artoo }
            };
            leia.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = threepio },
                new CharacterFriend { Friend = artoo }
            };
            tarkin.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = vader }
            };
            threepio.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia },
                new CharacterFriend { Friend = artoo }
            };
            artoo.CharacterFriends = new List<CharacterFriend>
            {
                new CharacterFriend { Friend = luke },
                new CharacterFriend { Friend = han },
                new CharacterFriend { Friend = leia }
            };

            db.SaveChanges();
        }
    }
}

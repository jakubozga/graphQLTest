using System;
using System.Linq;
using System.Linq.Expressions;
using GraphQL.Language.AST;
using GraphQL.Types;
using S360.GraphQL.Data;
using S360.GraphQL.Data.Entities;
using S360.GraphQL.Dtos;
using S360.GraphQL.S360Schema;

namespace S360.GraphQL.Query
{
    public class StarWarsQuery : ObjectGraphType
    {
        public StarWarsQuery(StarWarsContext db)
        {
            Droid(db);
            Character(db);
            Episode(db);
        }

        //Many to many relation is retrieved by injecting expression into base query
        private void Droid(StarWarsContext db)
        {
            Field<ListGraphType<DroidType>>(
                "droid", 
                arguments: new QueryArguments( new QueryArgument<IntGraphType>
                {
                    Name = "id"
                }),
                resolve: context =>
                {
                    var query = db.Droids.AsQueryable();

                    var id = context.GetArgument<int?>("id");
                    if (id.HasValue)
                    {
                        query = query.Where(x => x.Id == id);
                    }

                    var queryFields = context.FieldAst.GetQueryFields();
                    var queryEpisodes = queryFields.Any(x => x.Name == "episodes");

                    Expression<Func<Episode, EpisodeDto>> exp = d => new EpisodeDto();
                    if (queryEpisodes)
                    {
                        exp = e => new EpisodeDto
                        {
                            Id = e.Id,
                            Title = e.Title
                        };
                    }

                    var resultQuery = query.Select(x => new DroidDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Episodes = x.CharacterEpisodes.Select(ce => ce.Episode).AsQueryable().Select(exp)
                    });

                    return resultQuery.ToList();
                });
        }

        //base query fields as well as 1-1 relation fields are retrieved by creating dynamic expression query
        private void Character(StarWarsContext db)
        {
            Field<ListGraphType<CharacterType>>("character", resolve: context =>
            {
                var query = db.Characters.AsQueryable();

                var queryFields = context.FieldAst.GetQueryFields();

                var droidSelect = new SelectList<Character>();

                foreach (Field node in queryFields)
                {
                    if (node.Name == "planet")
                    {
                        foreach (var nodeChild in node.GetQueryFields())
                        {
                            var type2 = typeof(PlanetDto).GetProperties().Single(x => x.Name.ToUpper() == nodeChild.Name.ToUpper()).PropertyType;
                            if (type2 == typeof(int))
                                droidSelect.Add<Planet, int>(x => x.Planet, nodeChild.Name);
                            else if (type2 == typeof(string))
                                droidSelect.Add<Planet, string>(x => x.Planet, nodeChild.Name);
                        }

                        continue;
                    }

                    var type = typeof(CharacterDto).GetProperties().Single(x => x.Name.ToUpper() == node.Name.ToUpper()).PropertyType;
                    if (type == typeof(int))
                        droidSelect.Add<int>(node.Name);
                    else if (type == typeof(string))
                        droidSelect.Add<string>(node.Name);
                }

                var selectResult = droidSelect.Select<CharacterDto>(query);

                return selectResult.ToList();
            });
        }

        //many to many relation is retrieved by post materialization
        private void Episode(StarWarsContext db)
        {
            Field<ListGraphType<EpisodeType>>("episode", resolve: context =>
            {
                var dtos = db
                    .Episodes
                    .Select(x => new EpisodeDto()
                    {
                        Id = x.Id,
                        Title = x.Title
                    })
                    .ToList();

                var ids = dtos.Select(x => x.Id);

                var chars = db
                    .CharacterEpisodes
                    .Where(x => ids.Contains(x.EpisodeId))
                    .Select(x => new CharacterDto()
                    {
                        Id = x.Character.Id,
                        Name = x.Character.Name,
                        EpisodeId = x.EpisodeId
                    })
                    .ToList();

                foreach (var dto in dtos)
                {
                    dto.Characters = chars.Where(x => x.EpisodeId == dto.Id);
                }

                return dtos;
            });
        }
    }
}
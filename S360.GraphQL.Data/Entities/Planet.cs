using System.Collections.Generic;

namespace S360.GraphQL.Data.Entities
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Human Human { get; set; }
    }
}
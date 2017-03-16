namespace S360.GraphQL.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<S360.GraphQL.Data.StarWarsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(S360.GraphQL.Data.StarWarsContext context)
        {
            context.EnsureSeedData();
        }
    }
}

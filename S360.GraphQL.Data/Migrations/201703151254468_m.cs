namespace S360.GraphQL.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharacterEpisodes",
                c => new
                    {
                        CharacterId = c.Int(nullable: false),
                        EpisodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterId, t.EpisodeId })
                .ForeignKey("dbo.Characters", t => t.CharacterId)
                .ForeignKey("dbo.Episodes", t => t.EpisodeId)
                .Index(t => t.CharacterId)
                .Index(t => t.EpisodeId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PrimaryFunction = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Planet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Planets", t => t.Planet_Id)
                .Index(t => t.Planet_Id);
            
            CreateTable(
                "dbo.CharacterFriends",
                c => new
                    {
                        CharacterId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterId, t.FriendId })
                .ForeignKey("dbo.Characters", t => t.CharacterId)
                .ForeignKey("dbo.Characters", t => t.FriendId)
                .Index(t => t.CharacterId)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Human_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.Human_Id)
                .Index(t => t.Human_Id);
            
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterEpisodes", "EpisodeId", "dbo.Episodes");
            DropForeignKey("dbo.CharacterEpisodes", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Characters", "Planet_Id", "dbo.Planets");
            DropForeignKey("dbo.Planets", "Human_Id", "dbo.Characters");
            DropForeignKey("dbo.CharacterFriends", "FriendId", "dbo.Characters");
            DropForeignKey("dbo.CharacterFriends", "CharacterId", "dbo.Characters");
            DropIndex("dbo.Planets", new[] { "Human_Id" });
            DropIndex("dbo.CharacterFriends", new[] { "FriendId" });
            DropIndex("dbo.CharacterFriends", new[] { "CharacterId" });
            DropIndex("dbo.Characters", new[] { "Planet_Id" });
            DropIndex("dbo.CharacterEpisodes", new[] { "EpisodeId" });
            DropIndex("dbo.CharacterEpisodes", new[] { "CharacterId" });
            DropTable("dbo.Episodes");
            DropTable("dbo.Planets");
            DropTable("dbo.CharacterFriends");
            DropTable("dbo.Characters");
            DropTable("dbo.CharacterEpisodes");
        }
    }
}

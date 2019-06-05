namespace DiscussionApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CutoutTelevisionandSports : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discussion", "SportId", "dbo.Sport");
            DropForeignKey("dbo.Discussion", "TelevisionId", "dbo.TVShow");
            DropIndex("dbo.Discussion", new[] { "TelevisionId" });
            DropIndex("dbo.Discussion", new[] { "SportId" });
            DropColumn("dbo.Discussion", "TelevisionId");
            DropColumn("dbo.Discussion", "SportId");
            DropColumn("dbo.Discussion", "MediaType");
            DropColumn("dbo.Film", "MediaType");
            DropTable("dbo.Sport");
            DropTable("dbo.TVShow");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TVShow",
                c => new
                    {
                        TelevisionId = c.Int(nullable: false, identity: true),
                        MediaType = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Creator = c.String(nullable: false),
                        Director = c.String(),
                        Writer = c.String(),
                        Stars = c.String(nullable: false),
                        Synopsis = c.String(nullable: false),
                        Genre1 = c.Int(nullable: false),
                        Genre2 = c.Int(nullable: false),
                        Network = c.String(nullable: false),
                        Released = c.Boolean(nullable: false),
                        Year = c.String(nullable: false),
                        DateAired = c.String(),
                        Runtime = c.Int(nullable: false),
                        Rating = c.String(nullable: false),
                        Cinematographer = c.String(),
                        Editor = c.String(),
                    })
                .PrimaryKey(t => t.TelevisionId);
            
            CreateTable(
                "dbo.Sport",
                c => new
                    {
                        SportId = c.Int(nullable: false, identity: true),
                        MediaType = c.Int(nullable: false),
                        League = c.Int(nullable: false),
                        HomeTeam = c.String(nullable: false),
                        AwayTeam = c.String(nullable: false),
                        Location = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Network = c.String(),
                        Score = c.String(),
                    })
                .PrimaryKey(t => t.SportId);
            
            AddColumn("dbo.Film", "MediaType", c => c.Int(nullable: false));
            AddColumn("dbo.Discussion", "MediaType", c => c.Int(nullable: false));
            AddColumn("dbo.Discussion", "SportId", c => c.Int(nullable: false));
            AddColumn("dbo.Discussion", "TelevisionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Discussion", "SportId");
            CreateIndex("dbo.Discussion", "TelevisionId");
            AddForeignKey("dbo.Discussion", "TelevisionId", "dbo.TVShow", "TelevisionId", cascadeDelete: true);
            AddForeignKey("dbo.Discussion", "SportId", "dbo.Sport", "SportId", cascadeDelete: true);
        }
    }
}

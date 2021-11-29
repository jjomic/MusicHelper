namespace MusicHelp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StarredMethodAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instrument", "IsStarred", c => c.Boolean(nullable: false));
            AddColumn("dbo.Lesson", "IsStarred", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tablature", "IsStarred", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tablature", "IsStarred");
            DropColumn("dbo.Lesson", "IsStarred");
            DropColumn("dbo.Instrument", "IsStarred");
        }
    }
}

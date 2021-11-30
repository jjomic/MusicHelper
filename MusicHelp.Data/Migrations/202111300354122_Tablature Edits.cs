namespace MusicHelp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablatureEdits : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tablature", new[] { "LessonID" });
            AlterColumn("dbo.Tablature", "LessonID", c => c.Int());
            CreateIndex("dbo.Tablature", "LessonID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tablature", new[] { "LessonID" });
            AlterColumn("dbo.Tablature", "LessonID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tablature", "LessonID");
        }
    }
}

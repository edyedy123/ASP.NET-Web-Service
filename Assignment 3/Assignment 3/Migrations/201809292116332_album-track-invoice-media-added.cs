namespace Assignment_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class albumtrackinvoicemediaadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "Photo", c => c.Binary());
            AddColumn("dbo.Album", "ContentType", c => c.String(maxLength: 50));
            AddColumn("dbo.Track", "Audio", c => c.Binary());
            AddColumn("dbo.Track", "ContentType", c => c.String(maxLength: 50));
            AddColumn("dbo.Invoice", "PDF", c => c.Binary());
            AddColumn("dbo.Invoice", "ContentType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "ContentType");
            DropColumn("dbo.Invoice", "PDF");
            DropColumn("dbo.Track", "ContentType");
            DropColumn("dbo.Track", "Audio");
            DropColumn("dbo.Album", "ContentType");
            DropColumn("dbo.Album", "Photo");
        }
    }
}

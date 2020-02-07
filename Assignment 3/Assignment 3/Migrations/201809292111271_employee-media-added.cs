namespace Assignment_3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeemediaadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Photo", c => c.Binary());
            AddColumn("dbo.Employee", "ContentType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ContentType");
            DropColumn("dbo.Employee", "Photo");
        }
    }
}

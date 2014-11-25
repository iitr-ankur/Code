namespace testcodefirstService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameToUserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("testcodefirst.Users", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("testcodefirst.Users", "UserName");
        }
    }
}

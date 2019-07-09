namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarios3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "EMail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "EMail");
        }
    }
}

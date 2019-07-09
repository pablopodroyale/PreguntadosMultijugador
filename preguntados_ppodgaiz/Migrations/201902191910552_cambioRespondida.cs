namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambioRespondida : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PreguntasRespondidas", new[] { "usuario_Id" });
            CreateIndex("dbo.PreguntasRespondidas", "Usuario_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PreguntasRespondidas", new[] { "Usuario_Id" });
            CreateIndex("dbo.PreguntasRespondidas", "usuario_Id");
        }
    }
}

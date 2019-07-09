namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_Usuario_Add_Preguntas_Respondidas : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsuarioTablas", newName: "PreguntasRespondidas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PreguntasRespondidas", newName: "UsuarioTablas");
        }
    }
}

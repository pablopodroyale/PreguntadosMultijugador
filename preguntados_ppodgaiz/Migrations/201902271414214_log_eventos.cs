namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class log_eventos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEventoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Accion = c.Int(nullable: false),
                        Entidad = c.Int(nullable: false),
                        EntidadId = c.Guid(nullable: false),
                        usuarioId = c.String(),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogEventoes");
        }
    }
}

namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoria_3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Color = c.String(),
                        CantidadPreguntasAResponder = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categorias");
        }
    }
}

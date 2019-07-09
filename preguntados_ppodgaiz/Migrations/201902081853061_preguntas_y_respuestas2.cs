namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class preguntas_y_respuestas2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Preguntas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Categoria_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id)
                .Index(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.Respuestas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EsCorrecta = c.Boolean(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Pregunta_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Preguntas", t => t.Pregunta_Id)
                .Index(t => t.Pregunta_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respuestas", "Pregunta_Id", "dbo.Preguntas");
            DropForeignKey("dbo.Preguntas", "Categoria_Id", "dbo.Categorias");
            DropIndex("dbo.Respuestas", new[] { "Pregunta_Id" });
            DropIndex("dbo.Preguntas", new[] { "Categoria_Id" });
            DropTable("dbo.Respuestas");
            DropTable("dbo.Preguntas");
        }
    }
}

namespace preguntados_ppodgaiz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuario_tabla : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioTablas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Categoria_Id = c.Guid(),
                        Pregunta_Id = c.Guid(),
                        Respuesta_Id = c.Guid(),
                        usuario_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categorias", t => t.Categoria_Id)
                .ForeignKey("dbo.Preguntas", t => t.Pregunta_Id)
                .ForeignKey("dbo.Respuestas", t => t.Respuesta_Id)
                .ForeignKey("dbo.Usuarios", t => t.usuario_Id)
                .Index(t => t.Categoria_Id)
                .Index(t => t.Pregunta_Id)
                .Index(t => t.Respuesta_Id)
                .Index(t => t.usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioTablas", "usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioTablas", "Respuesta_Id", "dbo.Respuestas");
            DropForeignKey("dbo.UsuarioTablas", "Pregunta_Id", "dbo.Preguntas");
            DropForeignKey("dbo.UsuarioTablas", "Categoria_Id", "dbo.Categorias");
            DropIndex("dbo.UsuarioTablas", new[] { "usuario_Id" });
            DropIndex("dbo.UsuarioTablas", new[] { "Respuesta_Id" });
            DropIndex("dbo.UsuarioTablas", new[] { "Pregunta_Id" });
            DropIndex("dbo.UsuarioTablas", new[] { "Categoria_Id" });
            DropTable("dbo.UsuarioTablas");
        }
    }
}

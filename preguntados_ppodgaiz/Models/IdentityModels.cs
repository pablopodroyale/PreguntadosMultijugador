using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using preguntados_ppodgaiz.Models.Dominio;

namespace preguntados_ppodgaiz.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<LogEvento> logEventos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Pregunta> preguntas { get; set; }
        public DbSet<Respuesta> respuestas { get; set; }
        public DbSet<PreguntasRespondidas> usuariosTabla { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()

        {
            return new ApplicationDbContext();
        }
        
    }
}
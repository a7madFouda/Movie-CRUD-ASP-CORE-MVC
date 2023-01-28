using Microsoft.EntityFrameworkCore;

namespace coreMVC.Models
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder option)
        // {
        //     option.UseSqlServer("Data Source=DESKTOP-3J3MF5I\\SQLEXPRESS;Initial Catalog=coreMVC;Integrated Security=True;Trusted_Connection=True");
        // }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
    }
}

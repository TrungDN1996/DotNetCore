    using Microsoft.EntityFrameworkCore;
    using Web_App.Models;
    using Web_App.Entities;
    
    namespace Web_App.Helpers
    {
        public class DBContext : DbContext
        {
            public DBContext(DbContextOptions options)
                : base(options)
            {
            }
            
            public DbSet<Vacancy> Vacancies { get; set; }
            
            public DbSet<Candidate> Candidates { get; set; }
            
            public DbSet<Role> Roles { get; set; }
            
            public DbSet<User> Users { get; set; }
        }
    } 
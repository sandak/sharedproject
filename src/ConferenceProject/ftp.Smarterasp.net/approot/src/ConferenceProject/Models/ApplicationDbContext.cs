using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using ConferenceProject.Models;

namespace ConferenceProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            
        }
        public DbSet<ConferenceItem> ConferenceItemList { get; set; }
        public DbSet<IdentityRole> IdentityRoleList { get; set; }
        public DbSet<Lecturer> LecturersList { get; set; }
        public DbSet<UsersConferenceItems> UsersConferenceItemsList { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Properties> Properties { get; set; }
    }
}

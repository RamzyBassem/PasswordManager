using Manager.DAL.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Manager.DAL.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            builder.Entity<Employee>().ToTable("Employees");
        }
        private void SeedUsers(ModelBuilder builder)
        {
            Employee user = new Employee()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                NormalizedUserName = "ADMIN"

            };

            PasswordHasher<Employee> passwordHasher = new PasswordHasher<Employee>();
            passwordHasher.HashPassword(user, "Admin*123");
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");
            builder.Entity<Employee>().HasData(user);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstProject.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FirstProject.Data
{
public class MyDBContext : IdentityDbContext<ApplicationUser>
    {
        public MyDBContext (DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public DbSet<FirstProject.Domain.ArticlesDTO> Articles { get; set; }

        public DbSet<FirstProject.Domain.Author> Author { get; set; }

        public DbSet<FirstProject.Domain.TipsForEveryOne> TipsForEveryOne { get; set; }

        public DbSet<FirstProject.Domain.FuelWise> FuelWise { get; set; }

        public DbSet<FirstProject.Domain.FileStorage> FileStorage { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace demo.DAL.Context
{
    public class MvcAppDbContext :IdentityDbContext<ApplicationUser>  //zy eldbcontext bs 3leha el identity whywrs el 6 7 dbset l lehom 3laqa bel identity zy users /roles
    {
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=. ; Database=MvcAppDb ; Trusted_Connection=True;");
        //}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<IdentityUser> Users { get; set; }
        //public DbSet<IdentityUser> Roles { get; set; }

    }
}

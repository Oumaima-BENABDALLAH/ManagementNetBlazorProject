using BasicLibrary.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Data
{
    public  class AppDbContext (DbContextOptions<AppDbContext> options):DbContext (options)
    {


        public DbSet<Employee> Employee { get; set; }

        // GeneralDepartment /Department/ Branch
        public DbSet<GeneralDepartment> GeneralDepartment { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Branch> Branch { get; set; }


        //Town Country / City
   
        public DbSet<Town> Towns { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<City> cities { get; set; }

        //public DbSet<Country> countries { get; set; }

        public DbSet<RefreshTokenInfo> RefreshTokenInfos { get; set; }

        public DbSet<Vaction> Vactions { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<OverTimeType> OvertimeTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }


    }
}

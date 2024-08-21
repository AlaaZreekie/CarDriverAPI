using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Data.DBContext;

public class ApplicationDBContext : IdentityDbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
    {

    }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    public DbSet<CarDriver> CarsDrivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Car>().HasKey(c => c.Id);
        //modelBuilder.Entity<Car>().HasCheckConstraint(" k", "");
        /* modelBuilder.Entity<Car>(t =>
         {
            *//* t.HasCheckConstraint("CheckDorrs", "NumberOfDoors = 2 or NummberOfDoors = 4");*//*
         });*/

        modelBuilder.Entity<Driver>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);

        modelBuilder.Entity<CarDriver>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<CarDriver>(t =>
        {
            t.HasOne(c => c.Car)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(ic => ic.CarId);

            t.HasOne(d => d.Driver)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(id => id.DriverId);

            t.HasIndex(l => new { l.CarId, l.StartDate, l.EndDate })
             .IsUnique();


            /* t.HasCheckConstraint("CK_NoOverlappingLeases",
                 "(SELECT * FROM \"CarsDrivers\" \"l2\" WHERE (\"l2\".\"CarId\" = \"CarId\") AND  (\"StartDate\" < \"l2\".\"EndDate\" AND \"EndDate\" > \"l2\".\"StartDate\"))");
             
            t.HasCheckConstraint("CK_Lease_NoOverlap","NOT EXISTS (SELECT * FROM Leases l2 WHERE l2.CarId = CarId AND (StartDate =< l2.EndDate AND EndDate >= l2.StartDate))");

                t.HasCheckConstraint("CK_Blog_TooFewBits", "Id > 1023");
             
             */




        });
        base.OnModelCreating(modelBuilder);
    }

}
/*t.HasIndex(l => new { l.CarId, l.StartDate, l.EndDate })
          .IsUnique();*/
/*t.HasCheckConstraint("CK_Lease_NoOverlap",
    "NOT EXISTS (SELECT 1 FROM Leases l2 WHERE l2.CarId = CarId AND " +
    "(StartDate =< l2.EndDate AND EndDate >= l2.StartDate))");
*/
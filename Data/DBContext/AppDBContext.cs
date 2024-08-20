using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data.DBContext;

public class ApplicationDBContext : IdentityDbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> option) : base(option)
    {

    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    public DbSet<CarsDrivers> CarsDrivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Car>().HasKey(c => c.Id);

        modelBuilder.Entity<Driver>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);

        modelBuilder.Entity<CarsDrivers>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<CarsDrivers>(t =>
        {
            t.HasKey(cd => new { cd.carId, cd.driverId });

            t.HasOne(c => c.car)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(ic => ic.carId);

            t.HasOne(d => d.driver)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(id => id.driverId);


        });

        /*modelBuilder.Entity<Driver>()
       .HasMany(c => c.Leas)
       .WithMany(d => d.Leas)
       .UsingEntity("CarsDrivers", c =>
       {
           return c.HasOne(typeof(Car)).WithMany().HasForeignKey("CarId");

       }, d =>
       {
           return d.HasOne(typeof(Driver)).WithMany().HasForeignKey("DriverId");
       });*/



        base.OnModelCreating(modelBuilder);
    }

}
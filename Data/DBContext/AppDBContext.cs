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

    public DbSet<Leas> CarsDrivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Car>().HasKey(c => c.Id);

        modelBuilder.Entity<Driver>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);

        modelBuilder.Entity<Leas>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Leas>(t =>
        {
            t.HasKey(cd => new { cd.CarId, cd.DriverId });

            t.HasOne(c => c.Car)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(ic => ic.CarId);

            t.HasOne(d => d.Driver)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(id => id.DriverId);
            t.HasIndex(l => new { l.CarId, l.StartDate, l.EndDate })
                  .IsUnique(); 
            t.HasCheckConstraint("CK_Lease_NoOverlap",
                "NOT EXISTS (SELECT 1 FROM Leases l2 WHERE l2.CarId = CarId AND " +
                "(StartDate =< l2.EndDate AND EndDate >= l2.StartDate))");


        });
        base.OnModelCreating(modelBuilder);
    }

}
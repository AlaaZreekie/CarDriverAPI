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
        modelBuilder.Entity<Car>(c =>
        {
            c.Property(c => c.Id).ValueGeneratedOnAdd();
            c.HasKey(c => c.Id);
        });

        modelBuilder.Entity<Driver>().Property(d => d.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);

        modelBuilder.Entity<Leas>(t =>
        {
            t.ToTable("Lease");
            t.Property(d => d.Id).ValueGeneratedOnAdd();
            t.HasKey(cd => new { cd.CarId, cd.DriverId });

            t.HasOne(c => c.Car)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(ic => ic.CarId);

            t.HasOne(d => d.Driver)
             .WithMany(cd => cd.Leas)
             .HasForeignKey(id => id.DriverId);

            // TODO: Alaa, Add time interval here

        });

        base.OnModelCreating(modelBuilder);
    }

}
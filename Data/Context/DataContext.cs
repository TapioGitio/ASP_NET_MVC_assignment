using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<MemberEntity> Members { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<MemberEntity>()
            .HasOne(m => m.Address) 
            .WithMany()
            .HasForeignKey(m => m.AddressId)
            .OnDelete(DeleteBehavior.SetNull); 
    
    }

}

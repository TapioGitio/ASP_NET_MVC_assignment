using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<MemberEntity>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<NotificationTypeEntity> NotificationTypes { get; set; }
    public DbSet<NotificationTargetGroupEntity> NotificationTargetGroups { get; set; }
    public DbSet<NotificationDismissedEntity> NotificationDismissed { get; set; }

}

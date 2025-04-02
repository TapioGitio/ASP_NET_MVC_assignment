using Data.Context;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class NotificationRepository(DataContext context) : BaseRepository<NotificationEntity>(context), INotificationRepository
{
    public async Task<IEnumerable<NotificationEntity?>> GetAllIncludeAllAsync()
    {
        var result = await _dbSet
            .Include(x => x.NotificationType)
            .Include(x => x.NotificationTargetGroup)
            .Include(x => x.DismissedNotification)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<NotificationEntity?>> GetAllAsync()
    {
        var result = await _dbSet
            .Include(x => x.NotificationType)
            .Include(x => x.NotificationTargetGroup)
            .Include(x => x.DismissedNotification)
            .ToListAsync();

        return result;
    }
}

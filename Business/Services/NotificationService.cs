using Business.Interfaces;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class NotificationService(DataContext context) : INotificationService
{
    private readonly DataContext _context = context;

    public async Task AddNotificationAsync(NotificationEntity entity)
    {
        if (string.IsNullOrEmpty(entity.Image))
        {
            switch (entity.NotificationTypeId)
            {
                case 1:
                    entity.Image = "/images/avatar-orange.svg";
                        break;
                case 2:
                    entity.Image = "/images/alpha-icon-green.svg";
                        break;
            }
        }

        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NotificationEntity>> GetNotificationsAsync(string userId, int take = 10)
    {
        var dismissedIds = await _context.NotificationDismissed
            .Where(x => x.UserId == userId)
            .Select(x => x.NotificationId)
            .ToListAsync();

        var notifications = await _context.Notifications
            .Where(x => !dismissedIds.Contains(x.Id))
            .OrderByDescending(x => x.Created)
            .Take(take)
            .ToListAsync();

        return notifications;

    }

    public async Task DismissNotificationAsync(string notificationId, string userId)
    {
        var dismissed = await _context.NotificationDismissed.AnyAsync(x => x.NotificationId == notificationId && x.UserId == userId);
        if (!dismissed)
        {
            var dismissedEntity = new NotificationDismissedEntity
            {
                NotificationId = notificationId,
                UserId = userId,
            };

            _context.Add(dismissedEntity);
            await _context.SaveChangesAsync();
        }
    }
}

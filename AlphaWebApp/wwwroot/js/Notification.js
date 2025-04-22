const connection = new SignalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("RecieveNotification", function (notification) {
    const notifications = document.querySelector('.dropdown-menu')

    const item = document.createElement('li')
    item.className = 'dropdown-item'
    item.setAttribute('data-id', notification.id)
    item.innerHTML =
        `
            <div class="dropdown-item-image"
                <img src="${notification.image}">
            </div>
            <div class="dropdown-item-text">
                <p class="dropdown-item-header">${notification.message}</p>
                <p class="dropdown-item-footer" data-created="${new Date(notification.created).toISOString()}">${notification.created}</p>
            </div>
            <button class="btn-exit" onclick="dismissNotification('${notification.id}')"><i class="bi bi-x"></i></button>
        `
    /* Placing where they will spawn */
    notifications.insertBefore(item, notifications.firstChild)
    updateRelativeTimes()
    updateNotificationCount()

})

connection.on("NotificationDismissed", function (notificationId) {
    const element = document.querySelector(`.dropdown-item[data-id="${notificationId}"]`)
    if (element) {
        element.remove()
        updateNotificationCount()
    }
})

connection.start().catch(error => console.error(error))



/* Handling removing notifications */
async function dismissNotification(notificationId) {
    try {
        const res = await fetch(`/api/notifications/dismiss/${notificationId}`, { method: 'POST' });
        if (res.ok) {
            removeNotification(notificationId);
        } else {
            console.error('Error removing notification');
        }
    } catch (error) {
        console.error('Error removing notification: ', error);
    }
}

function removeNotification(notificationId) {
    const element = document.querySelector(`.dropdown-item[data-id='${notificationId}']`);
    if (element) {
        element.remove();
        updateNotificationCount();
    }
}


/* Update counter and add dot */
function updateNotificationCount() {
    const notifications = document.querySelector('.dropdown-menu')
    const number = document.querySelector('.dropdown-header-number')
    const button = document.querySelector('#notification-dropdown-button')
    const count = notifications.querySelectorAll('.dropdown-item').length

    if (number) {
        number.textContent = count
    }

    let dot = button.querySelector('.red-dot')
    if (count > 0 && !dot) {
        dot = document.createElement('div')
        dot.className = 'red-dot'
        button.appendChild(dot)
    }

    if (count === 0 && dot) {
        dot.remove()
   
    }
}
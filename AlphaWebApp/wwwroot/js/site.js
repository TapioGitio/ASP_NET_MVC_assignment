document.addEventListener('DOMContentLoaded', () => {
    initializeDropdowns();
    updateRelativeTimes();
    updateTimeLeft();
    setInterval(updateRelativeTimes, 60000);
});

function closeAllDropdowns(exceptDropdown, dropdownEl) {
    dropdownEl.forEach(dropdown => {
        if (dropdown !== exceptDropdown) {
            dropdown.classList.remove('show');
        }
    });
}

function initializeDropdowns() {
    const dropdownTriggers = document.querySelectorAll('[data-type="dropdown"]');

    const dropdownEl = new Set();
    dropdownTriggers.forEach(trigger => {
        const targetSelector = trigger.getAttribute('data-target');
        if (targetSelector) {
            const dropdown = document.querySelector(targetSelector);
            if (dropdown) {
                dropdownEl.add(dropdown);
            }
        }
    });

    dropdownTriggers.forEach(trigger => {
        trigger.addEventListener('click', (e) => {
            e.stopPropagation();
            const targetSelector = trigger.getAttribute('data-target');
            if (!targetSelector) return;

            const dropdown = document.querySelector(targetSelector);
            if (!dropdown) return;

            closeAllDropdowns(dropdown, dropdownEl);
            dropdown.classList.toggle('show');
        });
    });

    dropdownEl.forEach(dropdown => {
        dropdown.addEventListener('click', (e) => {
            e.stopPropagation();
        });
    });

    document.addEventListener('click', () => {
            closeAllDropdowns(null, dropdownEl);
    });
}

/* Userfriendly displaying of time */
function updateRelativeTimes() {
    const elements = document.querySelectorAll('.dropdown-item-footer')
    const now = new Date()

    elements.forEach(el => {
        const created = new Date(el.getAttribute('data-created'))
        if (isNaN(created.getTime())) return

        const diff = now - created
        const diffSeconds = Math.floor(diff / 1000);
        const diffMinutes = Math.floor(diffSeconds / 60)
        const diffHours = Math.floor(diffMinutes / 60)
        const diffDays = Math.floor(diffHours / 24)
        const diffWeeks = Math.floor(diffDays / 7)

        let relativeTime = ''

        if (diffMinutes < 1) {
            relativeTime = 'just now'
        } else if (diffMinutes < 60) {
            relativeTime = diffMinutes + ' min ago'
        } else if (diffHours < 2) {
            relativeTime = diffHours + ' hour ago'
        } else if (diffHours < 24) {
            relativeTime = diffHours + ' hours ago'
        } else if (diffDays < 2) {
            relativeTime = diffDays + ' day ago'
        } else if (diffDays < 7) {
            relativeTime = diffDays + ' days ago'
        } else {
            relativeTime = diffWeeks + ' weeks ago'
        }
        el.textContent = relativeTime
    });
}

// got help to update the time left, this was hard to figure out
// alot of dividing and multiplying to get the right time left.
function updateTimeLeft() {
    const elements = document.querySelectorAll('.time-left') // Select all elements with the class 'time-left'.
    const now = new Date()  // Get the current date and time

    elements.forEach(el => {
        const endTime = new Date(el.getAttribute('data-end')) // Get the end time from the data attribute.
        if (isNaN(endTime.getTime())) return // Check if the end time is valid if not then return.

        const diff = endTime - now // Calculate the difference between the end time and the current time.

        if (diff <= 0) {
            el.textContent = 'Time is up'
            return
        }

        const diffSeconds = Math.floor(diff / 1000) // Convert to seconds.
        const diffMinutes = Math.floor(diffSeconds / 60) // Convert to minutes.
        const diffHours = Math.floor(diffMinutes / 60) // Convert to hours.
        const diffDays = Math.floor(diffHours / 24) // Convert to days.
        const diffWeeks = Math.floor(diffDays / 7)  // Convert to weeks.

        let timeLeft = '' 
        let badge = el.closest('.notice') 

        if (!badge) return

        // Check the difference in time and set the class and text.
        if (diffDays < 1) {
            timeLeft = diffHours + ' hours left'
            badge.classList.add('notice-red')
            badge.classList.remove('notice-gray')
            badge.classList.remove('notice-yellow')
        } else if (diffDays == 1) {
            timeLeft = diffDays + ' day left'
            badge.classList.add('notice-red')
            badge.classList.remove('notice-gray')
            badge.classList.remove('notice-yellow')
        } else if (diffWeeks < 1) {
            timeLeft = diffDays + ' days left'
            badge.classList.add('notice-red')
            badge.classList.remove('notice-gray')
            badge.classList.remove('notice-yellow')
        } else if (diffWeeks == 1) {
            timeLeft = diffWeeks + ' week left'
            badge.classList.add('notice-yellow')
            badge.classList.remove('notice-gray')
            badge.classList.remove('notice-red')
        } else {
            timeLeft = diffWeeks + ' weeks left'
            badge.classList.add('notice-gray')
            badge.classList.remove('notice-yellow')
            badge.classList.remove('notice-red')
        }

        el.textContent = timeLeft
    });
}
// Handle darkmode
const darkmodeSwitch = document.querySelector('#darkmode-switch');

// Add event listener if the switch exists
if (darkmodeSwitch) {
    darkmodeSwitch.checked = localStorage.getItem('darkmode') === 'on';

    darkmodeSwitch.addEventListener('change', () => {
        if (darkmodeSwitch.checked) {
            enableDarkMode();
            localStorage.setItem('darkmode', 'on');
        } else {
            disableDarkMode();
            localStorage.setItem('darkmode', 'off');
        }
    });
}
// Darkmode functions
function enableDarkMode() {
    if (darkmodeSwitch) darkmodeSwitch.checked = true;
    document.documentElement.classList.add('dark');
}

function disableDarkMode() {
    if (darkmodeSwitch) darkmodeSwitch.checked = false;
    document.documentElement.classList.remove('dark');
}
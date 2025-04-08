﻿document.addEventListener('DOMContentLoaded', () => {
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

function updateTimeLeft() {
    const elements = document.querySelectorAll('.time-left')
    const now = new Date()

    elements.forEach(el => {
        const endTime = new Date(el.getAttribute('data-end'))
        if (isNaN(endTime.getTime())) return

        const diff = endTime - now

        if (diff <= 0) {
            el.textContent = 'Time is up'
            return
        }

        const diffSeconds = Math.floor(diff / 1000)
        const diffMinutes = Math.floor(diffSeconds / 60)
        const diffHours = Math.floor(diffMinutes / 60)
        const diffDays = Math.floor(diffHours / 24)
        const diffWeeks = Math.floor(diffDays / 7)

        let timeLeft = ''
        let badge = el.closest('.notice')

        if (!badge) return

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
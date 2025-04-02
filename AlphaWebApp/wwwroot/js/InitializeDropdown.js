document.addEventListener('DOMContentLoaded', () => {
    initializeDropdowns();
    updateRelativeTimes();
    setInterval(updateRelativeTimes, 60000);
});

function closeAllDropdowns(exceptDropdown, dropdownEl) {
    dropdownEl.foreach(dropdown => {
        if (dropdown !== exceptDropdown) {
            dropdown.classList.remove('show')
        }
    })
}

function initializeDropdowns() {
    const dropdownTriggers = document.querySelectorAll('[data-type="dropdown"]')

    const dropdownEl = new Set()
    dropdownTriggers.forEach(trigger => {
        const targetSelector = trigger.getAttribute('data-target')
        if (targetSelector) {
            const dropdown = document.querySelector(targetSelector)
            if (dropdown) {
                dropdownEl.add(dropdown)
            }
        }
    })

    dropdownTriggers.forEach(trigger => {
        trigger.addEventListener('click', (e) => {
            e.stopPropagation()
            const targetSelector = trigger.getAttribute('data-target')
            if (!targetSelector) return

            const dropdown = document.querySelector(targetSelector)
            if (!dropdown) return

            closeAllDropdowns(dropdown, dropdownEl)
            dropdown.classList.toggle('show')
        })
    })

    dropdownEl.forEach(dropdown => {
        dropdown.addEventListener('click', (e) => {
            e.stopPropagation()
        })
    })

    document.addEventListener('click', () => {
        closeAllDropdowns(null, dropdownEl)
    })
}

/* Using bootstrap dropdown, added this incase I need this in the future */
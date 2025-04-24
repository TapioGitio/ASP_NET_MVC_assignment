// Handle darkmode
const darkmodeSwitch = document.querySelector('#darkmode-switch');
const hasDarkmode = localStorage.getItem('darkmode');

// Check initial state
if (hasDarkmode == null) {
    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        enableDarkMode();
    } else {
        disableDarkMode();
    }
} else if (hasDarkmode === 'on') {
    enableDarkMode();
} else if (hasDarkmode === 'off') {
    disableDarkMode();
}

// Add event listener if the switch exists
if (darkmodeSwitch) {
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

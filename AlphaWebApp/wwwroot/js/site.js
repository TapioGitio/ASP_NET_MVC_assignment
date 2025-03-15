document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 125

    // Open Modal
    const modalButtons = document.querySelectorAll('[data-modal="true"]')
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target')
            const modal = document.querySelector(modalTarget)

            if (modal)
                modal.style.display = 'flex';
        })
    })


    // Close Modal
    const closeButtons = document.querySelectorAll('[data-close="true"]')
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal')

            if (modal) {
                modal.style.display = 'none';

                modal.querySelectorAll('form').forEach(form => {
                    form.reset()

                    const imagePreview = form.querySelector('.image-preview')
                    if (imagePreview)
                        imagePreview.src = '';

                    const imagePreviewer = form.querySelector('.image-previewer')
                    if (imagePreviewer)
                        imagePreviewer.classList.remove('selected')
                })

            }
        })
    })

    // Handle image-previewer
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]')
        const imagePreview = previewer.querySelector('.image-preview')

        previewer.addEventListener('click', () => fileInput.click())

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0]
            if (file)
                processImage(file, imagePreview, previewer, previewSize)
        })
    })


    // Handle submit forms
    const forms = document.querySelectorAll('form')
    forms.forEach(form => {
        form.addEventListener('submit', async (e) => {
            e.preventDefault()

            clearErrorMessage(form)

            const formData = new FormData(form)

            try {
                const res = await fetch(form.action, {
                    method: 'post',
                    body: formData
                })

                if (res.ok) {
                    const modal = form.closest('.modal')
                    if (modal)
                        modal.style.display = 'none';

                    window.location.reload()
                }
                else if (!res.ok) {
                    const data = await res.json()

                    if (data.errors) {
                        Object.keys(data.errors).forEach(key => {
                            const input = form.querySelector('[name="${key}"]')
                            if (input) {
                                input.classList.add('input-validation-error')
                            }

                            const span = form.querySelector('[data-valmsg-for="${key}"]')
                            if (span) {
                                span.innerText = data.errors[key].join('\n')
                                span.classList.add('field-validation-error')
                            }
                        })
                    }
                }
            }
            catch {
                console.log('Error submitting the form')
            }
        })
    })


    // Handle darkmode
    const darkmodeSwitch = document.querySelector('#darkmode-switch')
    const hasDarkmode = localStorage.getItem('darkmode')

    if (hasDarkmode == null) {
        if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
            enableDarkMode()
        } else {
            disableDarkMode()
        }
    } else if (hasDarkmode === 'on') {
        enableDarkMode()
    } else if (hasDarkmode === 'off') {
        disableDarkMode()
    }


    darkmodeSwitch.addEventListener('change', () => {
        if (darkmodeSwitch.checked) {
            enableDarkMode()
            localStorage.setItem('darkmode', 'on')
        } else {
            disableDarkMode()
            localStorage.setItem('darkmode', 'off')
        }
    })

})

// Darkmode
function enableDarkMode() {
    darkmodeSwitch.checked = true
    document.documentElement.classList.add('dark')
}
function disableDarkMode() {
    darkmodeSwitch.checked = false
    document.documentElement.classList.remove('dark')

}

// Clear errors
function clearErrorMessage(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error')
    })

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = '';
        span.classList.remove('field-validation-error')
    })
}


// Process image
async function processImage(file, imagePreview, previewer, previewSize = 125) {
    try {
        const img = await loadImage(file)
        const canvas = document.createElement('canvas')
        canvas.width = previewSize
        canvas.height = previewSize

        const ctx = canvas.getContext('2d')
        ctx.drawImage(img, 0, 0, previewSize, previewSize)
        imagePreview.src = canvas.toDataURL('image/jpeg')
        previewer.classList.add('selected')
    }
    catch (error) {
        console.error('Image-processing failed', error)
    }
}

// Load image
async function loadImage(file) {
    return new Promise((resole, reject) => {
        const reader = new FileReader()

        reader.onerror = () => reject(new Error("Failed to load file"))
        reader.onload = (e) => {
            const img = new Image()
            img.onerror = () => reject(new Error("Failed to load image"))
            img.onload = () => resole(img)
            img.src = e.target.result
        }

        reader.readAsDataURL(file)
    })
}
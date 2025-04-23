document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 125;

    // Open Modal
    const modalButtons = document.querySelectorAll('[data-modal="true"]');
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);

            if (modal)
                modal.style.display = 'flex';

            const Id = button.getAttribute('data-id') || '';
            const hiddenField = modal.querySelector('[name="UpdateFormData.Id"]');

            if (hiddenField) {
                hiddenField.value = Id;
            }

            // Fetch project data and populate the form
            if (Id) {
                fetchProjectData(Id, modal);
            }
        });
    });

    // Close Modal
    const closeButtons = document.querySelectorAll('[data-close="true"]');
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal');

            if (modal) {
                modal.style.display = 'none';

                modal.querySelectorAll('form').forEach(form => {
                    form.reset();

                    const imagePreview = form.querySelector('.image-preview');
                    if (imagePreview) imagePreview.src = '';

                    const imagePreviewer = form.querySelector('.image-previewer');
                    if (imagePreviewer) imagePreviewer.classList.remove('selected');

                    clearErrorMessage(form);
                });
            }
        });
    });

    // Handle image-previewer
    document.querySelectorAll('.image-previewer').forEach(previewer => {
        const fileInput = previewer.querySelector('input[type="file"]');
        const imagePreview = previewer.querySelector('.image-preview');

        previewer.addEventListener('click', () => fileInput.click());

        fileInput.addEventListener('change', ({ target: { files } }) => {
            const file = files[0];
            if (file) {
                processImage(file, imagePreview, previewer, previewSize);
            }
        });
    });

    // Fetch project data and populate the form
    async function fetchProjectData(Id, modal) {
        try {
            const res = await fetch(`/Project/Edit?id=${Id}`);

            const populate = await res.json();
            populateEditForm(modal, populate);
        } catch (error) {
            console.error('Error fetching project data:', error);
        }
    }

    // Populate the form with project data
    function populateEditForm(modal, data) {
        modal.querySelector('[name="UpdateFormData.ProjectName"]').value = data.updateFormData.projectName || '';
        modal.querySelector('[name="UpdateFormData.ClientName"]').value = data.updateFormData.clientName || '';
        modal.querySelector('[name="UpdateFormData.ProjectDescription"]').value = data.updateFormData.projectDescription || '';
        modal.querySelector('[name="UpdateFormData.StartDate"]').value = data.updateFormData.startDate || '';
        modal.querySelector('[name="UpdateFormData.EndDate"]').value = data.updateFormData.endDate || '';
        modal.querySelector('[name="UpdateFormData.Budget"]').value = data.updateFormData.budget || '';
        modal.querySelector('[name="UpdateFormData.IsCompleted"]').checked = data.updateFormData.isCompleted || false;


        // Update the image preview
        const imagePreview = modal.querySelector('.image-preview');
        const imagePreviewer = modal.querySelector('.image-previewer')

        if (data.updateFormData.projectImagePath) {
            imagePreview.src = data.updateFormData.projectImagePath;
            imagePreviewer.classList.add('selected')
        } else {
            imagePreview.src = '';
        }
    }
});

// Clear errors
function clearErrorMessage(form) {
    form.querySelectorAll('[data-val="true"]').forEach(input => {
        input.classList.remove('input-validation-error');
    });

    form.querySelectorAll('[data-valmsg-for]').forEach(span => {
        span.innerText = '';
        span.classList.remove('field-validation-error');
    });
}

// Process image
async function processImage(file, imagePreview, previewer, previewSize = 125) {
    try {
        const img = await loadImage(file);
        const canvas = document.createElement('canvas');
        canvas.width = previewSize;
        canvas.height = previewSize;

        const ctx = canvas.getContext('2d');
        ctx.drawImage(img, 0, 0, previewSize, previewSize);
        imagePreview.src = canvas.toDataURL('image/jpeg');
        previewer.classList.add('selected');
    } catch (error) {
        console.error('Image-processing failed', error);
    }
}

// Load image
async function loadImage(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();

        reader.onerror = () => reject(new Error("Failed to load file"));
        reader.onload = (e) => {
            const img = new Image();
            img.onerror = () => reject(new Error("Failed to load image"));
            img.onload = () => resolve(img);
            img.src = e.target.result;
        };

        reader.readAsDataURL(file);
    });
}

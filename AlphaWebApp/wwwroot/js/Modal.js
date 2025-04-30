document.addEventListener('DOMContentLoaded', () => {
    const previewSize = 125;

    // Open Modal
    const modalButtons = document.querySelectorAll('[data-modal="true"]');
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);
            const modalId = modal.id;

            if (modal)
                modal.style.display = 'flex';

            const Id = button.getAttribute('data-id') || '';
            const hiddenField = modal.querySelector('[name="UpdateFormData.Id"]');

            if (hiddenField)
                hiddenField.value = Id;

            // Fetch project data and populate the form
            if (Id) {
                if (modalId === 'editProjectModal')
                    fetchProjectData(Id, modal)

                else if (modalId === 'addMemberModal')
                    fetchMemberData(Id)    
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

            const data = await res.json();
            populateEditForm(modal, data);
        } catch (error) {
            console.error('Error fetching project data:', error);
        }
    }
    async function fetchMemberData(Id) {
        try {
            const res = await fetch(`/Project/EditMembers?id=${Id}`);

            const data = await res.json();
            populateMemberForm(data);
        } catch (error) {
            console.error('Error fetching member data:', error);
        }
    }

    // Populate the form with project data,
    // got help with this to populate the form,
    // it fetches each value from the data object and 
    // assigns it to the corresponding input field.
    function populateEditForm(modal, data) {
        modal.querySelector('[name="UpdateFormData.ProjectName"]').value = data.updateFormData.projectName || '';
        modal.querySelector('[name="UpdateFormData.ClientName"]').value = data.updateFormData.clientName || '';
        modal.querySelector('[name="UpdateFormData.StartDate"]').value = data.updateFormData.startDate || '';
        modal.querySelector('[name="UpdateFormData.EndDate"]').value = data.updateFormData.endDate || '';
        modal.querySelector('[name="UpdateFormData.Budget"]').value = data.updateFormData.budget || '';
        modal.querySelector('[name="UpdateFormData.IsCompleted"]').checked = data.updateFormData.isCompleted || false;

        // Populate the WYSIWYG editor
        const textarea = modal.querySelector('[name="UpdateFormData.ProjectDescription"]');
        const description = data.updateFormData.projectDescription || '';
        textarea.value = description;
        initializeEditWysiwyg(description);

        // Populate the image preview
        const imagePreview = modal.querySelector('.image-preview');
        const imagePreviewer = modal.querySelector('.image-previewer')

        if (data.updateFormData.projectImagePath) {
            imagePreview.src = data.updateFormData.projectImagePath;
            imagePreviewer.classList.add('selected')
        } else {
            imagePreview.src = '';
        }

        // Populate the tags for the EditProject
        initTagSelector({
            containerId: 'edit-project-tags',
            inputId: 'edit-project-tag-search',
            selectedInputIds: 'UpdateFormData_SelectedMemberIds',
            resultsId: 'edit-project-tag-search-results',
            searchUrl: (query) => '/Tags/SearchTags?term=' + encodeURIComponent(query),
            displayProperty: 'tagName',
            imageProperty: 'imageUrl',
            tagClass: 'user-tag',
            avatarFolder: '',
            emptyMessage: 'No tags found.',
            preselected: data.updateFormData.memberTags || []
        });
    }

    // Populate the tags for the AddMember
    function populateMemberForm(data) {
        initTagSelector({
            containerId: 'add-member-tags',
            inputId: 'add-member-tag-search',
            selectedInputIds: 'Member_SelectedMemberIds',
            resultsId: 'add-member-tag-search-results',
            searchUrl: (query) => '/Tags/SearchTags?term=' + encodeURIComponent(query),
            displayProperty: 'tagName',
            imageProperty: 'imageUrl',
            tagClass: 'user-tag',
            avatarFolder: '',
            emptyMessage: 'No tags found.',
            preselected: data.memberFormData.memberTags || []
        });
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

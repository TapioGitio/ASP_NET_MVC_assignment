let editQuill;

function initializeEditWysiwyg(content) {
    const textarea = document.getElementById('UpdateFormData_ProjectDescription');
    editQuill = initializeWysiwyg('#p-edit-wysiwyg-editor', '#p-edit-wysiwyg-toolbar', textarea.id, content);
}
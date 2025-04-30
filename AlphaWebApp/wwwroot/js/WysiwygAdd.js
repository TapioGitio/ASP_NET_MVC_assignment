document.addEventListener("DOMContentLoaded", function () {
    const textarea = document.getElementById('FormData_ProjectDescription');
    const content = textarea.value || '';
    initializeWysiwyg('#p-add-wysiwyg-editor', '#p-add-wysiwyg-toolbar', textarea.id, content);
});
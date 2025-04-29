document.addEventListener("DOMContentLoaded", function () {
    const quill = null;
    const projectDescription = document.getElementById('UpdateFormData_ProjectDescription').value
    initializeWysiwyg('#p-edit-wysiwyg-editor', '#p-edit-wysiwyg-toolbar', 'UpdateFormData_ProjectDescription', projectDescription)
})

function initializeWysiwyg(wysiwygEditorId, wysiwygToolbarId, textareaId, content) {
    const textarea = document.getElementById(textareaId);
    quill = new Quill(wysiwygEditorId, {
        modules: {
            syntax: true,
            toolbar: wysiwygToolbarId
        },
        placeholder: 'Type Something',
        theme: 'snow'
    })

    if (content)
        quill.root.innerHTML = content;

    quill.on('text-change', () => {
        textarea.value = quill.root.innerHTML
    })
}
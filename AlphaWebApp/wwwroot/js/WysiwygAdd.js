﻿document.addEventListener("DOMContentLoaded", function () {
    const projectDescription = document.getElementById('FormData_ProjectDescription').value
    initializeWysiwyg('#p-add-wysiwyg-editor', '#p-add-wysiwyg-toolbar', 'FormData_ProjectDescription', projectDescription)
})

function initializeWysiwyg(wysiwygEditorId, wysiwygToolbarId, textareaId, content) {

    const textarea = document.getElementById(textareaId);
    const quill = new Quill(wysiwygEditorId, {
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
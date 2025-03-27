document.addEventListener('DOMContentLoaded', function () {
   
    document.querySelectorAll('form').forEach(form => enableValidation(form));

    
    document.querySelectorAll('.modal').forEach(modal => {
        modal.addEventListener('shown.bs.modal', function () {
            let form = modal.querySelector('form');
            if (form)
                enableValidation(form);
        });
    });
});

function enableValidation(form) {
    const fields = form.querySelectorAll('input[data-val="true"]');

    fields.forEach(field => {
        field.addEventListener('input', () => validateField(field));
    });

    form.addEventListener('submit', function (e) {
        let isValid = true;

        fields.forEach(field => {
            if (!validateField(field)) {
                isValid = false;
            }
        });

        if (!isValid) {
            e.preventDefault();
        }
    });
}

function validateField(field) {
    let form = field.closest('form');
    if (!form) return true;

    let errorSpan = form.querySelector(`span[data-valmsg-for="${field.name}"]`);
    if (!errorSpan) return true;

    let errorMessage = '';
    let value = field.value.trim();

    if (field.hasAttribute("data-val-required") && value === '') {
        errorMessage = field.getAttribute("data-val-required");
    }

    if (field.hasAttribute("data-val-regex") && value !== "") {
        let patternValue = field.getAttribute("data-val-regex-pattern");
        if (patternValue) {
            try {
                let pattern = new RegExp(patternValue);
                if (!pattern.test(value)) {
                    errorMessage = field.getAttribute("data-val-regex");
                }
            } catch (e) {
                console.error("Invalid regex pattern:", patternValue, e);
            }
        }
    }

    if (errorMessage) {
        field.classList.add('input-validation-error');
        errorSpan.classList.remove('field-validation-valid');
        errorSpan.classList.add('field-validation-error');
        errorSpan.textContent = errorMessage;
        return false;
    } else {
        field.classList.remove('input-validation-error');
        errorSpan.classList.add('field-validation-valid');
        errorSpan.classList.remove('field-validation-error');
        errorSpan.textContent = '';
        return true;
    }
}
﻿function initTagSelector(config) {
    let activeIndex = -1
    let selectedIds = []

    const tagContainer = document.getElementById(config.containerId)
    const input = document.getElementById(config.inputId)
    const results = document.getElementById(config.resultsId)
    const selectedInputIds = document.getElementById(config.selectedInputIds)
    resetTags()

    if (Array.isArray(config.preselected)) {
        config.preselected.forEach(item => addTag(item))
    }

    if (!input.dataset.listenersAttached) {
        input.addEventListener('focus', () => {
            tagContainer.classList.add('focused')
            results.classList.add('focused')
        })

        input.addEventListener('blur', () => {
            setTimeout(() => {
                tagContainer.classList.remove('focused')
                results.classList.remove('focused')
            }, 100)
        })

        input.addEventListener('input', () => {
            const query = input.value.trim()
            activeIndex = -1

            if (query.length === 0) {
                results.style.display = 'none'
                results.innerHTML = ''
                return;
            }

            fetch(config.searchUrl(query))
                .then(r => r.json())
                .then(data => renderSearchResults(data))
        })

        input.addEventListener('keydown', (e) => {
            const items = results.querySelectorAll('.search-item')

            switch (e.key) {
                case 'ArrowDown':
                    e.preventDefault()
                    if (items.length > 0) {
                        activeIndex = (activeIndex + 1) % items.length
                        updateActiveItem(items)
                    }
                    break

                case 'ArrowUp':
                    e.preventDefault()
                    if (items.length > 0) {
                        activeIndex = (activeIndex - 1 + items.length) % items.length
                        updateActiveItem(items)
                    }
                    break

                case 'Enter':
                    e.preventDefault()
                    if (activeIndex >= 0 && items[activeIndex]) {
                        items[activeIndex].click()
                    }
                    break

                case 'Backspace':
                    if (input.value === '') {
                        removeLastTag()
                    }
                    break
            }
        })

        input.dataset.listenersAttached = true
    }

 
    function updateActiveItem(items) {
        items.forEach(item => item.classList.remove('active'))
        if (items[activeIndex]) {
            items[activeIndex].classList.add('active')
            items[activeIndex].scrollIntoView({ block: 'nearest' })
        }
    }

    function renderSearchResults(data) {
        results.innerHTML = ''

        if (data.length === 0) {
            const noResult = document.createElement('div')
            noResult.classList.add('search-item')
            noResult.textContent = config.emptyMessage || 'No results'
            results.appendChild(noResult)
        } else {
            data.forEach(item => {
                if (!selectedIds.includes(item.id)) {
                    const resultItem = document.createElement('div')
                    resultItem.classList.add('search-item')
                    resultItem.dataset.id = item.id

                    if (config.tagClass === 'user-tag') {
                        resultItem.innerHTML =
                            `
                        <img class="user-avatar" src="${config.avatarFolder || ''}${item[config.imageProperty]}">
                        <span>${item[config.displayProperty]} </span>
                        `;

                    } else {
                        resultItem.innerHTML =
                            `
                        <span>${item[config.displayProperty]} </span>
                        `;
                    }

                    resultItem.addEventListener('click', () => addTag(item))
                    results.appendChild(resultItem)
                }
            })
        }

        results.style.display = 'block'
    }

    function addTag(item) {
        //parse int or string for ID
        // had to convert to string because of the way the data is being passed
        // since IdentityUser has the id as a string in the database.
        const id = String(item.id)
        if (selectedIds.includes(id))
            return;

        selectedIds.push(id)
        updateSelectedIdsInput()

        const tag = document.createElement('div')
        tag.classList.add(config.tagClass || 'tag')

        if (config.tagClass === 'user-tag') {
            tag.innerHTML =
                `
            <img class="user-avatar" src="${config.avatarFolder || ''}${item[config.imageProperty]}">
            <span>${item[config.displayProperty]}</span>
            `;

        } else {
            tag.innerHTML =
                `
            <span>${item[config.displayProperty]} </span>
            `;

        }

        const removeBtn = document.createElement('span')
        removeBtn.textContent = 'x'
        removeBtn.classList.add('btn-remove')
        removeBtn.dataset.id = id
        removeBtn.addEventListener('click', (e) => {
            selectedIds = selectedIds.filter(i => i !== id)
            tag.remove()
            updateSelectedIdsInput()
            e.stopPropagation()
        })

        tag.appendChild(removeBtn)
        tagContainer.insertBefore(tag, input)

        input.value = ''
        results.innerHTML = ''
        results.style.display = 'none'
    }

    function updateSelectedIdsInput() {
        // since the ids are being passed as strings
        // they are joined with a comma to separate them.
        if (selectedInputIds) {
            selectedInputIds.value = selectedIds.join(',');
        }
    }
    function removeLastTag() {
        const tags = tagContainer.querySelectorAll(`.${config.tagClass}`)
        if (tags.length === 0)
            return

        const lastTag = tags[tags.length - 1]
        const lastId = lastTag.querySelector('.btn-remove').dataset.id

        selectedIds = selectedIds.filter(id => id !== lastId)
        lastTag.remove()
        updateSelectedIdsInput()
    }
    function resetTags() {
        tagContainer.querySelectorAll(`.${config.tagClass || "tag"}`).forEach(tag => tag.remove())

        if (selectedInputIds) selectedInputIds.value = ''
        selectedIds = []
        results.innerHTML = ''
        results.style.display = 'none'
        input.value = ''
    }
}
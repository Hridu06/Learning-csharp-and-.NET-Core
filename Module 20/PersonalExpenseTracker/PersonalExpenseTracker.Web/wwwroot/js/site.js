// Small progressive enhancements on top of Bootstrap. Every page still works without them.
(function () {
    'use strict';

    // Theme toggle: the initial theme is applied by an inline script in _Layout.cshtml.
    document.querySelectorAll('[data-theme-toggle]').forEach(function (button) {
        button.addEventListener('click', function () {
            var next = document.documentElement.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
            document.documentElement.setAttribute('data-bs-theme', next);
            try { localStorage.setItem('theme', next); } catch (e) { /* storage unavailable */ }
            document.dispatchEvent(new CustomEvent('themechange', { detail: next }));
        });
    });

    // Toasts rendered from TempData.
    document.querySelectorAll('.toast').forEach(function (el) {
        bootstrap.Toast.getOrCreateInstance(el).show();
    });

    // Tooltips for any element with data-bs-title (icon buttons etc).
    document.querySelectorAll('[data-bs-title]').forEach(function (el) {
        new bootstrap.Tooltip(el);
    });

    // Shared delete modal: copy the trigger's URL and labels into the form.
    var deleteModal = document.getElementById('deleteModal');
    if (deleteModal) {
        deleteModal.addEventListener('show.bs.modal', function (event) {
            var trigger = event.relatedTarget;
            if (!trigger) return;
            deleteModal.querySelector('[data-delete-form]').setAttribute('action', trigger.getAttribute('data-delete-url'));
            deleteModal.querySelector('[data-delete-return]').value = window.location.pathname + window.location.search;
            deleteModal.querySelector('[data-delete-name]').textContent = trigger.getAttribute('data-delete-name') || 'this item';
            deleteModal.querySelector('[data-delete-message]').textContent =
                trigger.getAttribute('data-delete-message') || 'This action cannot be undone.';
            var tooltip = bootstrap.Tooltip.getInstance(trigger);
            if (tooltip) tooltip.hide();
        });
    }

    // Loading state on submit prevents double submissions.
    document.addEventListener('submit', function (event) {
        var form = event.target;
        if (event.defaultPrevented || form.method.toLowerCase() !== 'post') return;
        if (window.jQuery && jQuery(form).data('validator') && !jQuery(form).valid()) return;
        form.querySelectorAll('button[type="submit"]').forEach(function (button) {
            button.dataset.originalHtml = button.innerHTML;
            button.disabled = true;
            button.innerHTML = '<span class="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>' +
                (button.dataset.loadingText || 'Please wait…');
        });
    });
    // Restore buttons if the page comes back from the back/forward cache.
    window.addEventListener('pageshow', function (event) {
        if (!event.persisted) return;
        document.querySelectorAll('button[data-original-html]').forEach(function (button) {
            button.innerHTML = button.dataset.originalHtml;
            button.disabled = false;
        });
    });

    // Receipt drop zone: preview and quick client-side checks (the server validates again).
    document.querySelectorAll('[data-dropzone]').forEach(function (zone) {
        var input = zone.querySelector('input[type="file"]');
        var prompt = zone.querySelector('.dropzone-prompt');
        var preview = zone.querySelector('.dropzone-preview');
        var error = zone.parentElement.querySelector('[data-dropzone-error]');
        var maxBytes = parseInt(zone.getAttribute('data-max-bytes'), 10);
        var objectUrl = null;

        function showError(message) {
            error.textContent = message;
            error.classList.toggle('d-none', !message);
        }

        function reset() {
            input.value = '';
            if (objectUrl) URL.revokeObjectURL(objectUrl);
            objectUrl = null;
            preview.classList.add('d-none');
            prompt.classList.remove('d-none');
            zone.classList.remove('has-file');
            input.style.pointerEvents = '';
        }

        input.addEventListener('change', function () {
            showError('');
            var file = input.files && input.files[0];
            if (!file) { reset(); return; }
            if (!/\.(jpg|png)$/i.test(file.name)) { reset(); showError('Only JPG and PNG images are allowed.'); return; }
            if (file.size > maxBytes) { reset(); showError('The receipt must be no larger than 2 MB.'); return; }

            if (objectUrl) URL.revokeObjectURL(objectUrl);
            objectUrl = URL.createObjectURL(file);
            preview.querySelector('img').src = objectUrl;
            preview.querySelector('[data-file-name]').textContent = file.name;
            preview.querySelector('[data-file-size]').textContent = (file.size / 1024).toFixed(0) + ' KB';
            prompt.classList.add('d-none');
            preview.classList.remove('d-none');
            zone.classList.add('has-file');
        });

        zone.querySelector('[data-dropzone-clear]').addEventListener('click', function () {
            reset();
            input.focus();
        });

        ['dragenter', 'dragover'].forEach(function (name) {
            zone.addEventListener(name, function () { zone.classList.add('is-dragover'); });
        });
        ['dragleave', 'drop'].forEach(function (name) {
            zone.addEventListener(name, function () { zone.classList.remove('is-dragover'); });
        });
    });

    // Live preview on the category form.
    var categoryPreview = document.querySelector('[data-category-preview]');
    if (categoryPreview) {
        var form = categoryPreview.closest('form');
        var avatar = categoryPreview.querySelector('.cat-avatar');
        var nameLabel = categoryPreview.querySelector('[data-preview-name]');
        form.addEventListener('input', function () {
            var name = form.querySelector('[name="Name"]').value.trim();
            var color = form.querySelector('[name="Color"]:checked');
            var icon = form.querySelector('[name="Icon"]:checked');
            nameLabel.textContent = name || 'New category';
            if (color) avatar.style.setProperty('--cat', color.value);
            if (icon) avatar.querySelector('i').className = 'bi bi-' + icon.value;
        });
    }

    document.querySelectorAll('[data-history-back]').forEach(function (button) {
        button.addEventListener('click', function () { history.back(); });
    });
})();

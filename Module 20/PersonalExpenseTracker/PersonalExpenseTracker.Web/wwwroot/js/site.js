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

    // Desktop sidebar: expanded (labels) or collapsed (icon rail). The initial class is set in _Layout.cshtml.
    var root = document.documentElement;
    var sidebarToggle = document.querySelector('[data-sidebar-toggle]');
    function syncSidebarToggle() {
        if (!sidebarToggle) return;
        var collapsed = root.classList.contains('sidebar-collapsed');
        sidebarToggle.setAttribute('aria-expanded', collapsed ? 'false' : 'true');
        sidebarToggle.setAttribute('aria-label', collapsed ? 'Expand sidebar' : 'Collapse sidebar');
    }
    if (sidebarToggle) {
        syncSidebarToggle();
        sidebarToggle.addEventListener('click', function () {
            var collapsed = root.classList.toggle('sidebar-collapsed');
            try { localStorage.setItem('sidebar', collapsed ? 'collapsed' : 'expanded'); } catch (e) { /* storage unavailable */ }
            syncSidebarToggle();
            document.querySelectorAll('.app-sidebar [data-bs-title]').forEach(function (el) {
                var tip = bootstrap.Tooltip.getInstance(el);
                if (tip) tip.hide();
            });
        });
    }
    // Sidebar tooltips only matter on the icon rail, where the labels are hidden.
    document.querySelectorAll('.app-sidebar [data-bs-title]').forEach(function (el) {
        el.addEventListener('show.bs.tooltip', function (event) {
            var rail = root.classList.contains('sidebar-collapsed') && window.matchMedia('(min-width: 992px)').matches;
            if (!rail) event.preventDefault();
        });
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

    // Show/hide password buttons next to password inputs.
    document.querySelectorAll('[data-password-toggle]').forEach(function (button) {
        var input = button.parentElement.querySelector('input');
        button.addEventListener('click', function () {
            var show = input.type === 'password';
            input.type = show ? 'text' : 'password';
            button.setAttribute('aria-label', show ? 'Hide password' : 'Show password');
            button.querySelector('i').className = show ? 'bi bi-eye-slash' : 'bi bi-eye';
        });
    });

    // Live password checklist (rules match the Identity options on the server).
    var passwordRules = {
        length: function (v) { return v.length >= 8; },
        upper: function (v) { return /[A-Z]/.test(v); },
        lower: function (v) { return /[a-z]/.test(v); },
        digit: function (v) { return /[0-9]/.test(v); }
    };
    document.querySelectorAll('[data-password-rules]').forEach(function (input) {
        var list = document.querySelector(input.getAttribute('data-password-rules'));
        if (!list) return;
        input.addEventListener('input', function () {
            list.querySelectorAll('[data-rule]').forEach(function (item) {
                var met = passwordRules[item.getAttribute('data-rule')](input.value);
                item.classList.toggle('is-met', met);
                item.querySelector('i').className = met ? 'bi bi-check-circle-fill' : 'bi bi-circle';
            });
        });
    });

    // Profile picture preview.
    document.querySelectorAll('[data-avatar-editor]').forEach(function (editor) {
        var input = editor.querySelector('input[type="file"]');
        var preview = editor.querySelector('[data-avatar-preview]');
        var current = preview.previousElementSibling;
        var hint = editor.querySelector('[data-avatar-hint]');
        var remove = editor.querySelector('input[type="checkbox"]');
        var maxBytes = parseInt(input.getAttribute('data-max-bytes'), 10);

        input.addEventListener('change', function () {
            var file = input.files && input.files[0];
            if (!file) return;
            if (!/\.(jpg|png)$/i.test(file.name) || file.size > maxBytes) {
                input.value = '';
                hint.textContent = 'Please choose a JPG or PNG image up to 2 MB.';
                hint.classList.add('text-danger');
                return;
            }
            preview.src = URL.createObjectURL(file);
            preview.classList.remove('d-none');
            current.classList.add('d-none');
            hint.textContent = file.name + ' will be saved when you click Save changes.';
            hint.classList.remove('text-danger');
            if (remove) remove.checked = false;
        });
        if (remove) {
            remove.addEventListener('change', function () {
                current.style.opacity = remove.checked ? '.35' : '';
                hint.textContent = remove.checked ? 'Your picture will be removed when you click Save changes.' : 'JPG or PNG, up to 2 MB.';
            });
        }
    });

    // Ask for the current password only when the email address is being changed.
    var emailInput = document.querySelector('[data-original-email]');
    var emailPassword = document.querySelector('[data-email-password]');
    if (emailInput && emailPassword) {
        emailInput.addEventListener('input', function () {
            var changed = emailInput.value.trim().toLowerCase() !== emailInput.getAttribute('data-original-email').toLowerCase();
            emailPassword.classList.toggle('d-none', !changed);
        });
    }

    // Inputs marked data-autosubmit (the dashboard month picker) reload the page as soon as they change.
    document.querySelectorAll('[data-autosubmit]').forEach(function (input) {
        input.addEventListener('change', function () {
            if (input.value) input.form.submit();
        });
        // Open the native picker on any click, not just on its small calendar icon.
        input.addEventListener('click', function () {
            try { if (input.showPicker) input.showPicker(); } catch (e) { /* not supported for this input */ }
        });
    });

    // "Pay full balance" on a loan copies the remaining amount into the payment field.
    document.querySelectorAll('[data-fill-amount]').forEach(function (button) {
        button.addEventListener('click', function () {
            var input = button.closest('form').querySelector('input[type="number"]');
            input.value = button.getAttribute('data-fill-amount');
            input.focus();
        });
    });

    // Re-open a modal after a failed post (e.g. wrong password on delete account).
    // Razor always renders data-* attributes, so check the value rather than its presence.
    document.querySelectorAll('.modal[data-show-on-load="true"]').forEach(function (modal) {
        bootstrap.Modal.getOrCreateInstance(modal).show();
    });
})();

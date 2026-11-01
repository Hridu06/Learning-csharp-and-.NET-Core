// Dashboard charts. Data is embedded by Views/Summary/Index.cshtml as JSON.
(function () {
    'use strict';

    var dataElement = document.getElementById('dashboard-data');
    if (!dataElement || !window.Chart) return;
    var data = JSON.parse(dataElement.textContent);
    var charts = [];

    function money(value) {
        return '৳ ' + Number(value).toLocaleString('en-GB', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    function compactMoney(value) {
        return '৳ ' + Number(value).toLocaleString('en-GB', { notation: 'compact', maximumFractionDigits: 1 });
    }

    function themeColors() {
        var styles = getComputedStyle(document.documentElement);
        return {
            text: styles.getPropertyValue('--app-muted').trim(),
            grid: styles.getPropertyValue('--app-border').trim(),
            surface: styles.getPropertyValue('--app-surface').trim(),
            brand: styles.getPropertyValue('--brand').trim(),
            brandRgb: styles.getPropertyValue('--brand-rgb').trim()
        };
    }

    function applyTheme() {
        var c = themeColors();
        Chart.defaults.color = c.text;
        Chart.defaults.borderColor = c.grid;
        charts.forEach(function (chart) {
            if (chart.config.type === 'bar') {
                var last = data.months.length - 1;
                // The current month is solid; earlier months are softer.
                chart.data.datasets[0].backgroundColor = data.months.map(function (_, i) {
                    return i === last ? c.brand : 'rgba(' + c.brandRgb + ', 0.35)';
                });
                chart.data.datasets[0].hoverBackgroundColor = c.brand;
                chart.options.scales.y.grid.color = c.grid;
                chart.options.scales.x.ticks.color = c.text;
                chart.options.scales.y.ticks.color = c.text;
            } else {
                chart.data.datasets[0].borderColor = c.surface;
            }
            chart.update('none');
        });
    }

    Chart.defaults.font.family = getComputedStyle(document.body).fontFamily;
    var tooltip = {
        padding: 10,
        cornerRadius: 8,
        displayColors: false,
        callbacks: { label: function (ctx) { return money(ctx.parsed.y !== undefined ? ctx.parsed.y : ctx.parsed); } }
    };

    var monthlyCanvas = document.getElementById('monthlyChart');
    if (monthlyCanvas) {
        charts.push(new Chart(monthlyCanvas, {
            type: 'bar',
            data: {
                labels: data.months.map(function (m) { return m.label; }),
                datasets: [{ label: 'Spending', data: data.months.map(function (m) { return m.total; }), borderRadius: 6, maxBarThickness: 36 }]
            },
            options: {
                maintainAspectRatio: false,
                plugins: { legend: { display: false }, tooltip: tooltip },
                scales: {
                    x: { grid: { display: false }, border: { display: false }, ticks: {} },
                    y: { beginAtZero: true, border: { display: false }, grid: {}, ticks: { callback: compactMoney, maxTicksLimit: 6 } }
                }
            }
        }));
    }

    var categoryCanvas = document.getElementById('categoryChart');
    if (categoryCanvas) {
        charts.push(new Chart(categoryCanvas, {
            type: 'doughnut',
            data: {
                labels: data.categories.map(function (c) { return c.name; }),
                datasets: [{
                    data: data.categories.map(function (c) { return c.total; }),
                    backgroundColor: data.categories.map(function (c) { return c.color; }),
                    borderWidth: 3,
                    hoverOffset: 6
                }]
            },
            options: {
                maintainAspectRatio: false,
                cutout: '68%',
                plugins: {
                    legend: { display: false },
                    tooltip: Object.assign({}, tooltip, {
                        displayColors: true,
                        callbacks: { label: function (ctx) { return ' ' + ctx.label + ': ' + money(ctx.parsed); } }
                    })
                }
            }
        }));
    }

    applyTheme();
    document.addEventListener('themechange', applyTheme);
})();

// Dashboard charts. Data is embedded by Views/Summary/Index.cshtml as JSON;
// colours come from the --chart-* and --app-* tokens in site.css so both themes stay in sync.
(function () {
    'use strict';

    var dataElement = document.getElementById('dashboard-data');
    if (!dataElement || !window.Chart) return;
    var data = JSON.parse(dataElement.textContent);
    // The trend always ends at the month being viewed.
    var selectedIndex = data.months.length - 1;
    var charts = [];

    function money(value) {
        return '৳ ' + Number(value).toLocaleString('en-GB', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }

    // ৳ 777, ৳ 8.8K: decimals only once the value is abbreviated.
    function compactMoney(value) {
        var n = Number(value);
        return '৳ ' + n.toLocaleString('en-GB', { notation: 'compact', maximumFractionDigits: n >= 1000 ? 1 : 0 });
    }

    function token(name) {
        return getComputedStyle(document.documentElement).getPropertyValue(name).trim();
    }

    function themeColors() {
        return {
            text: token('--app-text'),
            muted: token('--app-muted'),
            grid: token('--app-border'),
            surface: token('--app-surface'),
            primary: token('--chart-primary'),
            barMuted: token('--chart-muted'),
            accent: token('--chart-accent'),
            tooltip: token('--chart-tooltip')
        };
    }

    // Writes the all-time total in the middle of the doughnut.
    var centerTotal = {
        id: 'centerTotal',
        afterDraw: function (chart) {
            if (chart.config.type !== 'doughnut') return;
            var c = themeColors();
            var meta = chart.getDatasetMeta(0).data[0];
            if (!meta) return;
            var ctx = chart.ctx;
            var family = Chart.defaults.font.family;
            ctx.save();
            ctx.textAlign = 'center';
            ctx.textBaseline = 'middle';
            ctx.fillStyle = c.muted;
            ctx.font = '500 10px ' + family;
            ctx.fillText('TOTAL', meta.x, meta.y - 10);
            ctx.fillStyle = c.text;
            ctx.font = '700 15px ' + family;
            ctx.fillText(compactMoney(data.total), meta.x, meta.y + 7);
            ctx.restore();
        }
    };

    // Indigo at the base fading to violet at the top of the plot area.
    function barGradient(context) {
        var c = themeColors();
        var area = context.chart.chartArea;
        if (!area) return c.primary;
        var gradient = context.chart.ctx.createLinearGradient(0, area.bottom, 0, area.top);
        gradient.addColorStop(0, c.primary);
        gradient.addColorStop(1, c.accent);
        return gradient;
    }

    // Monthly chart extras: a label for the average line and, when only spending is shown,
    // the amount above each bar (with income bars beside them the labels would crowd).
    var monthlyDecorations = {
        id: 'monthlyDecorations',
        // A soft column behind the selected month (always the last one in the trend).
        beforeDatasetsDraw: function (chart) {
            var area = chart.chartArea;
            var x = chart.scales.x;
            var slot = (area.right - area.left) / data.months.length;
            var ctx = chart.ctx;
            ctx.save();
            ctx.fillStyle = token('--brand-soft');
            ctx.beginPath();
            ctx.roundRect(x.getPixelForValue(selectedIndex) - slot * 0.45, area.top - 14, slot * 0.9, area.bottom - area.top + 14, 10);
            ctx.fill();
            ctx.restore();
        },
        afterDatasetsDraw: function (chart) {
            var c = themeColors();
            var ctx = chart.ctx;
            var family = Chart.defaults.font.family;
            ctx.save();
            ctx.font = '600 11px ' + family;
            if (!data.hasIncome) {
                ctx.textAlign = 'center';
                ctx.textBaseline = 'bottom';
                ctx.fillStyle = c.text;
                chart.getDatasetMeta(expenseIndex).data.forEach(function (bar, i) {
                    var value = data.months[i].total;
                    if (value > 0) ctx.fillText(compactMoney(value), bar.x, bar.y - 6);
                });
            }
            if (data.average > 0) {
                var y = chart.scales.y.getPixelForValue(data.average);
                // Left end of the line: recent months (right side) are the ones most likely to have bars.
                ctx.textAlign = 'left';
                ctx.textBaseline = 'bottom';
                ctx.fillStyle = c.accent;
                ctx.fillText('Avg spend ' + compactMoney(data.average), chart.chartArea.left + 4, y - 5);
            }
            ctx.restore();
        }
    };

    function tooltipOptions(c) {
        return {
            backgroundColor: c.tooltip,
            titleColor: '#ffffff',
            bodyColor: '#d7dbef',
            footerColor: '#ffffff',
            padding: 12,
            cornerRadius: 10,
            titleFont: { weight: '600' },
            footerFont: { weight: '600' },
            displayColors: data.hasIncome,
            boxPadding: 4
        };
    }

    function applyTheme() {
        var c = themeColors();
        Chart.defaults.color = c.muted;
        Chart.defaults.borderColor = c.grid;
        charts.forEach(function (chart) {
            Object.assign(chart.options.plugins.tooltip, tooltipOptions(c));
            if (chart.config.type === 'bar') {
                // Bar fills are scriptable (barGradient, incomeColor), so they follow the theme on redraw.
                chart.data.datasets.forEach(function (dataset) {
                    if (dataset.type === 'line') dataset.borderColor = c.accent;
                });
                chart.options.scales.y.grid.color = c.grid;
                chart.options.scales.x.ticks.color = function (ctx) { return ctx.index === selectedIndex ? c.primary : c.muted; };
                chart.options.scales.y.ticks.color = c.muted;
            } else {
                chart.data.datasets[0].borderColor = c.surface;
                chart.options.plugins.tooltip.displayColors = true;
            }
            chart.update('none');
        });
    }

    function incomeColor() { return token('--chart-income'); }

    Chart.defaults.font.family = getComputedStyle(document.body).fontFamily;
    Chart.defaults.font.size = 12;

    // Income bars only appear once the user has recorded some income.
    var barStyle = { borderRadius: 6, borderSkipped: false, barPercentage: data.hasIncome ? 0.78 : 0.62, categoryPercentage: data.hasIncome ? 0.72 : 0.8, order: 2 };
    var monthlyDatasets = [];
    if (data.hasIncome) {
        monthlyDatasets.push(Object.assign({
            label: 'Income',
            data: data.months.map(function (m) { return m.income; }),
            backgroundColor: incomeColor,
            hoverBackgroundColor: incomeColor
        }, barStyle));
    }
    var expenseIndex = monthlyDatasets.length;
    monthlyDatasets.push(Object.assign({
        label: 'Expenses',
        data: data.months.map(function (m) { return m.total; }),
        backgroundColor: barGradient,
        hoverBackgroundColor: barGradient
    }, barStyle));
    monthlyDatasets.push({
        type: 'line',
        label: 'Average spending',
        data: data.months.map(function () { return data.average; }),
        borderWidth: 1.5,
        borderDash: [5, 4],
        pointRadius: 0,
        pointHoverRadius: 0,
        fill: false,
        order: 1
    });

    var monthlyCanvas = document.getElementById('monthlyChart');
    if (monthlyCanvas) {
        charts.push(new Chart(monthlyCanvas, {
            type: 'bar',
            data: { labels: data.months.map(function (m) { return m.label; }), datasets: monthlyDatasets },
            options: {
                maintainAspectRatio: false,
                interaction: { mode: 'index', intersect: false },
                // Clicking anywhere in a month's column opens that month on the dashboard.
                onClick: function (event, elements, chart) {
                    var index = chart.scales.x.getValueForPixel(event.x);
                    var month = data.months[index];
                    if (!month || index === selectedIndex) return;
                    window.location.search = month.key === data.currentKey ? '' : '?month=' + month.key;
                },
                onHover: function (event, elements, chart) {
                    chart.canvas.style.cursor = chart.scales.x.getValueForPixel(event.x) === selectedIndex ? 'default' : 'pointer';
                },
                // Room above the tallest bar for its amount label.
                layout: { padding: { top: 22 } },
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        filter: function (item) { return item.dataset.type !== 'line'; },
                        callbacks: {
                            title: function (items) { return data.months[items[0].dataIndex].full; },
                            label: function (ctx) { return ' ' + ctx.dataset.label + ': ' + money(ctx.parsed.y); },
                            footer: function (items) {
                                var month = data.months[items[0].dataIndex];
                                if (data.hasIncome) {
                                    var net = month.income - month.total;
                                    return (net >= 0 ? 'Saved ' : 'Overspent ') + money(Math.abs(net));
                                }
                                if (!data.average) return '';
                                var diff = (month.total - data.average) / data.average * 100;
                                return (diff >= 0 ? '+' : '') + diff.toFixed(0) + '% vs average';
                            }
                        }
                    }
                },
                scales: {
                    x: {
                        grid: { display: false },
                        border: { display: false },
                        ticks: { font: function (ctx) { return { weight: ctx.index === selectedIndex ? '700' : '500' }; } }
                    },
                    y: {
                        beginAtZero: true,
                        border: { display: false, dash: [3, 4] },
                        grid: { drawTicks: false },
                        ticks: { callback: compactMoney, maxTicksLimit: 5, padding: 8 }
                    }
                }
            },
            plugins: [monthlyDecorations]
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
                    borderRadius: 4,
                    hoverOffset: 4
                }]
            },
            options: {
                maintainAspectRatio: false,
                cutout: '74%',
                plugins: {
                    legend: { display: false },
                    tooltip: {
                        callbacks: {
                            label: function (ctx) {
                                var share = ctx.parsed / data.total * 100;
                                return ' ' + ctx.label + ': ' + money(ctx.parsed) + ' (' + share.toFixed(1) + '%)';
                            }
                        }
                    }
                }
            },
            plugins: [centerTotal]
        }));
    }

    applyTheme();
    document.addEventListener('themechange', applyTheme);
})();

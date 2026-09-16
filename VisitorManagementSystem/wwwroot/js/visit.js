$(document).ready(function () {
    console.log('Visit module loaded');
});

// Function to load visit data via AJAX
function loadVisits() {
    $.ajax({
        url: '/Visit/GetTodayVisits',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log('Today\'s visits:', data);
        },
        error: function (xhr, status, error) {
            console.error('Error loading visits:', error);
        }
    });
}

// Function to check out a visitor
function checkOutVisitor(visitId) {
    if (confirm('Are you sure you want to check out this visitor?')) {
        window.location.href = '/Visit/CheckOut/' + visitId;
    }
}

// Auto-refresh dashboard stats every 30 seconds
function refreshDashboardStats() {
    $.ajax({
        url: '/Dashboard/GetStats',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data.totalVisitorsToday !== undefined) {
                $('#totalVisitorsToday').text(data.totalVisitorsToday);
            }
            if (data.visitorsInside !== undefined) {
                $('#visitorsInside').text(data.visitorsInside);
            }
            if (data.checkedOutVisitors !== undefined) {
                $('#checkedOutVisitors').text(data.checkedOutVisitors);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error refreshing stats:', error);
        }
    });
}

// Uncomment to enable auto-refresh
// setInterval(refreshDashboardStats, 30000);

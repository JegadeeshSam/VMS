$(document).ready(function () {
    loadVisitors();
});

function loadVisitors() {
    $.ajax({
        url: '/Visitor/GetVisitors',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log('Visitors loaded:', data);
        },
        error: function (xhr, status, error) {
            console.error('Error loading visitors:', error);
        }
    });
}

function clearForm() {
    $('#VisitorId').val('');
    $('#VisitorName').val('');
    $('#CompanyName').val('');
    $('#MobileNo').val('');
    $('#Email').val('');
    $('#IDProof').val('');
    $('#modalTitle').text('Add Visitor');
}

function editVisitor(id) {
    $.ajax({
        url: '/Visitor/GetVisitors',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var visitor = data.find(v => v.visitorId === id);
            if (visitor) {
                $('#VisitorId').val(visitor.visitorId);
                $('#VisitorName').val(visitor.visitorName);
                $('#CompanyName').val(visitor.companyName);
                $('#MobileNo').val(visitor.mobileNo);
                $('#Email').val(visitor.email);
                $('#IDProof').val(visitor.idProof);
                $('#modalTitle').text('Edit Visitor');
                $('#visitorModal').modal('show');
            }
        },
        error: function (xhr, status, error) {
            alert('Error loading visitor details: ' + error);
        }
    });
}

function saveVisitor() {
    var visitorId = $('#VisitorId').val();
    var formData = {
        VisitorId: visitorId ? parseInt(visitorId) : 0,
        VisitorName: $('#VisitorName').val(),
        CompanyName: $('#CompanyName').val(),
        MobileNo: $('#MobileNo').val(),
        Email: $('#Email').val(),
        IDProof: $('#IDProof').val()
    };

    // Validate required fields
    if (!formData.VisitorName || !formData.CompanyName || !formData.MobileNo) {
        alert('Please fill all required fields.');
        return;
    }

    var url = visitorId ? '/Visitor/Edit' : '/Visitor/Create';

    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').length > 0 
                ? $('input[name="__RequestVerificationToken"]').val() 
                : ''
        },
        success: function (response) {
            location.reload();
        },
        error: function (xhr, status, error) {
            alert('Error saving visitor: ' + error);
        }
    });
}

function deleteVisitor(id, name) {
    if (confirm('Are you sure you want to delete visitor "' + name + '"?')) {
        $.ajax({
            url: '/Visitor/Delete/' + id,
            type: 'POST',
            headers: {
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').length > 0 
                    ? $('input[name="__RequestVerificationToken"]').val() 
                    : ''
            },
            success: function (response) {
                location.reload();
            },
            error: function (xhr, status, error) {
                alert('Error deleting visitor: ' + error);
            }
        });
    }
}

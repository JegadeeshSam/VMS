$(document).ready(function () {
    // Load employees on page load
    loadEmployees();
});

function loadEmployees() {
    $.ajax({
        url: '/Employee/GetEmployees',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log('Employees loaded:', data);
        },
        error: function (xhr, status, error) {
            console.error('Error loading employees:', error);
        }
    });
}

function clearForm() {
    $('#EmployeeId').val('');
    $('#EmployeeName').val('');
    $('#Department').val('');
    $('#Designation').val('');
    $('#MobileNo').val('');
    $('#Email').val('');
    $('#IsActive').prop('checked', true);
    $('#modalTitle').text('Add Employee');
}

function editEmployee(id) {
    $.ajax({
        url: '/Employee/GetEmployees',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var employee = data.find(e => e.employeeId === id);
            if (employee) {
                $('#EmployeeId').val(employee.employeeId);
                $('#EmployeeName').val(employee.employeeName);
                $('#Department').val(employee.department);
                $('#Designation').val(employee.designation);
                $('#MobileNo').val(employee.mobileNo);
                $('#Email').val(employee.email);
                $('#IsActive').prop('checked', employee.isActive);
                $('#modalTitle').text('Edit Employee');
                $('#employeeModal').modal('show');
            }
        },
        error: function (xhr, status, error) {
            alert('Error loading employee details: ' + error);
        }
    });
}

function saveEmployee() {
    var employeeId = $('#EmployeeId').val();
    var formData = {
        EmployeeId: employeeId ? parseInt(employeeId) : 0,
        EmployeeName: $('#EmployeeName').val(),
        Department: $('#Department').val(),
        Designation: $('#Designation').val(),
        MobileNo: $('#MobileNo').val(),
        Email: $('#Email').val(),
        IsActive: $('#IsActive').is(':checked')
    };

    // Validate required fields
    if (!formData.EmployeeName || !formData.Department || !formData.Designation || !formData.MobileNo || !formData.Email) {
        alert('Please fill all required fields.');
        return;
    }

    var url = employeeId ? '/Employee/Edit' : '/Employee/Create';
    var method = employeeId ? 'POST' : 'POST';

    $.ajax({
        url: url,
        type: method,
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
            alert('Error saving employee: ' + error);
        }
    });
}

function deleteEmployee(id, name) {
    if (confirm('Are you sure you want to delete employee "' + name + '"?')) {
        $.ajax({
            url: '/Employee/Delete/' + id,
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
                alert('Error deleting employee: ' + error);
            }
        });
    }
}

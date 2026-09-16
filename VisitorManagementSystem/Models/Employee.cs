namespace VisitorManagementSystem.Models;

public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation Property
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}

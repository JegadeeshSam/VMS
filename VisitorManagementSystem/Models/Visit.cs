namespace VisitorManagementSystem.Models;

public class Visit
{
    public int VisitId { get; set; }
    public int VisitorId { get; set; }
    public int EmployeeId { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public DateTime VisitDate { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public string Status { get; set; } = "CheckedIn"; // CheckedIn, CheckedOut
    
    // Navigation Properties
    public Visitor? Visitor { get; set; }
    public Employee? Employee { get; set; }
}

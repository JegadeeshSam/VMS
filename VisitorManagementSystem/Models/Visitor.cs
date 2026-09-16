namespace VisitorManagementSystem.Models;

public class Visitor
{
    public int VisitorId { get; set; }
    public string VisitorName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string IDProof { get; set; } = string.Empty;
    
    // Navigation Property
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}

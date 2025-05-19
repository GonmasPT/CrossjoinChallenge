namespace CrossjoinChallenge.Models;

public class Lead(Company company, string businessType)
{
    public Guid LeadId { get; } = Guid.NewGuid();
    public Company Company { get; } = company;
    public string Country => Company.Country; // Expression-bodied property (? = null-conditional)
    // Equivalent to: get { return Company.country; }
    public string BusinessType { get; set; } = businessType; // e.g., Industry, Retail
    public string Status { get; set; } = "Draft"; // e.g., Draft, Active
}
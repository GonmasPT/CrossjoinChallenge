namespace CrossjoinChallenge.Models;

// Primary Constructor
public class Company(string nif, string address, string country, string stakeholder, string contact)
{
    public Guid Id { get; } = Guid.NewGuid(); // guid auto-generates ids
    public string Nif { get; set; } = nif; // Required depending on the country
    public string Address { get; set; } = address;
    public string Country { get; set; } = country;
    public string Status { get; set; } = "Draft"; // e.g., Draft, Active
    public string Stakeholder { get; set; } = stakeholder;
    public string Contact { get; set; } = contact;
}
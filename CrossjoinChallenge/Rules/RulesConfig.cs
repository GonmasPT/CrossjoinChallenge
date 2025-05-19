using CrossjoinChallenge.Models;

namespace CrossjoinChallenge.Rules;

public static class RulesConfig
{
    public static void RegisterRules()
    {
        var rules = RuleManager.Instance;

        // If the company is in Portugal, NIF is required
        rules.SetRequired<Company>(
            c => c.Country == "Portugal",
            nameof(Company.Nif),
            true
        );

        // If the business type is B2B, Stakeholder is required
        rules.SetRequired<Company>(
            c => c.Status == "Draft", // assuming this means it's still editable
            nameof(Company.Stakeholder),
            true
        );

        // If the lead's business type is Retail, then contact must be filled
        rules.SetRequired<Lead>(
            l => l.BusinessType == "Retail",
            nameof(Lead.Company.Contact),
            true
        );
    }
}
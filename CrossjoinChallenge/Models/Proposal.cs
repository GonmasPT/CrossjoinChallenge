namespace CrossjoinChallenge.Models;

using CrossjoinChallenge.Rules; // to access RuleManager

public class Proposal
{
    public Guid ProposalId { get; } = Guid.NewGuid();
    public Lead Lead { get; }
    public List<Product> Products { get; } = [];
    public float ProductionCost { get; set; }
    public int MonthlyProducedProducts { get; set; }
    public float ExpectedMonthlyProfit { get; set; }
    public string Status { get; set; } = "Draft";

    // Inherit relevant fields from Lead
    public Company Company => Lead.Company;
    public string Country => Lead.Country;
    public string BusinessType => Lead.BusinessType;

    public Proposal(Lead lead, float productionCost, int monthlyProducedProducts, float expectedMonthlyProfit)
    {
        Lead = lead;
        ProductionCost = productionCost;
        MonthlyProducedProducts = monthlyProducedProducts;
        ExpectedMonthlyProfit = expectedMonthlyProfit;

        // ✅ Enforce required field rules
        RuleManager.Instance.Validate(lead);
        RuleManager.Instance.Validate(lead.Company);
    }

    public void AddProduct(Product product)
    {
        var dependentProduct = product.DependentProduct;

        if (dependentProduct != null)
        {
            if (!Products.Contains(dependentProduct))
                throw new Exception("Dependent product not found! Please add it first.");
            if (product.ProductType != dependentProduct.ProductType)
                throw new Exception("Product type mismatch! Please check the dependent product type.");
        }

        Products.Add(product);
    }

    public void FinalizeProposal()
    {
        Lead.Company.Status = "Active";
        Lead.Status = "Active";
        Status = "Active";
    }
}
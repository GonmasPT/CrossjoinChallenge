using CrossjoinChallenge.Models;
using CrossjoinChallenge.Rules;

namespace CrossjoinChallenge;

class Program
{
    public static void Main(string[] args)
    {
        RulesConfig.RegisterRules();
        
        var company1 = new Company("123456789", "TestAddress", "Spain", "John", "919191919");
        var lead1 = new Lead(company1, "Industry");
        var proposal = new Proposal(lead1, 1000, 10, 100);
        
        Console.WriteLine(proposal.Lead.Company.Stakeholder);
        Console.WriteLine(proposal.Lead.Company.Status);

        var product1 = new Product(null, "Electronics");
        proposal.AddProduct(product1);
        
        var product2 = new Product(null, "Furniture");
        proposal.AddProduct(product2);
        
        // Product dependency type mismatch
        //var product3 = new Product(productType:"Furniture", dependentProduct:product1);
        //proposal.AddProduct(product3);
        
        // Product dependency isn't found
        //var product4 = new Product(productType:"Furniture");
        //var product5 = new Product(productType:"Furniture", dependentProduct:product4);
        //proposal.AddProduct(product5);
        
        proposal.FinalizeProposal();
        Console.WriteLine(proposal.Lead.Company.Status);
    }
}
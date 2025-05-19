using System;
using CrossjoinChallenge.Models;
//using JetBrains.Annotations;
using Xunit;

namespace CrossjoinChallenge.Tests;

//[TestSubject(typeof(Proposal))]
public class EntityEvolutionTests
{
    [Fact]
    public void ProposalInitialization_ShouldMatchLeadAndInputValues()
    {
        var company = new Company("12345", "Lisbon", "Portugal", "John Doe", "contact@example.com");
        var lead = new Lead(company, "Retail");
        var productionCost = 5000.00f;
        var monthlyProducedProducts = 100;
        var expectedMonthlyProfit = 2000.00f;

        var proposal = new Proposal(lead, productionCost, monthlyProducedProducts, expectedMonthlyProfit);

        // Verify constructor parameters
        Assert.Equal(productionCost, proposal.ProductionCost);
        Assert.Equal(monthlyProducedProducts, proposal.MonthlyProducedProducts);
        Assert.Equal(expectedMonthlyProfit, proposal.ExpectedMonthlyProfit);
        Assert.Equal("Draft", proposal.Status);
        Assert.Empty(proposal.Products);

        // Verify data transfer from Lead
        Assert.Equal(lead.Company, proposal.Company);
        Assert.Equal(lead.Country, proposal.Country);
        Assert.Equal(lead.BusinessType, proposal.BusinessType);
    }
    
    [Fact]
    public void AddProduct_SucceedsIfDependentNullOrExists()
    {
        var company = new Company("12345", "Lisbon", "Portugal", "John Doe", "contact@example.com");
        var lead = new Lead(company, "Retail");
        var proposal = new Proposal(lead, 5000f, 100, 2000f);

        // Create a base product
        var baseProduct = new Product(null, "Electronics");

        // Add base product first — should succeed
        proposal.AddProduct(baseProduct);
        Assert.Single(proposal.Products);
        Assert.Equal(baseProduct, proposal.Products[0]);

        // Create a dependent product with the same type
        var dependentProduct = new Product(baseProduct, "Electronics");

        // Add dependent product — should succeed
        proposal.AddProduct(dependentProduct);
        Assert.Equal(2, proposal.Products.Count);
        Assert.Contains(dependentProduct, proposal.Products);
    }
    
    [Fact]
    public void AddProduct_ThrowsIfDependentMissing()
    {
        var company = new Company("12345", "Lisbon", "Portugal", "John Doe", "contact@example.com");
        var lead = new Lead(company, "Retail");
        var proposal = new Proposal(lead, 5000f, 100, 2000f);

        var baseProduct = new Product(null, "Electronics");
        var dependentProduct = new Product(baseProduct, "Electronics");

        // Try adding dependent first — should throw
        var ex = Assert.Throws<Exception>(() => proposal.AddProduct(dependentProduct));
        Assert.Equal("Dependent product not found! Please add it first.", ex.Message);
    }
    
    [Fact]
    public void AddProduct_ThrowsIfTypeMismatch()
    {
        var company = new Company("12345", "Lisbon", "Portugal", "John Doe", "contact@example.com");
        var lead = new Lead(company, "Retail");
        var proposal = new Proposal(lead, 5000f, 100, 2000f);

        var baseProduct = new Product(null, "Electronics");
        proposal.AddProduct(baseProduct);

        var dependentProduct = new Product(baseProduct, "Furniture");

        // Add with different type — should throw
        var ex = Assert.Throws<Exception>(() => proposal.AddProduct(dependentProduct));
        Assert.Equal("Product type mismatch! Please check the dependent product type.", ex.Message);
    }
}
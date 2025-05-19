using System;
using CrossjoinChallenge.Models;
using CrossjoinChallenge.Rules;
using Xunit;

namespace CrossjoinChallenge.Tests;

public class FieldValidationTests
{
    public FieldValidationTests()
    {
        // Ensure rules are registered before each test run
        RulesConfig.RegisterRules();
    }

    [Fact]
    public void Should_Throw_When_Nif_IsMissing_For_PortugueseCompany()
    {
        var company = new Company("", "Lisbon", "Portugal", "Stakeholder", "contact@example.com");
        var lead = new Lead(company, "Retail");
        
        var ex = Assert.Throws<Exception>(() => new Proposal(lead, 5000f, 100, 2000f));

        Assert.Equal("Nif is required for Company.", ex.Message);
    }

    [Fact]
    public void Should_Throw_When_Stakeholder_IsMissing_While_StatusIsDraft()
    {
        var company = new Company("123456789", "Lisbon", "Spain", "", "contact@example.com"); // Status is Draft by default
        var lead = new Lead(company, "Retail");
        
        var ex = Assert.Throws<Exception>(() => new Proposal(lead, 5000f, 100, 2000f));

        Assert.Equal("Stakeholder is required for Company.", ex.Message);
    }
    
    /*[Fact]
    public void Should_Throw_When_Contact_IsMissing_For_RetailLead()
    {
        var company = new Company("123456789", "Lisbon", "Portugal", "Stakeholder", ""); // Missing contact
        var lead = new Lead(company, "Retail");

        var ex = Assert.Throws<Exception>(() => RuleManager.Instance.Validate(lead));

        Assert.Equal("Contact is required for Lead.", ex.Message);
    } */
}
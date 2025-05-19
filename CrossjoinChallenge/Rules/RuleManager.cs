namespace CrossjoinChallenge.Rules;

public class RuleManager
{
    private class Rule
    {
        public required string ClassName { get; init; }
        public required Func<object, bool> Condition { get; init; } // Condition delegate
        public required string FieldName { get; init; }
        public bool IsRequired { get; init; } = true;
    }
    
    private Dictionary<string, List<Rule>> Rules { get; set; } = []; // new Dictionary<string, List<Rule>>()
    
    // ✅ Singleton instance
    private static readonly RuleManager _instance = new();
    public static RuleManager Instance => _instance;

    private RuleManager() { } // ✅ Private constructor to prevent external instantiation
    
    
    public void SetRequired<T>(Func<T, bool> condition, string fieldName, bool required)
    {
        var rule = new Rule {
            ClassName = typeof(T).Name,
            FieldName = fieldName,
            Condition = obj => condition((T)obj), // explicar esta parte da condicao, generico e como chamar este metodo
            IsRequired = required
        };
        
        if (!Rules.ContainsKey(rule.ClassName)) Rules.Add(rule.ClassName, []); // [] equivalent to new List<Rule>()
        Rules[rule.ClassName].Add(rule);
    }

    
    public bool IsFieldRequired(object instance, string fieldName)
    {
        var className = instance.GetType().Name;

        if (Rules.ContainsKey(className))
        {
            foreach (var rule in Rules[className])
            {
                if (rule.FieldName == fieldName && rule.Condition(instance)) return rule.IsRequired;
            }
        }
        return false;
    }
    
    
    // ✅ Validation method to enforce field requirements
    public void Validate(object instance)
    {
        var type = instance.GetType();
        var className = type.Name;
        var fields = type.GetProperties();

        foreach (var property in fields)
        {
            if (IsFieldRequired(instance, property.Name))
            {
                var value = property.GetValue(instance);
                if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                {
                    throw new Exception($"{property.Name} is required for {className}.");
                }
            }
        }
    }
}
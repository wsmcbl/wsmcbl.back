using Xunit.Abstractions;
using Xunit.Sdk;

namespace wsmcbl.tests.integration.util;

public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;
        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
        foreach (TTestCase testCase in testCases)
        {
            int priority = testCase.TestMethod.Method
                .GetCustomAttributes(assemblyName)
                .FirstOrDefault()
                ?.GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? 0;

            GetOrCreate(sortedMethods, priority).Add(testCase);
        }

        var aux = sortedMethods.Keys.SelectMany(priority =>
            sortedMethods[priority].OrderBy(testCase => testCase.TestMethod.Method.Name));
        foreach (var testCase in aux)
        {
            yield return testCase;
        }
    }

    private static TValue GetOrCreate<TValue>(IDictionary<int, TValue> dictionary, int key) where TValue : new()
        => dictionary.TryGetValue(key, out var result) ? result : dictionary[key] = new TValue();
}
using Xunit.Abstractions;

namespace wsmcbl.tests.integration.util;

public class CustomCollectionOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        return testCollections.OrderBy(c => c);
    }
}
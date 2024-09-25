namespace wsmcbl.tests.integration.util;

[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute : Attribute
{
    public int Priority { get; private set; }
    
    public TestPriorityAttribute(int priority) => Priority = priority;
}
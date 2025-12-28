using AutoFixture.Xunit2;
using Xunit.Sdk;

namespace ABCGame;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
        : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
    }
}

public class InlineAutoNSubstituteDataAttribute : CompositeDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] values)
        : base(new DataAttribute[] {
            new InlineDataAttribute(values), new AutoNSubstituteDataAttribute() })
    {
    }
}
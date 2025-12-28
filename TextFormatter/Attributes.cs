using AutoFixture.AutoNSubstitute;

namespace TextFormatter;

public class AutoNSubstituteDataAttribute()
    : AutoDataAttribute(
        () => new Fixture().Customize(new AutoNSubstituteCustomization())
    );

public class InlineAutoNSubstituteDataAttribute : CompositeDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] values) : base(
        new InlineDataAttribute(values),
        new AutoNSubstituteDataAttribute()
    ) { }
}
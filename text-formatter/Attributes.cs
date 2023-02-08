using AutoFixture.AutoNSubstitute;

namespace text_formatter;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute() : base(
        () => new Fixture().Customize(new AutoNSubstituteCustomization())
    ) { }
}

public class InlineAutoNSubstituteDataAttribute : CompositeDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] values) : base(
        new InlineDataAttribute(values),
        new AutoNSubstituteDataAttribute()
    ) { }
}
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Xunit;
using Xunit.Sdk;

namespace Katas.Testing;

public class AutoNSubstituteDataAttribute()
    : AutoDataAttribute(
        () => new Fixture().Customize(new AutoNSubstituteCustomization())
    );

public class InlineAutoNSubstituteDataAttribute(params object[] values) : CompositeDataAttribute(
    new InlineDataAttribute(values), 
    new AutoNSubstituteDataAttribute()
);

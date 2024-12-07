using FsCheck;

namespace Preparation.Tests;

public static class RecommendedAgeArbitraries
{
    // Generator for invalid strings
    public static Arbitrary<string> InvalidStrings() =>
        Arb.Generate<string>()
           .Where(s => !int.TryParse(s, out _)) 
           .ToArbitrary();
}
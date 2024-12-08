using FluentAssertions;
using Xunit;

namespace EID.Tests
{
    public class EIDTests
    {
        [Fact]
        public void Null_Is_Not_A_Valid_ID() => EilfId.Validate(null).Should().BeFalse();
    }

    internal class EilfId
    {
        internal static bool Validate(string? value)
        {
            if (value is null)
            {
                return false;
            }
            return true;
        }
    }
}

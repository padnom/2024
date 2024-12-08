using FluentAssertions;
using Xunit;

namespace EID.Tests
{
    public class EIDTests
    {
        [Fact]
        public void Null_Is_Not_A_Valid_ID() => EilfId.Validate(null).Should().BeFalse();

        [Fact]
        public void Empty_String_Is_Not_A_Valid_ID() =>
            EilfId.Validate(string.Empty).Should().BeFalse();

        [Fact]
        public void White_Space_Is_Not_A_Valid_ID() => EilfId.Validate(" ").Should().BeFalse();

        [Fact]
        public void Too_Short_String_Is_Not_A_Valid_ID() => EilfId.Validate("1").Should().BeFalse();

        [Fact]
        public void Too_Long_String_Is_A_Valid_ID()
        {
            EilfId
                .Validate(
                    "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                )
                .Should()
                .BeFalse();
        }

        internal class EilfId
        {
            internal static bool Validate(string? value)
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    return false;
                }
                if (value.Length < 2 || value.Length > 8)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

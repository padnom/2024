using System.Text.RegularExpressions;
using FluentAssertions;
using Xunit;

namespace EID.Tests
{
    public class EIDTests
    {
        [Fact]
        public void Null_Is_Not_A_Valid_ID() => ElfId.Validate(null).Should().BeFalse();

        [Fact]
        public void Empty_String_Is_Not_A_Valid_ID() =>
            ElfId.Validate(string.Empty).Should().BeFalse();

        [Fact]
        public void White_Space_Is_Not_A_Valid_ID() => ElfId.Validate(" ").Should().BeFalse();

        [Fact]
        public void Too_Short_String_Is_Not_A_Valid_ID() => ElfId.Validate("1").Should().BeFalse();

        [Fact]
        public void Too_Long_String_Is_A_Valid_ID()
        {
            ElfId
                .Validate(
                    "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                )
                .Should()
                .BeFalse();
        }

        [Fact]
        public void Not_Exactly_8_Characters_Is_Not_A_Valid_ID() =>
            ElfId.Validate("1234567").Should().BeFalse();

        [Fact]
        public void ValidString_Is_A_Valid_ID() => ElfId.Validate("19800767").Should().BeTrue();

        internal class ElfId
        {
            internal static bool Validate(string? value)
            {
                if (
                    string.IsNullOrWhiteSpace(value)
                    || !MatchesPattern(value)
                    || !IsValidControlKey(value)
                )
                {
                    return false;
                }

                return true;
            }

            private static bool MatchesPattern(string value)
            {
                string pattern = @"^[1-3][0-9]{2}[0-9]{3}(0[1-9]|[1-8][0-9]|9[0-7])$";
                return Regex.IsMatch(value, pattern);
            }

            private static bool IsValidControlKey(string value)
            {
                int firstSixDigits = int.Parse(value.Substring(0, 6));
                int controlKey = int.Parse(value.Substring(6, 2));
                return firstSixDigits % 97 == 97 - controlKey;
            }
        }
    }
}

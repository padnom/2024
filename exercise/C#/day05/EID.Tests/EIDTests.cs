using FluentAssertions.LanguageExt;

using LanguageExt;
using LanguageExt.Common;

using System.Text.RegularExpressions;

using Xunit;

using static LanguageExt.Prelude;

namespace EID.Tests;

public class EIDTests
{
    [Fact]
    public void Empty_String_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(string.Empty).Should().BeFail("Value cannot be null or whitespace");
    }

    [Fact]
    public void Not_Exactly_8_Characters_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("1234567").Should().BeFail("Value does not match pattern");
    }

    [Fact]
    public void Null_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(null).Should().BeFail("Value cannot be null or whitespace");
    }

    [Fact]
    public void Too_Long_String_Is_A_Valid_ID()
    {
        ElfId
            .Validate(
                "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
            )
            .Should()
            .BeFail("Value does not match pattern");
    }

    [Fact]
    public void Too_Short_String_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("1").Should().BeFail("Value does not match pattern");
    }

    [Fact]
    public void ValidString_Is_A_Valid_ID()
    {
        ElfId.Validate("19800767").Should().BeSuccess();
    }

    [Fact]
    public void White_Space_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(" ").Should().BeFail("Value cannot be null or whitespace");
    }

    internal class ElfId
    {
        internal static Validation<Error, string> Validate(string? value)
        {
            return ValidateNotNullOrWhitespace(value).Bind(MatchesPattern).Bind(IsValidControlKey);
        }

        private static Validation<Error, string> IsValidControlKey(string value)
        {
            var firstSixDigits = int.Parse(value.Substring(0, 6));
            var controlKey = int.Parse(value.Substring(6, 2));

            return firstSixDigits % 97 == 97 - controlKey
                ? Success<Error, string>(value)
                : Fail<Error, string>(Error.New("Invalid control key"));
        }

        private static Validation<Error, string> MatchesPattern(string value)
        {
            var pattern = @"^[1-3][0-9]{2}[0-9]{3}(0[1-9]|[1-8][0-9]|9[0-7])$";

            return Regex.IsMatch(value, pattern)
                ? Success<Error, string>(value)
                : Fail<Error, string>(Error.New("Invalid pattern"));
        }

        private static Validation<Error, string> ValidateNotNullOrWhitespace(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? Fail<Error, string>(Error.New("Value cannot be null or whitespace"))
                : Success<Error, string>(value);
        }
    }
}

using FluentAssertions;
using FluentAssertions.LanguageExt;

using Xunit;

namespace EID.Tests;

public class EIDTests
{
    [Fact]
    public void Empty_String_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(string.Empty)
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.ValueCannotBeNullOrWhitespace,
                             ElfId.InvalidPattern,
                             ElfId.InvalidControlKey);
    }

    [Fact]
    public void InvalidControlKey_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("19800768")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle(ElfId.InvalidControlKey);
    }

    [Fact]
    public void Not_Exactly_8_Characters_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("1234567")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.InvalidPattern,
                             ElfId.InvalidControlKey);
    }

    [Fact]
    public void Null_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(null)
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.ValueCannotBeNullOrWhitespace,
                             ElfId.InvalidPattern,
                             ElfId.InvalidControlKey);
    }

    [Fact]
    public void Too_Long_String_Is_A_Valid_ID()
    {
        ElfId.Validate("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.InvalidPattern,
                             ElfId.InvalidControlKey);
    }

    [Fact]
    public void Too_Short_String_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("1")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle(error => error.Message == ElfId.InvalidPattern);
    }

    [Fact]
    public void ValidString_Is_A_Valid_ID()
    {
        ElfId.Validate("19800767")
             .Should()
             .BeSuccess();
    }

    [Fact]
    public void White_Space_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(" ")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.ValueCannotBeNullOrWhitespace,
                             ElfId.InvalidPattern,
                             ElfId.InvalidControlKey);
    }
}
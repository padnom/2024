using FluentAssertions;
using FluentAssertions.LanguageExt;

using Xunit;

namespace EID.Tests;

public class EIDTests
{
    [Fact]
    public void ElfId_Validation_Returns_All_Possible_Error_Types_For_Pattern()
    {
        ElfId.Validate("49800088")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.InvalidSex, ElfId.InvalidSerialNumber, ElfId.InvalidControlKey);
    }

    [Fact]
    public void Empty_String_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(string.Empty)
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle()
             .And.ContainSingle(error => error.Message == ElfId.ValueCannotBeNullOrWhitespaceOrEmpty);
    }

    [Fact]
    public void InvalidControlKey_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("19800768")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle()
             .And.ContainSingle(error => error.Message == ElfId.InvalidControlKey);
    }

    [Fact]
    public void Not_Exactly_8_Characters_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("1234567")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainInOrder(ElfId.InvalidLength);
    }

    [Fact]
    public void Null_Is_Not_A_Valid_ID()
    {
        ElfId.Validate(null)
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle()
             .And.ContainSingle(error => error.Message == ElfId.ValueCannotBeNullOrWhitespaceOrEmpty);

        ;
    }

    [Fact]
    public void Too_Long_String_Is_A_Valid_ID()
    {
        ElfId.Validate("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle()
             .And.ContainSingle(error => error.Message == ElfId.InvalidLength);
    }

    [Fact]
    public void Too_Short_String_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("1")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle(error => error.Message == ElfId.InvalidLength);
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
             .ContainSingle()
             .And.ContainSingle(error => error.Message == ElfId.ValueCannotBeNullOrWhitespaceOrEmpty);

        ;
    }

    [Fact]
    public void WrongSerialNumber_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("19800074")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle()
             .And.ContainSingle(error => error.Message == ElfId.InvalidSerialNumber);
    }

    [Fact]
    public void WrongSex_Is_Not_A_Valid_ID()
    {
        ElfId.Validate("49800788")
             .Should()
             .BeFail()
             .Which.Should()
             .ContainSingle(ElfId.InvalidSex);
    }
}
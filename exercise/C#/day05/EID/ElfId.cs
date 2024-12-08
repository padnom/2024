using LanguageExt;
using LanguageExt.Common;

using System.Text.RegularExpressions;

namespace EID;

public partial class ElfId
{
    public const string ValueCannotBeNullOrWhitespaceOrEmpty = "Value cannot be null or whitespace or empty";
    public const string InvalidControlKey = "Invalid control key";
    public const string InvalidLength = "Invalid length";

    public const string InvalidSex = "Invalid sex value";

    public const string InvalidYear = "Invalid year value";

    public const string InvalidSerialNumber = "Invalid serial number value";

    public static Validation<Error, string> Validate(string? value)
    {
        return ValidateNotNullOrEmptyOrWhitespace(value)
               .Bind(ValidateLength)
               .Bind(ValidatePattern);
    }

    private static Validation<Error, string> ValidateControlKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value)
            || value.Length < 8)
        {
            return Prelude.Fail<Error, string>(Error.New(InvalidControlKey));
        }

        var firstSixDigits = int.Parse(value.Substring(0, 6));
        var controlKey = int.Parse(value.Substring(6, 2));

        return firstSixDigits % 97 == 97 - controlKey
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidControlKey));
    }

    private static Validation<Error, string> ValidateLength(string? value)
    {
        return value?.Length == 8
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidLength));
    }

    private static Validation<Error, string> ValidateNotEmpty(string value)
    {
        return string.Empty == value
            ? Prelude.Fail<Error, string>(Error.New(ValueCannotBeNullOrWhitespaceOrEmpty))
            : Prelude.Success<Error, string>(value);
    }

    private static Validation<Error, string> ValidateNotNullOrEmptyOrWhitespace(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? Prelude.Fail<Error, string>(Error.New(ValueCannotBeNullOrWhitespaceOrEmpty))
            : Prelude.Success<Error, string>(value);
    }

    private static Validation<Error, string> ValidatePattern(string value)
    {
        var validateSex = ValidateSex(value);
        var validateYear = ValidateYear(value);
        var validateSerialNumber = ValidateSerialNumber(value);
        var validateControlKey = ValidateControlKey(value);

        return (validateSex, validateYear, validateSerialNumber, validateControlKey).Apply((_, _, _, _) => value);
    }

    private static Validation<Error, string> ValidateSerialNumber(string value)
    {
        return Regex.IsMatch(value.AsSpan(3, 3), "^(?!000$)[0-9]{3}$")
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidSerialNumber));
    }

    private static Validation<Error, string> ValidateSex(string value)
    {
        return Regex.IsMatch(value.Substring(0, 1), "^[1-3]$")
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidSex));
    }

    private static Validation<Error, string> ValidateYear(string value)
    {
        return Regex.IsMatch(value.Substring(1, 2), "^[0-9]{2}$")
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidYear));
    }
}
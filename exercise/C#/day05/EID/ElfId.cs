using LanguageExt;
using LanguageExt.Common;

using System.Text.RegularExpressions;

namespace EID;

public class ElfId
{
    public const string ValueCannotBeNullOrWhitespace  = "Value cannot be null or whitespace";
    public const string InvalidControlKey = "Invalid control key";
    public const string InvalidPattern = "Invalid pattern";

    public static Validation<Error, string> Validate(string? value)
    {
        return ValidateNotNullOrWhitespace(value).Bind(MatchesPattern).Bind(IsValidControlKey);
    }

    private static Validation<Error, string> IsValidControlKey(string value)
    {
        var firstSixDigits = int.Parse(value.Substring(0, 6));
        var controlKey = int.Parse(value.Substring(6, 2));

        return firstSixDigits % 97 == 97 - controlKey
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidControlKey));
    }

    private static Validation<Error, string> MatchesPattern(string value)
    {
        var pattern = "^[1-3][0-9]{2}[0-9]{3}(0[1-9]|[1-8][0-9]|9[0-7])$";

        return Regex.IsMatch(value, pattern)
            ? Prelude.Success<Error, string>(value)
            : Prelude.Fail<Error, string>(Error.New(InvalidPattern));
    }

    private static Validation<Error, string> ValidateNotNullOrWhitespace(string? value)
    {

        return string.IsNullOrWhiteSpace(value)
            ? Prelude.Fail<Error, string>(Error.New(ValueCannotBeNullOrWhitespace))
            : Prelude.Success<Error, string>(value);
    }
}
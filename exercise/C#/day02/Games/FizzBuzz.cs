using LanguageExt;

namespace Games;

public class FizzBuzz
{
    public const int Min = 1;
    public const int Max = 100;
    private readonly Dictionary<int, string> _mapping;

    private FizzBuzz(Dictionary<int, string> mapping) => _mapping = mapping;

    public static FizzBuzz InitGame(Dictionary<int, string> Mapping) => new(Mapping);

    public  Option<string> Convert(int input) =>
        IsOutOfRange(input) ? Option<string>.None : ConvertSafely(input);

    private string ConvertSafely(int input)
    {
        var result = _mapping
                     .Filter(kvp => IsDivisibleBy(kvp.Key, input))
                     .Map(kvp => kvp.Value)                           
                     .Fold(string.Empty, (acc, value) => acc + value);

        return string.IsNullOrEmpty(result) 
            ? input.ToString() 
            : result;
    }

    private static bool IsDivisibleBy(int divisor, int input) => input % divisor == 0;

    private static bool IsOutOfRange(int input) => input is < Min or > Max;
}

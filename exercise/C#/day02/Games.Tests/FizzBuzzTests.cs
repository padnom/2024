using FluentAssertions;
using FluentAssertions.LanguageExt;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Games.Tests
{
    public class FizzBuzzTests
    {
        private readonly FizzBuzz fizzBuzzGame = FizzBuzz.InitGame(FizzBuzzMappings);
        private static readonly Dictionary<int, string> FizzBuzzMappings = new()
                                                                           {
                                                                               { 3, "Fizz" },
                                                                               { 5, "Buzz" },
                                                                               { 7, "Whizz" },
                                                                               { 11, "Bang" }
                                                                           };
        private static readonly string[] ValidParts = FizzBuzzMappings.Values.ToArray();

        [Theory]
        [InlineData(1, "1")]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(7, "Whizz")]
        [InlineData(11, "Bang")]
        [InlineData(15, "FizzBuzz")]
        [InlineData(21, "FizzWhizz")]
        [InlineData(30, "FizzBuzz")]
        [InlineData(33, "FizzBang")]
        [InlineData(35, "BuzzWhizz")]
        [InlineData(45, "FizzBuzz")]
        [InlineData(50, "Buzz")]
        [InlineData(55, "BuzzBang")]
        [InlineData(66, "FizzBang")]
        [InlineData(67, "67")]
        [InlineData(77, "WhizzBang")]
        [InlineData(82, "82")]
        [InlineData(85, "Buzz")]
        [InlineData(99, "FizzBang")]
        public void Returns_Number_Representation(int input, string expectedResult)
            => fizzBuzzGame.Convert(input)
                .Should()
                .BeSome(x => x.Should().Be(expectedResult));

        [Property]
        public Property Parse_Return_Valid_String_For_Numbers_Between_1_And_100()
            => Prop.ForAll(
                ValidInput(),
                IsConvertValid
            );

        private static Arbitrary<int> ValidInput()
            => Gen.Choose(FizzBuzz.Min, FizzBuzz.Max).ToArbitrary();

        private bool IsConvertValid(int number)
        {
            var resultOption = fizzBuzzGame.Convert(number);

            return resultOption.Match(
                result => IsValidFizzBuzzResult(result, number),
                () => false
            );
        }

        private static bool IsValidFizzBuzzResult(string result, int number)
        {
            if (result == number.ToString())
                return true;

            result = ValidParts.Aggregate(result, (current, part) => current.Replace(part, string.Empty));

            return string.IsNullOrEmpty(result);
        }

        [Property]
        public Property ParseFailForNumbersOutOfRange()
            => Prop.ForAll(
                InvalidInput(),
                x => fizzBuzzGame.Convert(x).IsNone
            );

        private static Arbitrary<int> InvalidInput()
            => Gen.Choose(-10_000, 10_000)
                .ToArbitrary()
                .Filter(x => x is < FizzBuzz.Min or > FizzBuzz.Max);
    }
}

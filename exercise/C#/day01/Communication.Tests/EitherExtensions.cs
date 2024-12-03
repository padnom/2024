using LanguageExt;

namespace Communication.Tests;

public static class EitherExtensions
{
    public static R GetRightUnsafe<L, R>(this Either<L, R> either)
    {
        return either.Match(
            Right: r => r,
            Left: _ => throw new InvalidOperationException("Either is in a Left state.")
        );
    }
}
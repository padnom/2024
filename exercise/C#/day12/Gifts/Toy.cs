namespace Gifts;

public sealed record ThirdChoiceToy(string Description): Toy(Description);
public record Toy(string Description);
public sealed record SecondeChoiceToy(string Description): Toy(Description);
public sealed record FirstChoiceToy(string Description): Toy(Description);

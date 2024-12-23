namespace Gifts;

public class Child
{
    public readonly string Name;
    private readonly Behavior _behavior;
    private WishList _wishList;

    private Child(string name, Behavior behavior, WishList wishList)
    {
        Name = name;
        _behavior = behavior;
        _wishList = wishList;
    }

    public static Child CreateChild(string name, Behavior behavior,WishList wishList) => new(name, behavior,wishList);

    public Option<Toy> GetToy() => _wishList.GetChoice(_behavior);
}
public enum Behavior
{
    Naughty,
    Nice,
    VeryNice
}
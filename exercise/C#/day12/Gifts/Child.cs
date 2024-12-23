namespace Gifts;

public class Child
{
    public string Name { get; }
    public Behavior Behavior { get; }
    public List<Toy> Wishlist { get; private set; }

    public Child(string name, Behavior behavior)
    {
        Name = name;
        Behavior = behavior;
        Wishlist = [];
    }

    public void SetWishList(Toy firstChoice, Toy secondChoice, Toy thirdChoice)
        => Wishlist = [firstChoice, secondChoice, thirdChoice];
}
public enum Behavior
{
    Naughty,
    Nice,
    VeryNice
}
namespace Gifts;

public sealed class Santa
{
    private  readonly Children _children = new();

    private Santa()
    {
    }
    
    public static  Santa CreateStanta => new();

    public Option<Toy> ChooseToyForChild(string childName)
    {
        return _children.GetChild(childName).Bind(c=>c.GetToy());
    }

    public void AddChild(Child child) => _children.AddChild(child);
}
namespace Gifts;

public sealed class Children
{
    private readonly List<Child> _childrenRepository = [];

    public Option<Child> GetChild(string name) => _childrenRepository.Find(c => c.Name == name) ?? Option<Child>.None;

    public  void AddChild(Child child) => _childrenRepository.Add(child);
}
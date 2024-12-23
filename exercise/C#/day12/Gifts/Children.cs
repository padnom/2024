namespace Gifts;

public sealed class Children
{
    private readonly List<Child> _childrenRepository = [];
    
    public  Option<Child> GetChild(string name)
    {
        return _childrenRepository.FirstOrDefault(c => c.Name == name) == null 
            ? Prelude.None 
            : _childrenRepository.FirstOrDefault(c => c.Name == name);
    }

    public  void AddChild(Child child) => _childrenRepository.Add(child);
}
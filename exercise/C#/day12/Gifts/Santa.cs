﻿namespace Gifts;

public class Santa
{
    private readonly List<Child> _childrenRepository = [];

    public Toy? ChooseToyForChild(string childName)
    {
        Child? found = null;
        for (int i = 0; i < _childrenRepository.Count; i++)
        {
            var currentChild = _childrenRepository[i];
            if (currentChild.Name == childName)
            {
                found = currentChild;
            }
        }

        if (found == null)
            throw new InvalidOperationException("No such child found");

        if (found.Behavior == Behavior.Naughty)
            return found.Wishlist[^1];

        if (found.Behavior == Behavior.Nice)
            return found.Wishlist[1];

        if (found.Behavior == Behavior.VeryNice)
            return found.Wishlist[0];

        return null;
    }

    public void AddChild(Child child) => _childrenRepository.Add(child);
}
using LanguageExt;
using LanguageExt.Common;

namespace SantaChristmasList.Operations;

public class Factory : Dictionary<Gift, ManufacturedGift>
{
    public  Either<Error,ManufacturedGift> FindManufacturedGift(Gift gift)
    {
        return ContainsKey(gift) ? this[gift] : Error.New("Missing gift: Gift wasn't manufactured!");
    }
}

public class Inventory : Dictionary<string, Gift>
{
    public  Either<Error,Gift> PickUpGift(string barCode)
    {
        return ContainsKey(barCode) ? this[barCode] : Error.New("Missing gift: The gift has probably been misplaced by the elves!");
    }
}

public class WishList : Dictionary<Child, Gift>
{
    public Either<Error,Gift> IdentifyGift(Child child)
    {
        return ContainsKey(child) ? this[child] : Error.New("Missing gift: Child wasn't nice this year!");
    }
}
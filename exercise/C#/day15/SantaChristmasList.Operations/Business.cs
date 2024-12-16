using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using static LanguageExt.Prelude;
namespace SantaChristmasList.Operations;

public class Business(Factory factory, Inventory inventory, WishList wishList)
{
    public Sleigh LoadGiftsInSleigh(params Child[] children)
    {
        var sleight = new Sleigh();

        Seq(children.AsEnumerable()).Iter(child =>
        {
            wishList.IdentifyGift(child)
                    .Bind(g => factory.FindManufacturedGift(g))
                    .Bind(m => inventory.PickUpGift(m.BarCode))
                    .Match(
                        error => sleight.Add(child, error.Message),
                        gift => sleight.Add(child, $"Gift: {gift.Name} has been loaded!")
                    );
        });

        return sleight;
    }
}
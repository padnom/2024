namespace Gifts;

public sealed class WishList
{
    private readonly FirstChoiceToy _firstChoice;
    private readonly SecondeChoiceToy _secondChoice ;
    private readonly ThirdChoiceToy _thirdChoice ;

    private WishList(FirstChoiceToy firstChoice, SecondeChoiceToy secondChoice, ThirdChoiceToy thirdChoice)
    {
        _firstChoice = firstChoice;
        _secondChoice = secondChoice;
        _thirdChoice = thirdChoice;
    }
    
    public static WishList Create (FirstChoiceToy firstChoice, SecondeChoiceToy secondChoice, ThirdChoiceToy third)
        => new(firstChoice, secondChoice, third);
    
    public Option<Toy> GetChoice(Behavior behavior)
    {
        return behavior switch
               {
                   Behavior.Naughty  => _thirdChoice,
                   Behavior.Nice     => _secondChoice,
                   Behavior.VeryNice => _firstChoice,
                   _                 => Prelude.None,
               };
    }
}
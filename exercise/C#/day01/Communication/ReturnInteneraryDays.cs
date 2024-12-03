using LanguageExt;
using LanguageExt.Common;

namespace Communication;

public sealed class ReturnInteneraryDays
{
    private NumberOfDaysBeforeChristmas _numberOfDaysBeforeChristmas;

    private ReturnInteneraryDays(ReturnTripDuration returnTripDuration, NumberOfDaysBeforeChristmas numberOfDaysBeforeChristmas)
    {
        ReturnTripDuration = returnTripDuration;
        NumberOfDaysBeforeChristmas = numberOfDaysBeforeChristmas;
    }
        
    public static Either<Error,ReturnInteneraryDays> Calculate(ReturnTripDuration numbersOfDaysForComingBack, NumberOfDaysBeforeChristmas 
                                                                   numberOfDaysBeforeChristmas)
    {
        if (numberOfDaysBeforeChristmas.Days<0 || numbersOfDaysForComingBack.Days<0)
        {
            return Error.New("Days cannot be negative");
        }
            
        return new ReturnInteneraryDays(numbersOfDaysForComingBack, numberOfDaysBeforeChristmas);
    }

    public ReturnTripDuration ReturnTripDuration { get; }
    public NumberOfDaysBeforeChristmas NumberOfDaysBeforeChristmas
    {
        get => _numberOfDaysBeforeChristmas;
        private set => _numberOfDaysBeforeChristmas = value;
    }
}
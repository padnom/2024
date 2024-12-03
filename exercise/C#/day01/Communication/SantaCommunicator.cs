using LanguageExt;
using LanguageExt.Common;

namespace Communication
{
    public class SantaCommunicator(int numberOfDaysToRest)
    {
        public Either<Error,string> ComposeMessage(Reinder reinder,
                                     NumberOfDaysBeforeChristmas numberOfDaysBeforeChristmas)
        {
            return ReturnInteneraryDays.Calculate(reinder.Location.ReturnTripDurationDays, numberOfDaysBeforeChristmas)
                                                       .Bind(returnInteneraryDays => DaysBeforeReturn(returnInteneraryDays))
                                                       .Bind(daysBeforeReturn => GenerateMessage(reinder, daysBeforeReturn));
        }

        private Either<Error, string> GenerateMessage(Reinder reinder, int daysBeforeReturn)
        {
            return $"Dear {reinder.Name.Value}, please return from {reinder.Location.Value} in {daysBeforeReturn
            } day(s) to be ready and rest before Christmas.";
        }

        public bool IsOverdue(Reinder reinder,ReturnInteneraryDays returnInteneraryDays, ILogger logger)
        {
            return DaysBeforeReturn(returnInteneraryDays).Match(
                                           Right: daysBeforeReturn => daysBeforeReturn < 0,
                                           Left: _ =>
                                           {
                                               logger.Log($"Overdue for {reinder.Name.Value} located {reinder.Location.Value}.");
                                               return true;
                                           });
        }

        private Either<Error,int> DaysBeforeReturn(ReturnInteneraryDays returnInteneraryDays)
        {
            var daysBeforeReturn = returnInteneraryDays.NumberOfDaysBeforeChristmas.Days - returnInteneraryDays.ReturnTripDuration.Days-numberOfDaysToRest;

            if (daysBeforeReturn < 0)
            {
                return Error.New("Overdue");
            }

            return daysBeforeReturn;
        }
    }
    public record Reinder(ReinderName Name, Location Location);
    public record Location(string Value,ReturnTripDuration ReturnTripDurationDays);
    public record ReinderName(string Value);
}
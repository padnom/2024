using Communication.Tests.Doubles;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using Xunit;

namespace Communication.Tests;

public class SantaCommunicatorTests
{
  private const string NorthPole = "North Pole";
  private const int NumberOfDaysToRest = 2;
  private readonly SantaCommunicator _communicator = new(NumberOfDaysToRest);
  private readonly TestLogger _logger = new();
  private readonly ReinderName Dasher = new("Dasher");
  private readonly NumberOfDaysBeforeChristmas NumberOfDayBeforeChristmas = new(24);


  [Fact]
  public void ComposeMessage()
  {
    var reinder = new ReindeerBuilder()
      .WithLocation(new Location(NorthPole, new ReturnTripDuration(5)))
      .Build();

    _communicator.ComposeMessage(reinder, NumberOfDayBeforeChristmas)
           .Should()
           .BeRight(c => c.Should()
                  .Be("Dear Dasher, please return from North Pole in 17 day(s) to be ready and rest before Christmas."));
  }

  [Fact]
  public void ShouldDetectOverdueReindeer()
  {
    var reinder = new ReindeerBuilder()
      .WithLocation(new Location(NorthPole, new ReturnTripDuration(24)))
      .Build();

    var returnInteneraryDays = ReturnInteneraryDays.Calculate(new ReturnTripDuration(24), NumberOfDayBeforeChristmas).GetRightUnsafe();
    bool overdue = _communicator.IsOverdue(reinder, returnInteneraryDays, _logger);

    overdue.Should().BeTrue();
    _logger.LoggedMessage().Should().Be("Overdue for Dasher located North Pole.");
  }

  [Fact]
  public void ShouldReturnFalseWhenNoOverdue()
  {
    var reinder = new ReindeerBuilder()
      .WithLocation(new Location(NorthPole, new ReturnTripDuration(5)))
      .Build();

    var numberOfDayBeforeChristmasError = new NumberOfDaysBeforeChristmas(24 - NumberOfDaysToRest - 1);
    var returnInteneraryDays = ReturnInteneraryDays.Calculate(new ReturnTripDuration(5), numberOfDayBeforeChristmasError).GetRightUnsafe();

    _communicator.IsOverdue(reinder, returnInteneraryDays, _logger).Should().BeFalse();
  }
}
namespace Communication.Tests;

public class ReindeerBuilder
{
    private const string NorthPole = "North Pole";
    private ReinderName _name = new("Dasher");
    private Location _location = new(NorthPole, new ReturnTripDuration(5));

    public ReindeerBuilder WithLocation(Location location)
    {
        _location = location;

        return this;
    }

    public Reinder Build()
    {
        return new Reinder(_name, _location);
    }
}
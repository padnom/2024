global using Xunit;
using FluentAssertions;
using FluentAssertions.LanguageExt;

namespace Gifts.Tests;

public sealed class SantaTest
{
    private static readonly FirstChoiceToy _playstation = new("playstation");
    private static readonly  ThirdChoiceToy _ball = new("ball");
    private static readonly SecondeChoiceToy _plush = new("plush");

    [Fact]
    public void GivenNaughtyChildWhenDistributingGiftsThenChildReceivesThirdChoice()
    {
        var bobby = Child.CreateChild("bobby", Behavior.Naughty, WishList.Create(_playstation, _plush, _ball));
        var santa = Santa.CreateStanta;
        santa.AddChild(bobby);
        var got = santa.ChooseToyForChild("bobby");
    
        got.Should().BeSome(toy =>
        {
            toy.Should().BeOfType<ThirdChoiceToy>();
            toy.Description.Should().Be("ball");
        });
    }

    [Fact]
    public void GivenNiceChildWhenDistributingGiftsThenChildReceivesSecondChoice()
    {
        var bobby = Child.CreateChild("bobby", Behavior.Nice, WishList.Create(_playstation, _plush, _ball));
        var santa = Santa.CreateStanta;
        santa.AddChild(bobby);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().BeSome(toy =>
        {
            toy.Should().BeOfType<SecondeChoiceToy>();
            toy.Description.Should().Be("plush");
        });
    }

    [Fact]
    public void GivenVeryNiceChildWhenDistributingGiftsThenChildReceivesFirstChoice()
    {
        var bobby = Child.CreateChild("bobby", Behavior.VeryNice, WishList.Create(_playstation, _plush, _ball));
        var santa = Santa.CreateStanta;
        santa.AddChild(bobby);
        var got = santa.ChooseToyForChild("bobby");

        got.Should().BeSome(toy =>
        {
            toy.Should().BeOfType<FirstChoiceToy>();
            toy.Description.Should().Be("playstation");
        });
    }

    [Fact]
    public void GivenNonExistingChildWhenDistributingGiftsThenExceptionThrown()
    {
        var santa = Santa.CreateStanta;
        var bobby = Child.CreateChild("bobby", Behavior.VeryNice, WishList.Create(_playstation, _plush, _ball));
        santa.AddChild(bobby);

        var chooseToyForChild =  santa.ChooseToyForChild("alice");

        chooseToyForChild.Should()
                         .BeNone();
    }
}
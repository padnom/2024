using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using System;
using System.Globalization;
using Xunit;

namespace Preparation.Tests
{
    public class SantaWorkshopServiceTests
    {
        private const string RecommendedAge = "recommendedAge";
        private readonly SantaWorkshopService _service = new();

        [Property]
        public void PrepareGift_ShouldInstantiateGift_ForValidInputs(
            NonEmptyString giftName,
            PositiveInt weight,
            NonEmptyString color,
            NonEmptyString material
        )
        {
            double validWeight = Math.Min(weight.Get, 5);

            string validGiftName = giftName.Get;
            string validColor = color.Get;
            string validMaterial = material.Get;

            var gift = _service.PrepareGift(validGiftName, validWeight, validColor, validMaterial);

            gift.Should().NotBeNull();

            gift.ToString()
                .Should()
                .ContainAll(
                    validGiftName,
                    validColor,
                    validWeight.ToString(CultureInfo.InvariantCulture),
                    validMaterial
                );
        }

        [Property]
        public void RetrieveAttributeOnGift_ShouldDefaultToZero_ForNonParsableRecommendedAge(
            string randomAge)
        {
            Prop.ForAll(
                RecommendedAgeArbitraries.InvalidStrings(),
                invalidAge =>
                {
                    var gift = new Gift("Furby", 1, "Multi", "Cotton");

                    gift.AddAttribute(RecommendedAge, invalidAge);

                    gift.RecommendedAge().Should().Be(0);
                }).QuickCheckThrowOnFailure();
        }

        [Property]
        public void AddingAttributeToGift_ShouldSetAttributeCorrectly(
            NonEmptyString giftName,
            PositiveInt weight,
            NonEmptyString color,
            NonEmptyString material,
            NonNegativeInt recommendedAge
        )
        {
            var gift = _service.PrepareGift(
                giftName.Get,
                Math.Min(weight.Get, 5),
                color.Get,
                material.Get
            );

            gift.AddAttribute(RecommendedAge, recommendedAge.Get.ToString());
            gift.RecommendedAge().Should().Be(recommendedAge.Get);
        }

        [Property]
        public void PrepareGift_ShouldThrowException_ForTooHeavyWeight(
            NonEmptyString giftName,
            NonNegativeInt weight,
            NonEmptyString color,
            NonEmptyString material
        )
        {
            Action action = () =>
                _service.PrepareGift(giftName.Get, weight.Get + 6, color.Get, material.Get);

            action
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("Gift is too heavy for Santa's sleigh");
        }
    }
}

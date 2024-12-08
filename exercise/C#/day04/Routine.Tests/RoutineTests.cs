using FakeItEasy;
using FluentAssertions;
using Routine.Tests.Fakes;
using Xunit;

namespace Routine.Tests
{
    public class RoutineTests
    {
        public RoutineTests() { }

        [Fact]
        public void StartRoutine_With_FakeItEasy()
        {
            var schedule = new Schedule();
            var emailService = A.Fake<IEmailService>();
            var reindeerFeeder = A.Fake<IReindeerFeeder>();
            var scheduleService = A.Fake<IScheduleService>();
            var routine = new Routine(emailService, scheduleService, reindeerFeeder);
            A.CallTo(() => scheduleService.TodaySchedule()).Returns(schedule);

            routine.Start();

            A.CallTo(() => emailService.ReadNewEmails()).MustHaveHappenedOnceExactly();
            A.CallTo(() => reindeerFeeder.FeedReindeers()).MustHaveHappenedOnceExactly();
            A.CallTo(() => scheduleService.OrganizeMyDay(schedule)).MustHaveHappenedOnceExactly();
            A.CallTo(() => scheduleService.Continue()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void StartRoutine_With_Manual_Test_Doubles()
        {
            var emailService = new FakeEmailService();
            var reindeerFeeder = new FakeReindeerFeeder();
            var scheduleService = new FakeScheduleService();
            var routine = new Routine(emailService, scheduleService, reindeerFeeder);

            routine.Start();

            emailService.IsReadNewEmailDone.Should().BeTrue();
            reindeerFeeder.IsFeedingDone.Should().BeTrue();
            scheduleService.IsContinueDone.Should().BeTrue();
            scheduleService.ScheduleOrganized.Should().NotBeNull();
        }
    }
}

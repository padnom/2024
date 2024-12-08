namespace Routine.Tests.Fakes
{
    public class FakeReindeerFeeder : IReindeerFeeder
    {
        public bool IsFeedingDone;

        public void FeedReindeers()
        {
            IsFeedingDone = true;
        }
    }
}

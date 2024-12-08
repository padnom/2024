namespace Routine.Tests.Fakes;

public class FakeScheduleService : IScheduleService
{
    public Schedule ScheduleOrganized { get; set; }
    public bool IsContinueDone { get; set; }

    public Schedule TodaySchedule() => new Schedule();

    public void OrganizeMyDay(Schedule schedule)
    {
        ScheduleOrganized = schedule;
    }

    public void Continue()
    {
        IsContinueDone = true;
    }
}

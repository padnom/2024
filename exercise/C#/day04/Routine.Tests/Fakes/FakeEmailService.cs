namespace Routine.Tests.Fakes;

public class FakeEmailService : IEmailService
{
    public bool IsReadNewEmailDone { get; set; }

    public void ReadNewEmails()
    {
        IsReadNewEmailDone = true;
    }
}

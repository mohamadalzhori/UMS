namespace UMS.Domain.Classes;

// session represents a timed instance of a class
public partial class Session
{
    private Session(long id, TimeOnly startTime, TimeOnly endTime, long classId)
    {
        Id = id;
        SetTime(startTime, endTime);
        ClassId = classId;
    }

    public long Id { get; init; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public void SetTime(TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime > endTime)
            throw new ArgumentException("End Time must not precede Start Time");

        StartTime = startTime;
        EndTime = endTime;
    }


    public long ClassId { get; private set; }
    public virtual Class Class { get; private set; } = null!;


    public static Session Create(TimeOnly startTime, TimeOnly endTime, long classId)
    {
        var session = new Session(0, startTime, endTime, classId);

        return session;
    }
}

namespace Shared;

public record Detection(int Id, TimeOnly Time, int TenthSeconds, int ParticipantId, int EventId)
{
    public override string ToString() =>
        $"#{Id} at {Time:HH:mm:ss}.{TenthSeconds} - participant {ParticipantId}, event {EventId}";
}


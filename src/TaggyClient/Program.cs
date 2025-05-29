using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TaggyClient;

public partial class Program(CancellationToken Token)
{
    private const int TaggyPort = 12555;
    private static readonly TimeSpan Delay = TimeSpan.FromMilliseconds(500);

    public static async Task Main(string[] args)
    {
        using var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
        await new Program(cts.Token).RunAsync();
    }

    private async Task RunAsync()
    {
        try
        {
            var taggyIp = await DiscoverTaggyAsync(Token);
            if (taggyIp is null)
            {
                Console.WriteLine("Taggy not found.");
                return;
            }

            using var client = new TcpClient();
            await client.ConnectAsync(taggyIp, TaggyPort, Token);
            using var network = client.GetStream();
            using var reader = new StreamReader(network, Encoding.ASCII);
            using var writer = new StreamWriter(network, Encoding.ASCII) { AutoFlush = true };

            while (!Token.IsCancellationRequested)
            {
                await writer.WriteLineAsync("H=j");
                var line = await reader.ReadLineAsync(Token);
                if (line is null)
                    break;

                if (TryParseDetection(line, out var detection))
                {
                    Console.WriteLine(detection);
                }
                else
                {
                    await Task.Delay(Delay, Token);
                }
            }
        }
        catch (OperationCanceledException)
        {
            // graceful cancellation
        }
    }

    private static async Task<IPAddress?> DiscoverTaggyAsync(CancellationToken token)
    {
        using var udp = new UdpClient(AddressFamily.InterNetwork) { EnableBroadcast = true };
        var payload = Encoding.ASCII.GetBytes("Hello Taggy!");
        await udp.SendAsync(payload, payload.Length, new IPEndPoint(IPAddress.Broadcast, TaggyPort));

        while (!token.IsCancellationRequested)
        {
            var task = udp.ReceiveAsync(token);
            UdpReceiveResult result;
            try
            {
                result = await task;
            }
            catch (OperationCanceledException)
            {
                return null;
            }

            var response = Encoding.ASCII.GetString(result.Buffer);
            if (response.StartsWith("I am ") && response.EndsWith(" - I hear you loud and clear"))
                return result.RemoteEndPoint.Address;
        }

        return null;
    }

    private static bool TryParseDetection(string line, out Detection detection)
    {
        detection = default!;
        if (line.StartsWith("-------"))
            return false;

        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts switch
        {
            [var idStr, var timeStr, var codes] when
                idStr.Length == 6 && int.TryParse(idStr, out var id) &&
                TryParseTime(timeStr, out var time, out var tenth) &&
                TryParseCodes(codes, out var participant, out var eventId)
                => (detection = new(id, time, tenth, participant, eventId)) is not null,
            _ => false
        };
    }

    private static bool TryParseTime(string text, out TimeOnly time, out int tenth)
    {
        time = default;
        tenth = default;
        return text.Length == 10 && text[8] == '.' &&
               TimeOnly.TryParseExact(text[..8], "HH:mm:ss", out time) &&
               int.TryParse(text[9].ToString(), out tenth);
    }

    private static bool TryParseCodes(string text, out int participant, out int eventId)
    {
        participant = default;
        eventId = default;
        return text.Length == 14 && text[0] == 'S' && text[7] == 'L' &&
               int.TryParse(text[1..7], out participant) &&
               int.TryParse(text[8..], out eventId);
    }
}

public record Detection(int Id, TimeOnly Time, int TenthSeconds, int ParticipantId, int EventId)
{
    public override string ToString() =>
        $"#{Id} at {Time:HH:mm:ss}.{TenthSeconds} - participant {ParticipantId}, event {EventId}";
}

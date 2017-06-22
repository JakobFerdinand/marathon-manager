using Logic.DIConfiguration;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("UI.TimeRecord.Tests")]
namespace UI.TimeRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            var recorder = container.GetInstance<TimeRecorder>();
            recorder.Run();
        }
    }
}
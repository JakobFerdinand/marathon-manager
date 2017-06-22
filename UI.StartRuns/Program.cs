using Logic.DIConfiguration;
using System;

namespace UI.StartRuns
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();

            var starter = container.GetInstance<RunStarter>();
            starter.Run();
        }
    }
}
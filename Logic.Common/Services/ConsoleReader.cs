using System;
using Logic.Common.Interfaces;

namespace Logic.Common.Services
{
    public class ConsoleReader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}

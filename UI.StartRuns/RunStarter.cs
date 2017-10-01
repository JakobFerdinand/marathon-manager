using Core;
using Core.Models;
using Logging.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.StartRuns
{
    internal class RunStarter
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RunStarter(ILogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            var categories = _unitOfWork.Categories.GetNotStarted();

            Console.WriteLine($"Noch nicht gestartete Läufe ({categories.Count()}):");
            foreach (var c in categories.OrderBy(c => c.PlannedStartTime))
                Console.WriteLine($"Id: {c.Id, 3} | {c.Name,-30} | geplant: {c.PlannedStartTime.ToString("HH:mm:ss")}");

            Console.WriteLine("\nWelche Läufe sollen gestartet werden?");
            Console.WriteLine("Id eingeben und mit Return bestätigen. Zum Starten 'start' eingeben.");
            var categoriesToStart = new HashSet<Category>();
            while (true)
            {
                Console.WriteLine();
                var eingabe = Console.ReadLine();

                if (eingabe.ToLowerInvariant() == "start")
                    break;

                if(int.TryParse(eingabe, out int id))
                {
                    var category = categories.SingleOrDefault(c => c.Id == id);

                    if (category is null)
                        Console.WriteLine($"Id: {id} ist ungültig.");
                    else
                        categoriesToStart.Add(category);
                }
                else
                {
                    Console.WriteLine($"Die Eingabe '{eingabe}' ist ungültig.");
                }

                Console.WriteLine($"Aktuell ausgewählte Läufe ({categoriesToStart.Count}):");
                foreach (var c in categoriesToStart)
                    Console.WriteLine($"\t{c.Id,3} | {c.Name}");
            }

            var startTime = DateTime.Now;
            foreach (var c in categoriesToStart)
                c.Starttime = startTime;

            _unitOfWork.Complete();

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n\nSoeben gestartete Läufe:");
            Console.ForegroundColor = color;
            foreach (var c in categoriesToStart)
                Console.WriteLine($"\t{c.Id,3} | {c.Name,15} | {c.Starttime.Value.ToString("HH:mm:ss:fff")}");

            Console.ReadKey();
        }
    }
}

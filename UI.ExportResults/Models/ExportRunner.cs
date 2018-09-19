using Core.Models;
using System;

namespace UI.ExportResults.Models
{
    public class ExportRunner
    {
        public ExportRunner(int rang, int startnummer, string vorname, string nachname, Gender geschlecht, string verein, int geburtsjahr, TimeSpan zeit)
        {
            Rang = rang;
            Startnummer = startnummer;
            Vorname = vorname;
            Nachname = nachname;
            Geschlecht = geschlecht;
            Verein = verein;
            Geburtsjahr = geburtsjahr;
            Zeit = zeit;
        }

        public int Rang { get; }
        public int Startnummer { get; }
        public string Vorname { get; }
        public string Nachname { get; }
        public Gender Geschlecht { get; }
        public string Verein { get; }
        public int Geburtsjahr { get; }
        public TimeSpan Zeit { get; }
    }

    public static partial class Extensions
    {
        public static ExportRunner WithRang(this ExportRunner @this, int rang)
            => new ExportRunner(
                rang,
                @this.Startnummer,
                @this.Vorname,
                @this.Nachname,
                @this.Geschlecht,
                @this.Verein,
                @this.Geburtsjahr,
                @this.Zeit);
    }
}

namespace UI.ExportResults.Models
{
    public sealed class ExportRunnerSimple
    {
        public ExportRunnerSimple(string vorname, string nachname, int startnummer, int geburtsjahr, string kategorie)
        {
            Vorname = vorname;
            Nachname = nachname;
            Startnummer = startnummer;
            Geburtsjahr = geburtsjahr;
            Kategorie = kategorie;
        }

        public int Startnummer { get; }
        public string Nachname { get; }
        public string Vorname { get; }
        public int Geburtsjahr { get; }
        public string Kategorie { get; }
    }
}

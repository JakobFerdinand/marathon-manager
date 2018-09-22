using CsvHelper.Configuration;
using UI.ExportResults.Models;

namespace UI.ExportResults.CsvMappings
{
    public class ExportOldestRunnerMap : ClassMap<ExportRunner>
    {
        public ExportOldestRunnerMap()
        {
            Map(r => r.Rang).Name("Rang");
            Map(r => r.Startnummer).Name("Startnummer");
            Map(r => r.Vorname).Name("Vorname");
            Map(r => r.Nachname).Name("Nachname");
            Map(r => r.Zeit).Name("Zeit");
            Map(r => r.Verein).Name("Verein");
            Map(r => r.Geburtsjahr).Name("Geburtsjahr");
            Map(r => r.Geschlecht).Name("Geschlecht");
        }
    }
}

using CsvHelper.Configuration;
using UI.ExportResults.Models;

namespace UI.ExportResults.CsvMappings
{
    public class ExportRunnerSimpleMap : ClassMap<ExportRunnerSimple>
    {
        public ExportRunnerSimpleMap()
        {
            Map(r => r.Startnummer).Name("Startnummer");
            Map(r => r.Nachname).Name("Nachname");
            Map(r => r.Vorname).Name("Vorname");
            Map(r => r.Geburtsjahr).Name("Geburtsjahr");
            Map(r => r.Kategorie).Name("Strecke");
        }
    }
}

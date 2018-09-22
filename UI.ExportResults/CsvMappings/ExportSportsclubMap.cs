using CsvHelper.Configuration;
using UI.ExportResults.Models;

namespace UI.ExportResults.CsvMappings
{
    public class ExportSportsclubMap : ClassMap<ExportSportsclub>
    {
        public ExportSportsclubMap()
        {
            Map(s => s.Rang).Name("Rang");
            Map(s => s.Name).Name("Verein");
            Map(s => s.Count).Name("Anzahl");
        }
    }
}

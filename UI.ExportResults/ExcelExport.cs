using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace UI.ExportResults
{
    public static class ExcelExport
    {
        public static void Export<T>(string path, IEnumerable<T> data)
        {
            using (var p = new ExcelPackage(new FileInfo(path)))
            {
                var ws = p.Workbook.Worksheets.Add("Alle Läufer");

            }
        }
    }
}

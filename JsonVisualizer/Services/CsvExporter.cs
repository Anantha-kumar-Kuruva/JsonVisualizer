using JsonVisualizer.Models;

namespace JsonVisualizer.Services
{
    public class CsvExporter
    {
        public static void ExportToCsv(List<TimeEntry> entries)
        {
            var lines = new List<string> { "EmployeeName,TotalHours" };
            lines.AddRange(entries.Select(e => $"{e.EmployeeName},{e.TimeWorked:F2}"));
            Directory.CreateDirectory("Output");
            File.WriteAllLines("Output/summary.csv", lines);
        }
    }
}

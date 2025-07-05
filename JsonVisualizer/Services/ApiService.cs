using Newtonsoft.Json.Linq;
using JsonVisualizer.Models;

namespace JsonVisualizer.Services
{
    public class ApiService
    {
        public async Task<List<TimeEntry>> FetchTimeEntriesAsync()
        {
            string jsonPath = "employeetimereport.json"; // Local file instead of API
            var jsonContent = await File.ReadAllTextAsync(jsonPath);
            var jArray = JArray.Parse(jsonContent);

            var grouped = jArray
                .Where(x =>
                {
                    var startStr = x["StarTimeUtc"]?.ToString();
                    var endStr = x["EndTimeUtc"]?.ToString();
                    if (DateTime.TryParse(startStr, out var start) && DateTime.TryParse(endStr, out var end))
                        return end > start;
                    return false;
                })
                .GroupBy(x => x["EmployeeName"]?.ToString())
                .Where(g => !string.IsNullOrWhiteSpace(g.Key))
                .Select(g => new TimeEntry
                {
                    EmployeeName = g.Key!,
                    TimeWorked = g.Sum(x =>
                    {
                        DateTime start = DateTime.Parse(x["StarTimeUtc"]!.ToString());
                        DateTime end = DateTime.Parse(x["EndTimeUtc"]!.ToString());
                        return (end - start).TotalHours;
                    })
                })
                .OrderByDescending(e => e.TimeWorked)
                .ToList();

            return grouped;
        }
    }
}

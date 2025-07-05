using Newtonsoft.Json.Linq;
using JsonVisualizer.Models;

namespace JsonVisualizer.Services
{
    public class ApiService
    {
        private static readonly HttpClient client = new();

        public async Task<List<TimeEntry>> FetchTimeEntriesAsync()
        {
            string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            var response = await client.GetStringAsync(url);
            var jArray = JArray.Parse(response);

            var grouped = jArray
                .Where(x =>
                {
                    DateTime start = DateTime.Parse(x["starTimeUtc"]!.ToString());
                    DateTime end = DateTime.Parse(x["endTimeUtc"]!.ToString());
                    return end > start;
                })
                .GroupBy(x => x["employeeName"]?.ToString())
                .Select(g => new TimeEntry
                {
                    EmployeeName = g.Key,
                    TimeWorked = g.Sum(x =>
                    {
                        DateTime start = DateTime.Parse(x["starTimeUtc"]!.ToString());
                        DateTime end = DateTime.Parse(x["endTimeUtc"]!.ToString());
                        return (end - start).TotalHours;
                    })
                })
                .OrderByDescending(e => e.TimeWorked)
                .ToList();

            return grouped;
        }
    }
}

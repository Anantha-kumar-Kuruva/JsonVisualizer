using JsonVisualizer.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var api = new ApiService();
        var entries = await api.FetchTimeEntriesAsync();

        HtmlRenderer.GenerateHtml(entries);
        PieChartGenerator.GeneratePieChart(entries);
        CsvExporter.ExportToCsv(entries);

        Console.WriteLine("✅ report.html, piechart.png, and summary.csv generated in Output/ folder.");
    }
}

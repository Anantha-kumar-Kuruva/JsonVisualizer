using JsonVisualizer.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var api = new ApiService();
        var entries = await api.FetchTimeEntriesAsync();

        HtmlRenderer.GenerateHtml(entries);
        PieChartGenerator.GeneratePieChart(entries);

        Console.WriteLine("HTML and PNG generated in Output/ folder.");
    }
}

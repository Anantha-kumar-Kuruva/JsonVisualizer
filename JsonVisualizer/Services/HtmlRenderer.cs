using JsonVisualizer.Models;
using System.Text;

namespace JsonVisualizer.Services
{
    public class HtmlRenderer
    {
        public static void GenerateHtml(List<TimeEntry> entries)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html><head><style>");
            sb.AppendLine("table { border-collapse: collapse; width: 50%; }");
            sb.AppendLine("th, td { border: 1px solid #ddd; padding: 8px; }");
            sb.AppendLine("tr.low-hours { background-color: #ffcccc; }");
            sb.AppendLine("</style></head><body>");
            sb.AppendLine("<h2>Employee Hours</h2><table>");
            sb.AppendLine("<tr><th>Name</th><th>Total Hours</th></tr>");

            foreach (var entry in entries)
            {
                string rowClass = entry.TimeWorked < 100 ? " class='low-hours'" : "";
                sb.AppendLine($"<tr{rowClass}><td>{entry.EmployeeName}</td><td>{entry.TimeWorked}</td></tr>");
            }

            sb.AppendLine("</table></body></html>");
            Directory.CreateDirectory("Output");
            File.WriteAllText("Output/report.html", sb.ToString());
        }
    }
}

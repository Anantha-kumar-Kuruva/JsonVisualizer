using JsonVisualizer.Models;
using SkiaSharp;

namespace JsonVisualizer.Services
{
    public class PieChartGenerator
    {
        public static void GeneratePieChart(List<TimeEntry> entries)
        {
            int width = 1000, height = 700;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            float total = (float)entries.Sum(e => e.TimeWorked);
            float startAngle = 0;

            var paint = new SKPaint { IsAntialias = true };
            var rect = new SKRect(100, 50, width - 300, height - 150);
            var random = new Random();

            var legendY = 60;
            int colorIndex = 0;
            List<(string Name, SKColor Color)> legend = new();

            var font = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 16,
                IsAntialias = true
            };

            foreach (var entry in entries)
            {
                float sweep = 360f * ((float)entry.TimeWorked / total);
                var color = new SKColor(
                    (byte)random.Next(100, 256),
                    (byte)random.Next(100, 256),
                    (byte)random.Next(100, 256));
                paint.Color = color;

                // Draw pie slice
                canvas.DrawArc(rect, startAngle, sweep, true, paint);

                // Mid angle label position
                float midAngle = startAngle + sweep / 2;
                double radians = midAngle * Math.PI / 180;
                float labelX = rect.MidX + (float)(Math.Cos(radians) * 150);
                float labelY = rect.MidY + (float)(Math.Sin(radians) * 100);
                canvas.DrawText(entry.EmployeeName, labelX, labelY, font);

                legend.Add((entry.EmployeeName, color));
                startAngle += sweep;
                colorIndex++;
            }

            // Draw legend
            int legendStartX = width - 200;
            canvas.DrawText("Legend:", legendStartX, legendY, font);
            legendY += 20;

            foreach (var item in legend)
            {
                paint.Color = item.Color;
                canvas.DrawRect(legendStartX, legendY - 12, 20, 15, paint);
                canvas.DrawText(item.Name, legendStartX + 30, legendY, font);
                legendY += 20;
            }

            // Save image
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            Directory.CreateDirectory("Output");
            using var stream = File.OpenWrite("Output/piechart.png");
            data.SaveTo(stream);
        }
    }
}

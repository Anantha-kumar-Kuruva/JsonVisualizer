using JsonVisualizer.Models;
using SkiaSharp;

namespace JsonVisualizer.Services
{
    public class PieChartGenerator
    {
        public static void GeneratePieChart(List<TimeEntry> entries)
        {
            int width = 800, height = 600;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            float total = (float)entries.Sum(e => e.TimeWorked);
            float startAngle = 0;

            var paint = new SKPaint { IsAntialias = true };
            var rect = new SKRect(100, 100, width - 100, height - 100);
            var random = new Random();

            foreach (var entry in entries)
            {
                float sweep = 360f * ((float)entry.TimeWorked / total);
                paint.Color = new SKColor(
                    (byte)random.Next(100, 256),
                    (byte)random.Next(100, 256),
                    (byte)random.Next(100, 256));

                canvas.DrawArc(rect, startAngle, sweep, true, paint);
                startAngle += sweep;
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            Directory.CreateDirectory("Output");
            using var stream = File.OpenWrite("Output/piechart.png");
            data.SaveTo(stream);
        }
    }
}

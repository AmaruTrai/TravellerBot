using System;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;

namespace TravellerBotAPI.Support
{
	public class ImageManager
	{
		private static readonly SKTypeface font;
		private static readonly SKBitmap template;

		static ImageManager()
		{
			using (var fontStream = new MemoryStream(Properties.Resources.SpaceFont)) {
				font = SKTypeface.FromStream(fontStream);
				template = SKBitmap.Decode(Properties.Resources.HUD);
			}
		}
		/// <summary>
		/// Генерирует изображение с текстом на фоне HUD.
		/// </summary>
		public static void GenerateUWPImage(IReadOnlyList<string> text, string targetFilePath)
		{
			var bitmap = template.Copy();
			var brush = new SKPaint
			{
				Typeface = font,
				TextSize = 40.0f,
				IsAntialias = true,

				Color = new SKColor(255, 255, 255, 255)
			};

			var size = MeasureText(text, brush);
			size.Width = (int)(size.Width * 1.29);
			size.Height = (int)(size.Height * 1.33);

			bitmap = bitmap.Resize(size, SKFilterQuality.High);

			var canvas = new SKCanvas(bitmap);
			canvas.DrawBitmap(bitmap, 0, 0);
			canvas.ResetMatrix();

			var y = size.Height * 0.1125f;
			var x = size.Width * 0.075f;
			foreach (var line in text)
			{
				canvas.DrawText(line, x, y, brush);
				y += brush.FontSpacing;
			}

			var image = SKImage.FromBitmap(bitmap);
			var data = image.Encode(SKEncodedImageFormat.Jpeg, 90);

			using (var stream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write)) {
				data.SaveTo(stream);
			}

			GC.Collect();
		}

		/// <summary>
		/// Измеряет размер текста в пикселях.
		/// </summary>
		public static SKSizeI MeasureText(IReadOnlyList<string> text, SKPaint brush)
		{
			float width = 0;
			float height = 0;
			foreach (var line in text) {
				var measure = brush.MeasureText(line);
				if (measure > width) {
					width = measure;
				}
				height += brush.FontSpacing;
			}
			return new SKSizeI((int)width, (int)height);
		}

	}
}

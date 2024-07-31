using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Drawing.Point;

namespace ImageRecognitionLibrary
{
    public class ImageFinder
    {
        public (bool found, Point center) FindImageInScreenshot(string imagePath, string screenshotPath, double threshold = 0.8)
        {
            using (Mat needle = Cv2.ImRead(imagePath, ImreadModes.Color))
            using (Mat haystack = Cv2.ImRead(screenshotPath, ImreadModes.Color))
            {
                Mat result = new Mat();
                Cv2.MatchTemplate(haystack, needle, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

                if (maxVal >= threshold)
                {
                    int centerX = maxLoc.X + needle.Width / 2;
                    int centerY = maxLoc.Y + needle.Height / 2;
                    return (true, new Point(centerX, centerY));
                }

                return (false, Point.Empty);
            }
        }
    }
}

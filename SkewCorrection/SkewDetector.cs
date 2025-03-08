using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace SkewCorrection
{
    public static class SkewDetector
    {
        private static double GetMedian(List<double> angles)
        {
            angles.Sort(); // Sort angles in ascending order
            int mid = angles.Count / 2;

            // If odd number of elements, return middle one
            if (angles.Count % 2 != 0)
                return angles[mid];

            // If even number of elements, return the average of the two middle ones
            return (angles[mid - 1] + angles[mid]) / 2.0;
        }

        public static double DetectSkewAngle(string imagePath)
        {
            using Mat image = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);
            
            CvInvoke.AdaptiveThreshold(image, image, 255, AdaptiveThresholdType.GaussianC, ThresholdType.Binary, 15, 10);

            Mat edges = new Mat();
            CvInvoke.Canny(image, edges, 50, 200); 

            LineSegment2D[] lines = CvInvoke.HoughLinesP(edges, 1, Math.PI / 180, 50, 50, 20);

            if (lines.Length == 0)
                return 0; // No skew detected

            var angles = lines
                .Select(line => Math.Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X) * (180 / Math.PI))
                .Where(angle => Math.Abs(angle) > 1) 
                .ToList();

            if (angles.Count == 0)
                return 0; // No skew detected

            double angle = GetMedian(angles);

            return -angle;
        }

        public static void CorrectSkew(string imagePath, string outputPath)
        {
            using Mat image = CvInvoke.Imread(imagePath, ImreadModes.Grayscale);
            double skewAngle = DetectSkewAngle(imagePath);

            if (Math.Abs(skewAngle) < 0.5) // No need to rotate
            {
                image.Save(outputPath);
                return;
            }

            int newWidth = (int)(image.Width * 1.5);
            int newHeight = (int)(image.Height * 1.5);
            Mat expandedImage = new Mat(newHeight, newWidth, DepthType.Cv8U, 1);
            CvInvoke.CopyMakeBorder(image, expandedImage, 
                newHeight / 4, newHeight / 4, 
                newWidth / 4, newWidth / 4, 
                BorderType.Constant, new MCvScalar(255));

            var center = new System.Drawing.Point(expandedImage.Width / 2, expandedImage.Height / 2);
            
            Mat rotationMatrix = new Mat();
            CvInvoke.GetRotationMatrix2D(center, -skewAngle, 1.0, rotationMatrix);

            Mat rotatedImage = new Mat();
            CvInvoke.WarpAffine(expandedImage, rotatedImage, rotationMatrix, expandedImage.Size, 
                Inter.Linear, Warp.Default, BorderType.Constant, new MCvScalar(255)); 

            // 🔥 FIX: Crop back to the original image size
            int x = (rotatedImage.Width - image.Width) / 2;
            int y = (rotatedImage.Height - image.Height) / 2;
            Mat croppedImage = new Mat(rotatedImage, new System.Drawing.Rectangle(x, y, image.Width, image.Height));

            // Save the corrected image
            croppedImage.Save(outputPath);
        }
    }
}

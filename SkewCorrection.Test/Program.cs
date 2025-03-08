using SkewCorrection;

string inputImage = "C:\\Users\\migue\\source\\SkewCorrection-EmguCV\\SkewCorrection.Test\\skewed_image.jpg";
string correctedImage = "C:\\Users\\migue\\source\\SkewCorrection-EmguCV\\SkewCorrection.Test\\corrected_image.jpg";

double skewAngle = SkewDetector.DetectSkewAngle(inputImage);
Console.WriteLine($"Detected Skew Angle: {skewAngle} degrees");

SkewDetector.CorrectSkew(inputImage, correctedImage);
Console.WriteLine("Corrected image saved!");

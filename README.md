# SkewCorrection-EmguCV

A .NET 9 library for **detecting and correcting skew** in scanned images using OpenCV (Emgu.CV).

## 📌 Features
✅ Detects skew angle in documents using **Hough Transform**  
✅ Corrects skew using **Affine Transformation**  
✅ Supports **Windows, Linux, and macOS**  
✅ Lightweight and efficient  

---

## 🚀 Installation

### **1️⃣ Clone the Repository**
```bash
git clone https://github.com/yourusername/SkewCorrection-EmguCV.git
cd SkewCorrection-EmguCV
```

### **2️⃣ Install Dependencies**
The **class library** (`SkewCorrection`) is OS-agnostic, but you must install the correct **Emgu.CV runtime** for your platform.

#### **🔹 Windows**
```bash
dotnet add SkewCorrection.Test package Emgu.CV.Runtime.Windows
```

#### **🔹 Linux**
```bash
dotnet add SkewCorrection.Test package Emgu.CV.Runtime.Linux
```

#### **🔹 macOS**
```bash
dotnet add SkewCorrection.Test package Emgu.CV.Runtime.Macos
```

---

## 🛠️ Usage

### **1️⃣ Add Reference to SkewCorrection**
If you are using this library in another project, add a reference:
```bash
dotnet add package SkewCorrection
```

### **2️⃣ Detect and Correct Skew**
Modify your `.cs` file to use the library:

```csharp
using SkewCorrection;

string inputImage = "path_to_skewed_image.jpg";
string correctedImage = "corrected_image.jpg";

double skewAngle = SkewDetector.DetectSkewAngle(inputImage);
Console.WriteLine($"Detected Skew Angle: {skewAngle} degrees");

SkewDetector.CorrectSkew(inputImage, correctedImage);
Console.WriteLine("Corrected image saved!");
```

---

## 📢 Running the Test Project
To test the skew detection and correction:
```bash
dotnet run --project SkewCorrection.Test
```
This will:
1. Detect the skew angle of a sample image.
2. Correct the skew and save the output.

---

## 🛠️ Troubleshooting
### **Issue: `cvextern.dll` Not Found** (Windows)
If you see **`DllNotFoundException: Unable to load DLL 'cvextern'`**, ensure you have installed the correct runtime:
```bash
dotnet add SkewCorrection.Test package Emgu.CV.Runtime.Windows
```

### **Issue: `cv2.so` Not Found** (Linux/macOS)
Make sure OpenCV libraries are installed:
```bash
sudo apt install libopencv-dev  # Linux (Ubuntu)
brew install opencv  # macOS
```

If the issue persists, manually copy OpenCV native binaries from:
```
~/.nuget/packages/emgu.cv.runtime.[platform]/[version]/runtimes/[os]/native/
```
into your project’s `bin/Release/net9.0/` folder.

---

## 🏗️ Contributing
1. **Fork the repo** and create a new branch.  
2. Make your changes and **commit** them.  
3. Open a **pull request** for review.  

---

## 📄 License
MIT License - Free to use and modify.

---

## 🔗 Links
- [Official Emgu.CV Documentation](https://www.emgu.com/wiki/)
- [OpenCV Documentation](https://docs.opencv.org/)

---

📧 **Need Help?** Open an issue in the repository!


using System;
using System.IO;
using System.Reflection;

namespace SesameTest
{
    public class ProtectedData
    {
        public static string Read(string name)
        {
            // SUPER HACK!!!  Not actually cryptographically protected or anything.
            // Read "protected" data from a file on disk.  These file is not checked in so that they don't appear in git history.

            string fileName = $"{name}.protected.dat";
            string folderName = Path.GetDirectoryName(typeof(ProtectedData).GetTypeInfo().Assembly.Location);
            string filePath = Path.Combine(folderName, fileName);

            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"Invalid protected data name '{name}'");
            }

            return File.ReadAllText(filePath);
        }
    }
}

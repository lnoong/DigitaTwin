using System.Diagnostics;
using System.IO;

namespace CitrioN.Common
{
  public static class FileUtility
  {
    public static string GetFileDirectory(string filePath)
    {
      if (string.IsNullOrEmpty(filePath)) { return string.Empty; }
      return new FileInfo(filePath).Directory.FullName;
    }

    public static void OpenFileDirectory(string directoryPath)
    {
      if (Directory.Exists(directoryPath))
      {
        Process.Start($@"{directoryPath}");
      }
      else
      {
        ConsoleLogger.LogWarning($"Unable to open directory '{directoryPath}' because it doesn't exist.");
      }
    }

    public static void DeleteFile(string path)
    {
      if (File.Exists(path))
      {
        File.Delete(path);
        ConsoleLogger.Log($"Successfully deleted file at {path}");
      }
      else
      {
        ConsoleLogger.LogWarning($"No file found to delete at {path}");
      }
    }
  }
}
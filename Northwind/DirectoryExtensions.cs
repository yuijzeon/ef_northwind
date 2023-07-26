using System.Text.RegularExpressions;

namespace Northwind;

public static class DirectoryExtensions
{
    public static DirectoryInfo FindParent(this DirectoryInfo sourceDirectory, Regex folderName)
    {
        var directory = sourceDirectory;

        while (directory != null && !folderName.IsMatch(directory.Name))
        {
            directory = directory.Parent;
        }

        return directory;
    }

    public static DirectoryInfo FindParent(this DirectoryInfo sourceDirectory, string folderName)
    {
        var directory = sourceDirectory;

        while (directory != null && !directory.Name.Equals(folderName, StringComparison.OrdinalIgnoreCase))
        {
            directory = directory.Parent;
        }

        return directory;
    }
}
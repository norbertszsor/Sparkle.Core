namespace Sparkle.Shared.Comparators
{
    public class FileInfoCreationDateComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo? x, FileInfo? y)
        {
            if (x == null || y == null) 
                throw new ArgumentNullException(nameof(x), "FileInfo is null");

            return x.CreationTime.CompareTo(y.CreationTime);
        }
    }
}

namespace Sparkle.Api.Shared.Comparers
{
    public class FileInfoCreationDateComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo? x, FileInfo? y)
        {
            if (x == null || y == null)
                throw new ArgumentNullException();

            return x.CreationTime.CompareTo(y.CreationTime);
        }
    }
}

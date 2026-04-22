namespace _10433939_PROG7311_Part2.Models
{
    public class UploadedDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}

namespace TendaGo.Api.Reporting
{
    public class ReportResult
    {
        public ReportResult(byte[] content)
        {
            Content = content;
        }

        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public string Enconding { get; set; }
        public string Extension { get; set; }
        public string[] Streams { get; set; }
        public Warning[] Warnings { get; set; }

    }
}

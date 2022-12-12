namespace documentation.Models
{
    public class documents
    {
        public int DocumentId { get; set; }
        public string? DocumentTitle { get; set; }
        public string? DocumentDiscription { get; set; }
        public string? DocumentNumber { get; set; }
        public int DocumentAuthor { get; set; }
        public int DocumentRecipient { get; set; }
    }
}

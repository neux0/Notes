using System.ComponentModel.DataAnnotations;

namespace NotesAPI.Models
{
    public class Note
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Visible { get; set; }
        public DateTime Date { get; set; }
    }
    public class NotePutted
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Visible { get; set; }
        public DateTime Date { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeltaQuestion.Models;

namespace DeltaQuestion.Data
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public List<Intrests> Groups { get; set; }
        public Confidentiality Confidentiality { get; set; }

        public DateTime CreatedAt = DateTime.Now;
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeltaQuestion.Models;

namespace DeltaQuestion.Data
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string Email { get; set; }
        public List<Intrests>  Instrests{ get; set; }
        public List<Skilled> Skilleds{ get; set; }

    }
}

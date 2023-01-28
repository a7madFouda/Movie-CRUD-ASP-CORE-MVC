using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreMVC.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte ID { get; set; } // Because data type is byte We Use Data annotation
        [Required,MaxLength(100)]
        public string Name { get; set; }
    }
}

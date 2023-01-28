using coreMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace coreMVC.ViewModels
{
    public class MovieFormViewModel
    {
        public int ID { get; set; }
        [Required, StringLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [Required, StringLength(2500)]
        public string Storyline { get; set; }
        public byte[]? Poster { get; set; }
        [Display(Name ="Genre")]
        public byte GenreID { get; set; }
        public IEnumerable<Genre>? Genres { get; set; }
    }
}

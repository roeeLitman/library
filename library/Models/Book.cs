using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.Models
{
    public class Book
    {
       

        [Key]
        public int Id { get; set; }

        [Display(Name ="כותרת")]
        public string Title { get; set; } = "";

        [Display(Name ="זאנר")]
        public string genre { get; set; } = "";

        [Display(Name ="גובה")]
        public int Height { get; set; }

        [Display(Name ="נמצא במדף")]
        public Shelf? Shelf { get; set; }

        [NotMapped]
        public int BookOnShelf { get; set; }
    }
}

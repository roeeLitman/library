using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.Models
{
    public class Shelf
    {
        public Shelf() 
        {
            Ark = new Ark();
        }

        [Key]
        public int Id {  get; set; }

        [Display(Name = "גובה")]
        public int Height { get; set; }

        [Display(Name = "שם מדף")]
        public string Name { get; set; } = "";

        [Display(Name = "שייך לספריית")]
        public Ark? Ark{ get; set; }

        [NotMapped]
        public int ArkId12 { get; set; }
    }
}

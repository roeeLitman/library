using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library.Models
{
    public class Ark
    {
        public Ark() 
        {
            Shelves = new List<Shelf>();
        }

        [Key]
        public int Id {  get; set; }

        [Display(Name ="זאנר")]
        public string genre { get; set; } = "";

        public List<Shelf> Shelves {get; set;}

        [NotMapped]
        public Shelf? AddShelf 
        {
            get { return null; }

            set 
            {
                Shelves.Add(
                new Shelf
                {
                    Ark = this,
                    Height = value.Height,
                    Name = value.Name
                });
                
            }
        }

    }
}

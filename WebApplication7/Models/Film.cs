using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Tytul { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int GatunekId { get; set; }
        [ForeignKey("GatunekId")]
        public virtual Gatunek Gatunek { get; set; }


        /*        [Range(1, 100)]
       [DataType(DataType.Currency)]
       [Column(TypeName = "decimal(18, 2)")]
       public decimal Price { get; set; }*/

/*        public Film(int id, string tytul, DateTime releaseDate, int gatunekId, Gatunek gatunek)
        {
            this.Id = id;
            this.Tytul = tytul;
            this.ReleaseDate = releaseDate;
            this.GatunekId = gatunekId;
            this.Gatunek = gatunek;
        }*/
    }
}

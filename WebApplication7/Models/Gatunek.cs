using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models
{
    public class Gatunek
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public String Name { get; set; }
    }
}



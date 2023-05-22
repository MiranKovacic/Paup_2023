using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paup_2023.Models
{
    public class KorisnikPrijava
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Lozinka { get; set; }
    }
}
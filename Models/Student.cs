﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paup_2023.Models
{
    public class Student
    {
        [Display(Name = "ID Studenta")] //Sadržaj HTML helper Label
        public int Id { get; set; }
        [Display(Name = "Ime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = 
        "{0} mora biti duljine minimalno {2} a maksimalno {1} znakova")]
        public string Ime { get; set; }
        [Display(Name = "Prezime")]
        [Required(ErrorMessage = "{0} je obavezno")]
        [StringLength(30, MinimumLength = 2, ErrorMessage =
        "{0} mora biti duljine minimalno {2} a maksimalno {1} znakova")]
        public string Prezime { get; set; }
        [Display(Name = "Spol")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public char Spol { get; set; }
        [Display(Name = "OIB")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [StringLength(11, MinimumLength = 11, ErrorMessage =
        "{0} mora biti duljine {1} znakova")]
        public string Oib { get; set; }
        [Display(Name = "Datum rođenja")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezan")]
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [Display(Name = "Godina studija")]
        public GodinaStudija GodinaStudija { get; set; }
        [Display(Name = "Redovan student")]
        public bool RedovanStudent { get; set; }
    }
}
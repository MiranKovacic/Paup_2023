﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paup_2023.Models
{
    [Table("studenti")]
    public class Student
    {
        [Key]
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

        public string PrezimeIme
        {
            get
            {
                return Prezime + " " + Ime;
            }
        }

        [Display(Name = "Spol")]
        [Required(ErrorMessage = "{0} je obavezan")]
        public string Spol { get; set; }
        [Display(Name = "Oib")]
        [Required(ErrorMessage = "{0} je obavezan")]
        [StringLength(11, MinimumLength = 11, ErrorMessage =
        "{0} mora biti duljine {1} znakova")]
        public string Oib { get; set; }
        [Column("datum_rodjenja")]
        [Display(Name = "Datum rođenja")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} je obavezan")]
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }
        [Column("godina_studija")]
        [Display(Name = "Godina studija")]
        [Range(1, 5, ErrorMessage ="Vrijednost {0} mora biti između {1} i {2}")]
        public GodinaStudija GodinaStudija { get; set; }
        [Column("redovni_student")]
        [Display(Name = "Redovan student")]
        public bool RedovanStudent { get; set; }
        [Column("broj_upisanih_kolegija")]
        [Display(Name = "Broj upisanih kolegija")]
        public int BrojUpisanihKolegija { get; set; }
        [Display(Name = "Smjer")]
        [Column("smjer_sifra")]
        [ForeignKey("UpisaniSmjer")]
        public string SifraSmjer { get; set; }
        public virtual Smjer UpisaniSmjer { get; set; }
        [Display(Name = "Fotografija")]
        [Column("slika")]
        public string SlikaPutanja { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
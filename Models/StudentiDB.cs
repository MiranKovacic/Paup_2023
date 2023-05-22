using System;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paup_2023.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BazaDbContext: DbContext
    {
        public DbSet<Student> PopisStudenata { get; set; }
        public DbSet<Smjer> PopisSmjerova { get; set; }
        public DbSet<Korisnik> PopisKorisnika { get; set; }
        public DbSet<Ovlast> PopisOvlasti { get; set; }
    }

    public class StudentiDB
    {
        //Lista Studenata
        private static List<Student> lista = new List<Student>();
        private static bool listaInicijalizirana = false;
        
        //Konstruktor se izvršava kod instanciranja klase, tj kad koristimo naredbu StudentDB studenti = new StudentiDB();
        public StudentiDB()
        {
            //samo kod privog instanicranja ove klase će vrijednost varijable 
            //listaInicijalizirana biti FALSE jer je varijabbla koa i lista static
            //što znači da svi objekti koriste istu listu
            // Punimo listu sa 4 zapisa:
            if (listaInicijalizirana == false)
            {
                //nakon svakog sljedećeg instanciranja će biti true
                //pa se donji blok naredbi neće više izvršavati
                //1. zapis
                lista.Add(new Student()
                {
                    Id = 1,
                    Prezime = "Ivić",
                    Ime = "Petar",
                    DatumRodjenja = new DateTime(1995, 10, 15),
                    Spol = "M",
                    GodinaStudija = GodinaStudija.Druga,
                    Oib = "12345678901",
                    RedovanStudent = true,
                });
                // 2. zapis
                lista.Add(new Student()
                {
                    Id = 2,
                    Prezime = "Mat",
                    Ime = "Milivoj",
                    DatumRodjenja = new DateTime(1958, 10, 02),
                    Spol = "M",
                    GodinaStudija = GodinaStudija.Peta,
                    Oib = "22255588813",
                    RedovanStudent = false,
                });
                // 3. zapisa
                lista.Add(new Student()
                {
                    Id = 3,
                    Prezime = "Bes",
                    Ime = "Marta",
                    DatumRodjenja = new DateTime(2000, 01, 02),
                    Spol = "Z",
                    GodinaStudija = GodinaStudija.Treca,
                    Oib = "12345678906",
                    RedovanStudent = true,
                });
                // 4. zapis
                lista.Add(new Student()
                {
                    Id = 4,
                    Prezime = "Preko",
                    Ime = "Mia",
                    DatumRodjenja = new DateTime(2002, 10, 15),
                    Spol = "Z",
                    GodinaStudija = GodinaStudija.Cetvrta,
                    Oib = "12945678901",
                    RedovanStudent = true,
                });
                listaInicijalizirana = true;
            }
        }

        //Metoda koja vraća listu studenata popunjenu u konstruktoru
        public List<Student> VratiListu()
        {
            return lista;
        }

        public void AzurirajStudenta(Student student)
        {
            // pronalatzimo lokaciju studenta u listi
            int studentIndex =
                VratiListu().FindIndex(x => x.Id == student.Id);
            // na tu lokaciju u listi stavljamo ažurirani objekt
            // s podaciam o studentu
            lista[studentIndex] = student;
        }
    }
}
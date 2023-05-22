using PagedList;
using Paup_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Paup_2023.Controllers
{
    public class StudentiController : Controller
    {
        BazaDbContext bazaPodataka = new BazaDbContext();
        // GET: Studenti
        public ActionResult Index()
        {
            ViewBag.Title = "Početna o studentima";
            ViewBag.Fakultet = "Međimursko veleučilište";
            return View();
        }
        
        public ActionResult Popis(string naziv, string spol, string smjer)
        {
            var smjeroviList = bazaPodataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            ViewBag.Smjerovi = smjeroviList;
            return View();
        }

        //int? definiramo da parametar id može biti nullabilan
        public ActionResult Detalji(int? id)
        {
            //provjeravamo ako parametar id nema vrijednost, tj ako nije difiniran
            if(!id.HasValue)
            {
                // preusmjerimo korisnika na akociju popis
                return RedirectToAction("Popis");
            }
            //instanciramo klasu StudentDB koja sadržava listu studenata
            //StudentiDB studentidb = new StudentiDB();
            /*
             * sa objekta studentidb pozivamo metodu VratiListu() koja nam vraća
             * listu studenata pomoću Lambada izraza FirstOrDefault( x=> x.Id == id)
             * dohvaćamo prvog elementa iz liste kojemu se vrijednost propertyja Id
             * podudara sa vrijednošću parametra id
             */
            //Student student = studentidb.VratiListu().FirstOrDefault(x => x.Id == id);
            Student student = bazaPodataka.PopisStudenata.
                FirstOrDefault(x => x.Id == id);
            //ako u listi nema studenta sa traženim Id-jem onda je varijabla student NULL
            if (student == null)
            {
                //u tom slučaju preusmjeravamo korisnika na akciju Popis
                return RedirectToAction("Popis");
            }
            //objekt student klase Student prosljeđujemo u View kao njegov model
            return View(student);
        }

        //int? definiramo da parametar ID može biti nullabilan
        //ovo je HTTP GET metoda
        //Primjer pozivanja /Student/Azuriraj/2
        public ActionResult Azuriraj(int? id)
        {
            Student student = null;
            if (!id.HasValue)
            {
                student = new Student();
                ViewBag.Title = "Kreiranje studenta";
                ViewBag.Novi = true;
            }
            else
            {
                student = bazaPodataka.PopisStudenata.
                    FirstOrDefault(x => x.Id == id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Title = "Ažuriranje potaka o studentu";
                ViewBag.Novi = false;
            }
            var smjerovi = bazaPodataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            smjerovi.Insert(0, new Smjer { Sifra = "", Naziv = "Nedefinirano" });
            ViewBag.Smjerovi = smjerovi;
            return View(student);
        }

        // Ovo je HTTP POST metoda
        [HttpPost]
        [ValidateAntiForgeryToken]
        // mehanizam koji nas štiti od cross site request forgery
        // tj, poziv post metoda izvan naše aplikacije
        public ActionResult Azuriraj(Student s)
        {
            if (!OIB.CheckOIB(s.Oib)) //provjera oib-a
            {
                ModelState.AddModelError("Oib", "Neispravan OIB");
            }
            // ModelState.Isvalid - provjera ispravnosti podataka
            //npr. ako je atribut int tipa a mi smo unjeli string
            //u to polje na formi, neće proći validaciju i 
            //preusmjerit će korisnika na stranicu za ažuriranje
            // ispisati grešku validacije
            if (ModelState.IsValid)
            {
                //ako je id u objektu s različit od 0 radi se o ažurianju studenta
                if (s.Id != 0)
                {
                    bazaPodataka.Entry(s).State = System.Data.Entity.EntityState.Modified;
                }
                // ako je id 0 znači da id još nije dodjeljen te znamo da moramo
                // dodati objekt s u listu studenata u bazi
                else
                {
                    bazaPodataka.PopisStudenata.Add(s);
                }
                bazaPodataka.SaveChanges();
                // preusmjeravanje na metodu koja vraća popis studenata
                return RedirectToAction("Popis");
            }
            if (s.Id == 0)
            {
                ViewBag.Title = "Kreiranje studenta";
                ViewBag.Novi = true;
            }
            else
            {
                ViewBag.Title = "Ažuriranje podataka o studentu";
                ViewBag.Novi = false;
            }
            // ako model nije ispravan onda ga vraćamo kljentu

            var smjerovi = bazaPodataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            smjerovi.Insert(0, new Smjer { Sifra = "", Naziv = "Nedefinirano" });
            ViewBag.Smjerovi = smjerovi;
            return View(s);
        }

        //Brisanje studenta
        //GET metoda
        // Primjer poziva: .../Brisi/2
        public ActionResult Brisi(int? id)
        {
            //ako id nije definiran preusmjerimo korisnika na popis studenata
            if (id == null)
            {
                return RedirectToAction("Popis");
            }
            Student s = bazaPodataka.PopisStudenata.
                FirstOrDefault(x => x.Id == id);
            //ako ne postoji student s tim id-em onda vraćamo HTTP status Not found
            if (s == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Potvrda brisanja studenta";
            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Brisanje studenta
        //POST metada koju poziva forma za potvrdu brisanja
        public ActionResult Brisi(int id)
        {
            Student s = bazaPodataka.PopisStudenata.
                FirstOrDefault(x => x.Id == id);
            if (s == null)
            {
                return HttpNotFound();
            }
            bazaPodataka.PopisStudenata.Remove(s);
            bazaPodataka.SaveChanges();
            return View("BrisiStatus");
        }

        public ActionResult PopisPartial(string naziv, string spol, string smjer, string sort, int? page)
        {
            ViewBag.Sortiranje = sort;
            ViewBag.NazivSort = String.IsNullOrEmpty(sort) ? "naziv_desc" : "";
            ViewBag.SmjerSort = sort == "smjer" ? "smjer_desc" : "smjer";
            ViewBag.Smjer = smjer;
            ViewBag.Naziv = naziv;
            ViewBag.Spol = spol;

            var studenti = bazaPodataka.PopisStudenata.ToList();
            var smjeroviList = bazaPodataka.PopisSmjerova.OrderBy(x => x.Naziv).ToList();
            ViewBag.Smjerovi = smjeroviList;
            //filtriranje
            if (!String.IsNullOrWhiteSpace(naziv))
                studenti = studenti.Where(x => x.PrezimeIme.ToUpper().Contains(naziv.ToUpper())).ToList();
            if (!String.IsNullOrWhiteSpace(spol))
                studenti = studenti.Where(x => x.Spol == spol).ToList();
            if (!String.IsNullOrWhiteSpace(smjer))
                studenti = studenti.Where(x => x.SifraSmjer == smjer).ToList();
            
            switch(sort)
            {
                case "naziv_desc":
                    studenti = studenti.OrderByDescending(s => s.PrezimeIme).ToList();
                    break;
                case "smjer":
                    studenti = studenti.OrderBy(s => s.SifraSmjer).ToList();
                    break;
                case "smjer_desc":
                    studenti = studenti.OrderByDescending(s => s.SifraSmjer).ToList();
                    break;
                default:
                    studenti = studenti.OrderBy(s => s.PrezimeIme).ToList();
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            return PartialView("_PartialPopis", studenti.ToPagedList(pageNumber, pageSize));
        }
    }
}
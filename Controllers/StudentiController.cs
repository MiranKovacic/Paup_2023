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
        // GET: Studenti
        public ActionResult Index()
        {
            ViewBag.Title = "Početna o studentima";
            ViewBag.Fakultet = "Međimursko veleučilište";
            return View();
        }
        
        public ActionResult Popis()
        {
            //Instanciramo klasu StudentiDB koaj sadržava
            // listu studenata
            StudentiDB studentidb = new StudentiDB();
            //objekt studentdb klase StudentDB prosljeđujemo
            // kao njegov model
            return View(studentidb);
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
            StudentiDB studentidb = new StudentiDB();
            /*
             * sa objekta studentidb pozivamo metodu VratiListu() koja nam vraća
             * listu studenata pomoću Lambada izraza FirstOrDefault( x=> x.Id == id)
             * dohvaćamo prvog elementa iz liste kojemu se vrijednost propertyja Id
             * podudara sa vrijednošću parametra id
             */
            Student student = studentidb.VratiListu().FirstOrDefault(x => x.Id == id);
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
            //provjerimo dal id ima vrijednost, tj ako on nije difiniran
            if (!id.HasValue)
            {
                //Vracamo HTTP status 400
                //Lista HTTP statusnih kodova: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //instanciramo klasu StudentDB koja sadržava listu studenata
            StudentiDB studentidb = new StudentiDB();
            /*
             * pomoću Lambada izraza FirstOrDefault( x=> x.Id == id)
             * dohvaćamo prvog elementa iz liste kojemu se vrijednost propertyja Id
             * podudara sa vrijednošću parametra id
             */
            Student student = studentidb.VratiListu().FirstOrDefault(x => x.Id == id);
            //ako u listi nema studenta sa traženim Id-jem onda je varijabla student NULL
            if (student == null)
            {
                //vratimo NotFound
                //return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                return HttpNotFound();
            }
            //objekt student klase Student prosljeđujemo u View kao njegov model
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
                // Ažuriranje liste podataka
                StudentiDB studentidb = new StudentiDB();
                studentidb.AzurirajStudenta(s);
                // preusmjeravanje na metodu koja vraća popis studenata
                return RedirectToAction("Popis");
            }
            // ako model nije ispravan onda ga vraćamo kljentu
            return View(s);
        }
    }
}
using Paup_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Paup_2023.Misc
{
    public class LogiraniKorisnik : IPrincipal
    {
        public string KorisnickoIme { get; set; }
        public string PrezimeIme { get; set; }
        public string Ovlast { get; set; }

        public IIdentity Identity { get; private set;}
        public bool IsInRole(string role)
        {
            if (Ovlast == role) return true;
            return false;
        }
        public LogiraniKorisnik(Korisnik k)
        {
            Identity = new GenericIdentity(k.KorisnickoIme);
            KorisnickoIme = k.KorisnickoIme;
            PrezimeIme = k.PrezimeIme;
            Ovlast = k.SifraOvlasti;
        }

        public LogiraniKorisnik(string korisnickoIme)
        {
            Identity = new GenericIdentity(korisnickoIme);
            KorisnickoIme = korisnickoIme;
        }
    }
}
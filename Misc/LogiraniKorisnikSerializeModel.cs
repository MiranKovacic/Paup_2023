using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paup_2023.Misc
{
    public class LogiraniKorisnikSerializeModel
    {
        public string KorisnickoIme { get; set; }
        public string PrezimeIme { get; set; }
        public string Ovlast { get; set; }

        internal void CopyFromUser(LogiraniKorisnik user)
        {
            KorisnickoIme = user.KorisnickoIme;
            PrezimeIme = user.PrezimeIme;
            Ovlast = user.Ovlast;
        }
    }
}
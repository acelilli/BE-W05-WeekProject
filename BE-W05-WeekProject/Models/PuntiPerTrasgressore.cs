using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_W05_WeekProject.Models
{
    public class PuntiPerTrasgressore
    {
        [Display(Name = "Id Anagrafica")]
        public int IdAnagrafica { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        [Display(Name = "Totale punti decurtati")]
        public int TotalePunti { get; set; }
    }
}
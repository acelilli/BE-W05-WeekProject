using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_W05_WeekProject.Models
{
    public class AnagraficaDieci
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [Display(Name = "Punti Decurtati")]
        public int PuntiDecurtati { get; set; }
        public decimal Importo { get; set; }

        [Display(Name = "Data Violazione")]
        [DataType(DataType.Date)]
        public DateTime DataViolazione { get; set; }
    }

}
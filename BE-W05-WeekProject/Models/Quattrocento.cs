using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_W05_WeekProject.Models
{
    public class Quattrocento
    {
        [Display(Name ="Id Verbale")]
        public int IdVerbale { get; set; }
        public decimal Importo { get; set; }
        [Display(Name = "Id Punti Decurtati")]
        public int PuntiDecurtati { get; set; }
        [Display(Name = "Data Violazione")]
        [DataType(DataType.Date)]
        public DateTime DataViolazione { get; set; }
        public string Descrizione { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
    }

}
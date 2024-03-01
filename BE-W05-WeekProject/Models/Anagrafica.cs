using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_W05_WeekProject.Models
{
    public class Anagrafica
    {
        [Display(Name = "Id Anagrafica")]
        public int IdAnagrafica { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il campo Nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il campo Cognome non può superare i 50 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il campo Indirizzo è obbligatorio.")]
        [StringLength(200, ErrorMessage = "Il campo Indirizzo non può superare i 200 caratteri.")]
        public string Indirizzo { get; set; }

        [Display(Name = "Città")]
        [Required(ErrorMessage = "Il campo Citta è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il campo Citta non può superare i 50 caratteri.")]
        public string Citta { get; set; }

        [Required(ErrorMessage = "Il campo CAP è obbligatorio.")]
        [StringLength(5, ErrorMessage = "Il campo CAP deve essere di 5 caratteri.")]
        public string CAP { get; set; }

        [Display(Name ="Codice Fiscale")]
        [Required(ErrorMessage = "Il campo CF è obbligatorio.")]
        [StringLength(16, ErrorMessage = "Il campo CF deve essere di 16 caratteri.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Il campo CF deve contenere solo lettere maiuscole e numeri.")]
        public string CF { get; set; }

        public Anagrafica() { }
        public Anagrafica( string nome, string cognome, string indirizzo, string citta, string cAP, string cF)
        {
            Nome = nome;
            Cognome = cognome;
            Indirizzo = indirizzo;
            Citta = citta;
            CAP = cAP;
            CF = cF;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BE_W05_WeekProject.Models
{
    public class Verbali
    {
        [Display(Name = "Id Verbale")]
        public int IdVerbale { get; set; }

        [Display(Name = "Id Anagrafica")]
        [Required(ErrorMessage = "È necessario specificare l'id dell'anagrafica.")]
        public int IdAnagrafica { get; set; }
        public List<SelectListItem> AnagraficaItems { get; set; } // Lista delle opzioni per l'anagrafica

        [Display(Name = "Id Violazione")]
        [Required(ErrorMessage = "È necessario specificare l'id della violazione.")]
        public int IdViolazione { get; set; }
        public List<SelectListItem> ViolazioneItems { get; set; } // Lista delle opzioni per la violazione

        [Display(Name = "Data Violazione")]
        [Required(ErrorMessage = "Il campo DataViolazione è obbligatorio.")]
        [DataType(DataType.Date)]
        public DateTime DataViolazione { get; set; }
        public string DataViolazioneBreve => DataViolazione.ToString("dd/MM/yyyy");

        [Display(Name = "Indirizzo violazione")]
        [Required(ErrorMessage = "Il campo Indirizzo violazione è obbligatorio.")]
        [StringLength(200, ErrorMessage = "Il campo IndirizzoViolazione non può superare i 200 caratteri.")]
        public string IndirizzoViolazione { get; set; }

        [Display(Name = "Nominativo agente")]
        [Required(ErrorMessage = "Il campo Nominativo agente è obbligatorio.")]
        [StringLength(100, ErrorMessage = "Il campo NominativoAgente non può superare i 100 caratteri.")]
        public string NominativoAgente { get; set; }

        [Display(Name = "Data trascrizione verbale")]
        [Required(ErrorMessage = "Il campo DataTrascrizioneVerbale è obbligatorio.")]
        [DataType(DataType.Date)]
        public DateTime DataTrascrizioneVerbale { get; set; }
        public string DataTrascrizioneBreve => DataTrascrizioneVerbale.ToString("dd/MM/yyyy");

        [Display(Name = "Importo")]
        [Range(0, double.MaxValue, ErrorMessage = "L'Importo deve essere maggiore o uguale a 0.")]
        public decimal Importo { get; set; }

        [Display(Name = "Punti decurtati")]
        [Required(ErrorMessage = "Il campo PuntiDecurtati è obbligatorio.")]
        [Range(0, short.MaxValue, ErrorMessage = "Il campo PuntiDecurtati deve essere maggiore o uguale a 0.")]
        public short PuntiDecurtati { get; set; }

        // Costruttore pubblico vuoto
        public Verbali()
        {
            // Inizializza le liste delle opzioni
            AnagraficaItems = new List<SelectListItem>();
            ViolazioneItems = new List<SelectListItem>();
        }

        // Costruttore con parametri per inizializzare le proprietà
        public Verbali(int idVerbale, int idAnagrafica, int idViolazione, DateTime dataViolazione, string indirizzoViolazione, string nominativoAgente, DateTime dataTrascrizioneVerbale, decimal importo, short puntiDecurtati)
        {
            IdVerbale = idVerbale;
            IdAnagrafica = idAnagrafica;
            IdViolazione = idViolazione;
            DataViolazione = dataViolazione;
            IndirizzoViolazione = indirizzoViolazione;
            NominativoAgente = nominativoAgente;
            DataTrascrizioneVerbale = dataTrascrizioneVerbale;
            Importo = importo;
            PuntiDecurtati = puntiDecurtati;

            // Inizializza le liste delle opzioni
            AnagraficaItems = new List<SelectListItem>();
            ViolazioneItems = new List<SelectListItem>();
        }
    }
}

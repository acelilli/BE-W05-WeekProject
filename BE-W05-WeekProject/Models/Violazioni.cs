using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE_W05_WeekProject.Models
{
    public class Violazioni
    {

        public int IdViolazione { get; set; }

        [Required(ErrorMessage = "Il campo Descrizione è obbligatorio.")]
        public string Descrizione { get; set; }

        // Lista delle opzioni per il menu a tendina
        public List<string> OpzioniDescrizione { get; set; }

        // Costruttore pubblico vuoto
        public Violazioni()
        {
        }

        // Costruttore con parametri per inizializzare le proprietà
        public Violazioni(int idViolazione, string descrizione)
        {
            IdViolazione = idViolazione;
            Descrizione = descrizione;
        }
    }
}
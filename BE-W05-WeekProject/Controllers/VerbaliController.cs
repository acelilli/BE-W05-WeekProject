using BE_W05_WeekProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BE_W05_WeekProject.Controllers
{
    public class VerbaliController : Controller
    {
        ////GET: Verbali
        public ActionResult Index()
        {
            return View();
        }
        //// NEL CONTROLLER VERBALI METTO SOLO LE OPERAZIONI RELATIVE AI VERBALI /// 
        ///
        ///
        //// METODI PER CREARE IL FORM DA COMPILARE PER CREARE UN NUOVO VERBALE
        //// GET -> Prendo le liste per fare il dropdown per scegliere le anagrafiche e per scegliere le violazioni
        [HttpGet]
        public ActionResult NuovoVerbale()
        {

            // Ottenimento delle liste per i menu a tendina
            var anagraficaList = GetAnagraficaList();
            var violazioneList = GetViolazioniList();

            // Costruzione del modello con le liste popolate
            var model = new Verbali
            {
                AnagraficaItems = anagraficaList,
                ViolazioneItems = violazioneList
            };

            // Passaggio del modello alla vista
            return View(model);
        }
        //// POST -> Per mandare i dati al DB
        [HttpPost]
        public ActionResult NuovoVerbale(Verbali verbale)
        {
            // Se il modello non è valido, ritorna alla vista NuovoVerbale con il modello per mostrare gli errori
            if (!ModelState.IsValid)
            {
                // Ottenimento delle liste per i menu a tendina
                var anagraficaList = GetAnagraficaList();
                var violazioniList = GetViolazioniList();

                // Aggiunta delle liste al modello
                verbale.AnagraficaItems = anagraficaList;
                verbale.ViolazioneItems = violazioniList;

                // Ritorno della vista con il modello
                return View(verbale);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO VERBALE (IdAnagrafica, IdViolazione, DataViolazione, IndirizzoViolazione, NominativoAgente, DataTrascrizioneVerbale, Importo, PuntiDecurtati)
                            VALUES (@IdAnagrafica, @IdViolazione, @DataViolazione, @IndirizzoViolazione, @NominativoAgente, @DataTrascrizioneVerbale, @Importo, @PuntiDecurtati)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdAnagrafica", verbale.IdAnagrafica);
                        command.Parameters.AddWithValue("@IdViolazione", verbale.IdViolazione);
                        command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                        command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                        command.Parameters.AddWithValue("@NominativoAgente", verbale.NominativoAgente);
                        command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                        command.Parameters.AddWithValue("@Importo", verbale.Importo);
                        command.Parameters.AddWithValue("@PuntiDecurtati", verbale.PuntiDecurtati);

                        // Esecuzione comando = inserimento del verbale nel database
                        command.ExecuteNonQuery();
                    }

                    //ViewBag.SuccessMessage = "Verbale inserito con successo!";
                    TempData["SuccessMessageVerbale"] = "Verbale inserito con successo!";
                }
                catch (Exception ex)
                {                    
                    System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
                    return View(verbale);
                }
                finally
                {
                    conn.Close();
                }
            }
            return View(verbale);
        }
        ///
        /// 
        ///// METODI PER RECUPERARE LE LISTE PER: 
        // 1. ANAGRAFICA
        // 2. VIOLAZIONI
        ///
        // 1. Metodo per creare la lista delle opzioni per il menu dropdown  -> Ritorna la lista delle anagrafiche
        private List<SelectListItem> GetAnagraficaList()
        {
            var anagraficaList = new List<SelectListItem>();

            // Connessione al database e recupero delle opzioni per l'anagrafica
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IdAnagrafica, Nome, Cognome FROM ANAGRAFICA";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["IdAnagrafica"].ToString(),
                            Text = $"{reader["Nome"]} {reader["Cognome"]}"
                        };
                        anagraficaList.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore nella richiesta SQL ad ANAGRAFICA" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return anagraficaList;
        }

        //2. Metodo per creare la lista delle violazioni per il menù dropdown -> Ritorna la lista delle violazioni
        private List<SelectListItem> GetViolazioniList()
        {
            var violazioniList = new List<SelectListItem>();

            // Connessione al database e recupero delle opzioni per la violazione
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT IdViolazione, Descrizione FROM VIOLAZIONE";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["IdViolazione"].ToString(),
                            Text = reader["Descrizione"].ToString()
                        };
                        violazioniList.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore nella richiesta SQL ad VIOLAZIONI" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return violazioniList;
        }
    }
}
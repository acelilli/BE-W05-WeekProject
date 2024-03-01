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
    public class AnagraficaController : Controller
    {
        // GET: Anagrafica STAMPA ANAGRAFICA INDEX
        public ActionResult Index()
        {
            return View();
        }
        /// OPERAZIONI RELATIVE ALL'ANAGRAFICA ///
        /// ////// METODI PER CREARE IL FORM DA COMPILARE PER CREARE UNA NUOVA ANAGRAFICA
        /// 
        [HttpGet]
        public ActionResult NuovaAnagrafica()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuovaAnagrafica(Anagrafica anagrafica)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            SqlConnection   conn = new SqlConnection(connectionString);
            try
            {
                // Salvataggio dei dati nel database senza verificare la validità del modello

                conn.Open();
                string query = @"INSERT INTO ANAGRAFICA (Nome, Cognome, Indirizzo, Citta, CAP, CF)
                VALUES (@Nome, @Cognome, @Indirizzo, @Citta, @CAP, @CF)";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                    command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                    command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                    command.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                    command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                    command.Parameters.AddWithValue("@CF", anagrafica.CF);

                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessageAagrafica"] = "Anagrafica inserita con successo!";

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Si è verificato un errore durante il salvataggio dell'anagrafica.");
                System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
                return View(anagrafica);
            }
            finally
            {
              conn.Close();
            }
            return View(anagrafica);
        }


    }
}
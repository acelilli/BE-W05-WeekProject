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
        ////GET: Anagrafica
        public ActionResult Index()
        {
            return View();
        }
        //// NEL CONTROLLER ANAGRAFICA METTO SOLO LE OPERAZIONI RELATIVE AI VERBALI /// 
        ///
        ///
        //// METODI PER CREARE IL FORM DA COMPILARE PER AGGIUNGERE UN NUOVO TRASGRESSORE
        //// GET 
        [HttpGet]
        public ActionResult NuovaAnagrafica()
        {
            return View();
        }
        //// POST -> Per mandare i dati al DB
        [HttpPost]
        public ActionResult NuovaAnagrafica(Anagrafica anagrafica)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
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

                    // Esecuzione comando = inserimento dell'anagrafica nel database
                    command.ExecuteNonQuery();
                }

                TempData["SuccessMessageAagrafica"] = "Anagrafica inserita con successo!";

            }
            catch (Exception ex)
            {                
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
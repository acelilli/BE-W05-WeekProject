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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Connessione al database
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            List<Verbali> verbaliList = new List<Verbali>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM VERBALE";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Verbali verbale = new Verbali()
                        {
                            IdVerbale = Convert.ToInt32(reader["IdVerbale"]),
                            IdAnagrafica = Convert.ToInt32(reader["IdAnagrafica"]),
                            IdViolazione = Convert.ToInt32(reader["IdViolazione"]),
                            DataViolazione = Convert.ToDateTime(reader["DataViolazione"]),
                            IndirizzoViolazione = reader["IndirizzoViolazione"].ToString(),
                            NominativoAgente = reader["NominativoAgente"].ToString(),
                            DataTrascrizioneVerbale = Convert.ToDateTime(reader["DataTrascrizioneVerbale"]),
                            Importo = Convert.ToDecimal(reader["Importo"]),
                            PuntiDecurtati = Convert.ToInt16(reader["PuntiDecurtati"])
                        };
                        verbaliList.Add(verbale);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
                }
            }

            return View(verbaliList);
        }

        public ActionResult ArchivioAnagrafica()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            List<Anagrafica> anagraficaList = new List<Anagrafica>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM ANAGRAFICA";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Anagrafica anagrafica = new Anagrafica()
                        {
                            IdAnagrafica = Convert.ToInt32(reader["IdAnagrafica"]),
                            Nome = reader["Nome"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Indirizzo = reader["Indirizzo"].ToString(),
                            Citta = reader["Citta"].ToString(),
                            CAP = reader["CAP"].ToString(),
                            CF = reader["CF"].ToString()
                        };
                        anagraficaList.Add(anagrafica);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
                }
            }

            return View(anagraficaList);
        }

        public ActionResult ArchivioViolazioni()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            List<Violazioni> violazioniList = new List<Violazioni>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM VIOLAZIONE";
                    SqlCommand command = new SqlCommand(query, conn);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Violazioni violazione = new Violazioni()
                        {
                            IdViolazione = Convert.ToInt32(reader["IdViolazione"]),
                            Descrizione = reader["Descrizione"].ToString()
                        };
                        violazioniList.Add(violazione);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
                }
            }

            return View(violazioniList);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using BE_W05_WeekProject.Models;
using System.Web.UI.WebControls;

namespace BE_W05_WeekProject.Controllers
{
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        
        ////In index: Solo pulsanti che rimandano alle azioni
        public ActionResult Index()
        {
            return View();
        }
        //// Visualizzare verbali per trasgressore:
        /// 1. Classe per rappresentare i dati dei verbali per trasgressore -> In models
        /// 2. Metodo per visualizzare i verbali raggruppati per trasgressore (id + nome e cognome)
        /// LEFT JOIN delle tabelle tramite ID ANAGRAFICA
        /// No parametri, ritorna la vista dei raggruppamenti
        public ActionResult VerbaliPerTrasgressorePrint()
        {
            List<VerbaliPerTrasgressore> verbaliPerTrasgressore = new List<VerbaliPerTrasgressore>();

            // Connessione al database
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = @"
            SELECT A.IdAnagrafica, A.Nome, A.Cognome, COUNT(V.IdVerbale) AS TotaleVerbali
            FROM ANAGRAFICA A
            LEFT JOIN VERBALE V ON A.IdAnagrafica = V.IdAnagrafica
            GROUP BY A.IdAnagrafica, A.Nome, A.Cognome";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var idAnagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                    var nome = reader["Nome"].ToString();
                    var cognome = reader["Cognome"].ToString();
                    var totaleVerbali = Convert.ToInt32(reader["TotaleVerbali"]);
                    verbaliPerTrasgressore.Add(new VerbaliPerTrasgressore { IdAnagrafica = idAnagrafica, Nome = nome, Cognome = cognome, TotaleVerbali = totaleVerbali });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(verbaliPerTrasgressore);
        }
        ////Visualizzare tabella PUNTI DECURTATI PER TRASGRESSORE:
        /// 1. Classe per rappresentare i punti decurtati  per trasgressore -> in models
        /// 2. Metodo per visualizzare i verbali raggruppati per trasgressore (id + nome e cognome)
        /// LEFT JOIN delle tabelle tramite ID ANAGRAFICA
        /// No parametri, ritorna la vista dei raggruppamenti
        public ActionResult PuntiPerTrasgressorePrint()
        {
            List<PuntiPerTrasgressore> puntiPerTrasgressore = new List<PuntiPerTrasgressore>();

            // Connessione al database
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                //Partire da VERBALE, raccogliere i record che hanno lo stesso idAnagrafica 
                // questi record, sommare i punti. 
                // Dopodiché verificare a quali IdAnagrafica corrisponde ciascun record di ANAGRAFICA
                string query = @"SELECT A.IdAnagrafica, A.Nome, A.Cognome, SUM(V.PuntiDecurtati) AS TotalePunti
                FROM ANAGRAFICA A
                INNER JOIN (
                SELECT IdAnagrafica, SUM(PuntiDecurtati) AS PuntiDecurtati
                FROM VERBALE
                GROUP BY IdAnagrafica
                ) AS V ON A.IdAnagrafica = V.IdAnagrafica
                GROUP BY A.IdAnagrafica, A.Nome, A.Cognome
                ORDER BY TotalePunti DESC";


                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var idAnagrafica = Convert.ToInt32(reader["IdAnagrafica"]);
                    var nome = reader["Nome"].ToString();
                    var cognome = reader["Cognome"].ToString();
                    var totalePunti = Convert.ToInt32(reader["TotalePunti"]);
                    puntiPerTrasgressore.Add(new PuntiPerTrasgressore { IdAnagrafica = idAnagrafica, Nome = nome, Cognome = cognome, TotalePunti = totalePunti });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(puntiPerTrasgressore);
        }
        // Visualizzare Nome, Cognome, decurtamento punti e importo per le violazioni che superano i 10 PUNTI:
        /// 1. Classe per costuire il modello x le trasgressioni con 10+ punti decurtati -> in models
        /// 2. Metodo per visualizzare i nominativi, data, importo e pt x le trasgressioni dove i punti 10+
        public ActionResult AnagraficaDieci()
        {
            List<AnagraficaDieci> anagraficheDieci = new List<AnagraficaDieci>();

            // Connessione al database
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = @"
                SELECT A.Nome, A.Cognome, V.PuntiDecurtati, V.Importo, V.DataViolazione
                FROM ANAGRAFICA A
                INNER JOIN VERBALE V ON A.IdAnagrafica = V.IdAnagrafica
                WHERE V.PuntiDecurtati > 10
                ORDER BY V.PuntiDecurtati DESC""";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var nome = reader["Nome"].ToString();
                    var cognome = reader["Cognome"].ToString();
                    var puntiDecurtati = Convert.ToInt32(reader["PuntiDecurtati"]);
                    var importo = Convert.ToDecimal(reader["Importo"]);
                    var dataViolazione = Convert.ToDateTime(reader["DataViolazione"]);

                    anagraficheDieci.Add(new AnagraficaDieci
                    {
                        Nome = nome,
                        Cognome = cognome,
                        PuntiDecurtati = puntiDecurtati,
                        Importo = importo,
                        DataViolazione = dataViolazione
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            // Visto che al momento non ho violazioni con 10+ punti stampo un messaggio
            if (anagraficheDieci.Count == 0)
            {
                ViewBag.Message = "Nessuna violazione con 10+ punti decurtati trovata.";
            }

            return View(anagraficheDieci);
        }
        ////
        //// Visualizzare le violazioni cui IMPORTO è maggiore di 400 EURO
        /// 1. Classe per costuire il modello x le trasgressioni con importi superiori a 400 -> in models
        /// 2. Metodo per visualizzare i nominativi e le trasgressioni dove l'importo è +400
        public ActionResult QuattrocentoPrint()
        {
            List<Quattrocento> quattrocentoList = new List<Quattrocento>();

            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaMinicipale"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = @"
            SELECT V.IdVerbale, V.Importo, V.PuntiDecurtati, V.DataViolazione, VI.Descrizione, A.Nome, A.Cognome
            FROM VERBALE V
            INNER JOIN VIOLAZIONE VI ON V.IdViolazione = VI.IdViolazione
            INNER JOIN ANAGRAFICA A ON V.IdAnagrafica = A.IdAnagrafica
            WHERE V.Importo > 400
            ORDER BY V.Importo DESC";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var idVerbale = Convert.ToInt32(reader["IdVerbale"]);
                    var importo = Convert.ToDecimal(reader["Importo"]);
                    var puntiDecurtati = Convert.ToInt32(reader["PuntiDecurtati"]);
                    var dataViolazione = Convert.ToDateTime(reader["DataViolazione"]);
                    var descrizione = reader["Descrizione"].ToString();
                    var nome = reader["Nome"].ToString();
                    var cognome = reader["Cognome"].ToString();

                    quattrocentoList.Add(new Quattrocento
                    {
                        IdVerbale = idVerbale,
                        Importo = importo,
                        PuntiDecurtati = puntiDecurtati,
                        DataViolazione = dataViolazione,
                        Descrizione = descrizione,
                        Nome = nome,
                        Cognome = cognome
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return View(quattrocentoList);
        }
    }
}
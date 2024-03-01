using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using BE_W05_WeekProject.Models;

namespace BE_W05_WeekProject.Controllers
{
    public class AnalyticsController : Controller
    {
        // GET: Analytics
        public ActionResult Index()
        {
            return View();
        }
        /// Verbali per trasgressore:
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
        /// Punti decurtati per trasgressore:
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

    }
}
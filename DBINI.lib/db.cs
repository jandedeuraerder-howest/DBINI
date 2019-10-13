using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DBINI.lib
{
    public class db
    {
        public static bool dbAlive(string authenticatie,string server, string poort, string catalogus, string gebruiker, string paswoord)
        {
            //string constring = "Data Source=88.151.242.22,14433;Initial Catalog=altws;User ID=dbIVOConnector;Password=45#jKLpo?7PRux9";
            string constring;
            if (authenticatie.ToLower() == "sql")
            {
                if (poort == "1433")
                    constring = "Data Source=" + server + ";Initial Catalog=" + catalogus + ";User ID=" + gebruiker + ";Password=" + paswoord;
                else
                    constring = "Data Source=" + server + "," + poort + ";Initial Catalog=" + catalogus + ";User ID=" + gebruiker + ";Password=" + paswoord;
            }
            else
            {
                if (poort == "1433")
                    constring = "Data Source=" + server + ";Initial Catalog=" + catalogus + ";Integrated Security=true";
                else
                    constring = "Data Source=" + server + "," + poort + ";Initial Catalog=" + catalogus + ";Integrated Security=true";
            }
            using (SqlConnection connectie = new SqlConnection(constring))
            {
                try
                {
                    connectie.Open();
                    return true;
                }
                catch (Exception fout)
                {
                    return false;
                }
            }
        }
        public static bool dbAlive()
        {
            string constring = ini.ReadIni();
            if (constring == "") return false;
            using (SqlConnection connectie = new SqlConnection(constring))
            {
                try
                {
                    connectie.Open();
                    return true;
                }
                catch (Exception fout)
                {
                    return false;
                }
            }
        }

        private static string maakConnectieString()
        {
            return ini.ReadIni(); 
        }

        public static DataTable ExecuteSelect(string sqlInstructie)
        {
            string constring = maakConnectieString();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sqlInstructie, constring);
            ds.Clear();
            try
            {
                da.Fill(ds);
            }
            catch(Exception fout)
            {
                string foutmelding = fout.Message;
                return null;
            }
            return ds.Tables[0];
        }

    }
}

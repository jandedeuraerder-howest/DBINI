using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DBINI.lib
{
    public class ini
    {

        public static bool WriteIni(string authenticatie, string server, string poort, string catalogus, string gebruiker, string paswoord)
        {
            try
            {
                string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
                string inifile = appBaseDir + "sql.ini";
                if (File.Exists(inifile))
                    File.Delete(inifile);

                FileStream fs = new FileStream(inifile, FileMode.Append, FileAccess.Write);
                StreamWriter sw =  new StreamWriter(fs);
                
                sw.WriteLine("authenticatie=" + authenticatie);
                sw.WriteLine("server=" + server);
                sw.WriteLine("poort=" + poort);
                sw.WriteLine("catalogus=" + catalogus);
                sw.WriteLine("gebruiker=" + gebruiker);
                sw.WriteLine("paswoord=" + paswoord);
                sw.Close();


                return true;
            }
            catch(Exception fout)
            {
                string dummy = fout.Message;
                return false;
            }
        }
        public static string ReadIni()
        {
            try
            {
                string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
                string inifile = appBaseDir + "sql.ini";
                if (!File.Exists(inifile))
                    return "";

                StreamReader inhoud = new System.IO.StreamReader(inifile);
                StringBuilder sb = new StringBuilder();

                string regel;
                string[] delen;
                string authenticatie;

                //eerste lijn = authenticatiemethode
                regel = inhoud.ReadLine();
                delen = regel.Split('=');
                if (delen[0].ToString().ToLower() != "authenticatie")
                    return "";
                if (delen[1] == "") return "";
                authenticatie = delen[1].ToLower();

                // tweede lijn = server
                regel = inhoud.ReadLine();
                delen = regel.Split('=');
                if (delen[0].ToString().ToLower() != "server")
                    return "";
                if (delen[1] == "") return "";
                sb.Append("Data Source=");
                sb.Append(delen[1]);

                // derde lijn = poort
                regel = inhoud.ReadLine();
                delen = regel.Split('=');
                if (delen[0].ToString().ToLower() != "poort")
                    return "";
                if (delen[1] == "") return "";
                if (delen[1] != "1433")
                {
                    sb.Append(",");
                    sb.Append(delen[1]);
                }
                sb.Append(";");

                // vierde lijn = catalogus
                regel = inhoud.ReadLine();
                delen = regel.Split('=');
                if (delen[0].ToString().ToLower() != "catalogus")
                    return "";
                if (delen[1] == "") return "";
                sb.Append("Initial Catalog=");
                sb.Append(delen[1]);
                sb.Append(";");


                if (authenticatie == "sql")
                {
                    // vijfde lijn = gebruikersnaam, enkel nodig voor sqlserver authenticatie
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "gebruiker")
                        return "";
                    if (delen[1] == "") return "";
                    sb.Append("User ID=");
                    sb.Append(delen[1]);
                    sb.Append(";");

                    // laatste lijn = paswoord, enkel nodig voor sqlserver authenticatie
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "paswoord")
                        return "";
                    if (delen[1] == "") return "";
                    sb.Append("Password=");
                    sb.Append(delen[1]);
                }
                else
                {
                    sb.Append("Integrated Security=true");
                }
                inhoud.Close();
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static void GeefWaarden(out string authenticatie, out string server, out string poort, out string catalogus, out string gebruiker, out string paswoord)
        {
            authenticatie = "";
            server = "";
            poort = "";
            catalogus = "";
            gebruiker = "";
            paswoord = "";
            try
            {
                string appBaseDir = System.AppDomain.CurrentDomain.BaseDirectory;
                string inifile = appBaseDir + "sql.ini";
                if (File.Exists(inifile))
                {
                    StreamReader inhoud = new System.IO.StreamReader(inifile);
                    string regel;
                    string[] delen;
                    //eerste lijn = authenticatiemethode
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "authenticatie")
                        return;
                    if (delen[1] == "")
                        return;
                    authenticatie = delen[1].ToLower();

                    // tweede lijn = server
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "server")
                        return;
                    if (delen[1] == "")
                        return;
                    server = delen[1];

                    // derde lijn = poort
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "poort")
                        return;
                    if (delen[1] == "")
                        return;
                    poort = delen[1];

                    // vierde lijn = catalogus
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "catalogus")
                        return;
                    if (delen[1] == "")
                        return;
                    catalogus = delen[1];

                    // vijfde lijn = gebruikersnaam, enkel nodig voor sqlserver authenticatie
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "gebruiker")
                        return;
                    if (delen[1] == "")
                        return;
                    gebruiker = delen[1];

                    // laatste lijn = paswoord, enkel nodig voor sqlserver authenticatie
                    regel = inhoud.ReadLine();
                    delen = regel.Split('=');
                    if (delen[0].ToString().ToLower() != "paswoord")
                        return;
                    if (delen[1] == "")
                        return;
                    paswoord = delen[1];
                    inhoud.Close();



                }
            }
            catch
            {
                return;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DBINI.lib;

namespace DBINI.wpf
{
    /// <summary>
    /// Interaction logic for winPopUp.xaml
    /// </summary>
    public partial class winPopUp : Window
    {
        // sluitknop bovenaan window verbergen
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            rdbSQL.IsChecked = false;
            rdbWindows.IsChecked = true;
            lblGebruikersnaam.Visibility = Visibility.Hidden;
            lblPaswoord.Visibility = Visibility.Hidden;
            txtGebruikersnaam.Visibility = Visibility.Hidden;
            txtPaswoord.Visibility = Visibility.Hidden;
        }

        // ======================


        public winPopUp()
        {
            InitializeComponent();
        }

        private void BtnStandaardPoort_Click(object sender, RoutedEventArgs e)
        {
            txtPoortnr.Text = "1433";
        }

        private void BtnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            string authenticatie = "windows";
            if (rdbSQL.IsChecked==true) authenticatie = "sql";
            string server = txtServer.Text;
            string poort = txtPoortnr.Text;
            string catalogus = txtCatalgus.Text;
            string gebruikersnaam = "";
            string paswoord = "";
            if (rdbSQL.IsChecked == true)
            {
                gebruikersnaam = txtGebruikersnaam.Text;
                paswoord = txtPaswoord.Password.ToString();
            }

            if(db.dbAlive(authenticatie, server,poort,catalogus,gebruikersnaam,paswoord))
            {
                if(ini.WriteIni(authenticatie, server, poort, catalogus, gebruikersnaam, paswoord))
                {


                    this.Close();
                }
                else
                {
                    txtServer.Focus();
                    return;

                }

            }
            else
            {
                txtServer.Focus();
                return;
            }
        }

        private void BtnEndApp_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void RdbSQL_Click(object sender, RoutedEventArgs e)
        {
            lblGebruikersnaam.Visibility = Visibility.Visible;
            lblPaswoord.Visibility = Visibility.Visible;
            txtGebruikersnaam.Visibility = Visibility.Visible;
            txtPaswoord.Visibility = Visibility.Visible;

        }

        private void RdbWindows_Checked(object sender, RoutedEventArgs e)
        {
            lblGebruikersnaam.Visibility = Visibility.Hidden;
            lblPaswoord.Visibility = Visibility.Hidden;
            txtGebruikersnaam.Visibility = Visibility.Hidden;
            txtPaswoord.Visibility = Visibility.Hidden;

        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBINI.lib;
using System.Data;



namespace DBINI.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if(!db.dbAlive())
            {
                this.WindowState = WindowState.Maximized;
                winPopUp frm = new winPopUp();
                frm.Owner = this;
                frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                frm.ShowDialog();
            }

            string sql;
            sql = "select * from BOEKEN";
            DataTable dt = db.ExecuteSelect(sql);
            if(dt!=null)
                grdTest.DataContext = dt.DefaultView;


        }

        private void MnuINI_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            winPopUp frm = new winPopUp();
            frm.Owner = this;
            frm.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            string authenticatie, server, poort, catalogus, gebruiker, paswoord;
            ini.GeefWaarden(out authenticatie, out server, out poort, out catalogus, out gebruiker, out paswoord);
            if (authenticatie.ToLower() == "windows" || authenticatie == "")
            {
                frm.rdbSQL.IsChecked = false;
                frm.rdbWindows.IsChecked = true;
            }
            else
            {
                frm.rdbSQL.IsChecked = true;
                frm.rdbWindows.IsChecked = false;
            }
            frm.txtServer.Text = server;
            frm.txtPoortnr.Text = poort;
            frm.txtCatalgus.Text = catalogus;
            frm.txtGebruikersnaam.Text = gebruiker;
            frm.txtPaswoord.Password = paswoord;
            frm.ShowDialog();

        }
    }
}

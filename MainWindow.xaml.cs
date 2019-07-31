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
using MySql.Data.MySqlClient;


namespace _122IT_CW2_APP_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string conn;
        private MySqlConnection connect;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void db_connection()
        {
            try
            {
                conn = "Server=**; Database=**; Uid=**; Pwd=;"; //Check SRV setting, DB Name, UID and PWD. Also, this is not secure...
            }
            catch (MySqlException e)
            {
                throw;
            }
        }
        private bool validate_login(string usrnm, string pwd)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from usrs where username=@usrmn and password=@pwd";
            cmd.Parameters.AddWithValue("@usrnm", usrnm);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }

        private void BTN_LGIN_Click(object sender, RoutedEventArgs e)
        {
            string usrnm = USRNM_INPUT.Text;
            string pwd = PSWD_INPUT.Text;
            if (usrnm == ""||pwd == "")
            {
                MessageBox.Show("Expty Fields");
                return;
            }
            bool r = validate_login(usrnm, pwd);
            if (r)
                MessageBox.Show("Correct Login Details");
            else
                MessageBox.Show("Incorrect Login Details");


        }
    }
}

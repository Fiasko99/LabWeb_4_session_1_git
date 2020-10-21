using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LabWeb_4_session_1
{
    public partial class AuthForm : Form {
        readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=session1_rasulov;";
        string query;
        public AuthForm() {
            InitializeComponent();
        }

        private void Auth_Click(object sender, EventArgs e) {
            // Change the username, password and database according to your needs
            // You can ignore the database option if you want to access all of them.
            // 127.0.0.1 stands for localhost and the default port to connect.
            // Your query,
            query = "SELECT ID FROM `users` where `Email`='" + login.Text + "' AND `Password`='" + password.Text + "'";
            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection)
            {
                CommandTimeout = 60
            };
            MySqlDataReader reader;

            // Let's do it !
            try {
                // Open the database
                databaseConnection.Open();

                // Execute the query
                reader = commandDatabase.ExecuteReader();

                // All succesfully executed, now do something

                // IMPORTANT : 
                // If your query returns result, use the following processor :

                
                if (reader.HasRows) {
                    databaseConnection.Close();
                    // Finally close the connection
                    ActiveTurn();
                }
                else
                {
                    databaseConnection.Close();
                }

            }
            catch (Exception ex) {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }
        
        void ActiveTurn()
        {
            query = "UPDATE `users` SET `Active` = '1' WHERE `users`.`Email` ='" + login.Text + "'";

            // Prepare the connection
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection)
            {
                CommandTimeout = 60
            };
            MySqlDataReader reader;

            try
            {
                // Open the database
                databaseConnection.Open();
                // Execute the query
                reader = commandDatabase.ExecuteReader();
                // All succesfully executed, now do something
                // IMPORTANT : 
                // If your query returns result, use the following processor 
                databaseConnection.Close();
                Main newForm = new Main();
                newForm.FormClosed += Page_FormClosed;
                newForm.Show();
                this.Hide();

            }
            catch (Exception ex)
            {
                // Show any error message.
                MessageBox.Show(ex.Message);
            }
        }

        private void Page_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); //отображение 1-й формы после закрытия 2-й
            this.Close();
        }
    }
}

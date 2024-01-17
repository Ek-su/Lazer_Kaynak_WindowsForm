using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lazer_Kaynak_WindowsForm
{
    public partial class Form3 : Form
    {
        private const string connectionString = "Host=localhost; Database=LazerKaynakDB;Username=postgres;Password=Esk5877*;Pooling=true";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //if (!UserExistsInDatabase())
            //{
            //    MessageBox.Show("Sistemde kayıtlı kullanıcı bulunamadı. Lütfen kayıt olun.");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (ValidateUser(username, password)) // kullanıcı doğrulandıysa form2 yi aç
            {
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }
        }
        private bool ValidateUser(string username, string password) // bu metod veritabanında kulllanıcı adı ve şifre kontrolü yapar.
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM \"Company_Login_Tbl\" WHERE \"Username\" = @Username AND \"Password\" = @Password";
                // count(*) sorgusu her zaman bir sayı döndürür.


                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int result = Convert.ToInt32(command.ExecuteScalar());

                    return result > 0;
                }
            }
        }

        //private bool UserExistsInDatabase()
        //{
        //    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string sql = "SELECT COUNT(*) FROM \"Company_Login_Tbl\"";

        //        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
        //        {
        //            int result = Convert.ToInt32(command.ExecuteScalar());

        //            return result > 0;
        //        }
        //    }
        //}
    }
}

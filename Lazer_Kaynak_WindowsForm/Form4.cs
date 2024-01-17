using Lazer_Kaynak_WindowsForm.Models;
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
    public partial class Form4 : Form
    {
        private const string connectionString = "Host=localhost; Database=LazerKaynakDB;Username=postgres;Password=Esk5877*;Pooling=true";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // MachineName'leri çekip ComboBox'a ekle
                    string machineNameSql = "SELECT DISTINCT \"MachineName\",\"DeviceNo\" FROM \"MachineList_Tbl\"";
                    using (NpgsqlCommand machineNameCommand = new NpgsqlCommand(machineNameSql, connection))
                    {
                        using (NpgsqlDataReader reader = machineNameCommand.ExecuteReader())
                        {
                            List<MachineListItem> items = new List<MachineListItem>();
                            while (reader.Read())
                            {
                                MachineListItem item = new MachineListItem();
                                item.Text=reader["MachineName"].ToString();
                                item.Value=reader["DeviceNo"].ToString();
                                items.Add(item);


                            }
                            comboBox1.DataSource=items;
                            comboBox1.DisplayMember="Text";
                            comboBox1.ValueMember="Value";
                        }
                    }
                    //combobox.ıtems.selectedvalue = deviceno
                    // Diğer verileri çek
                    string pieceCountSql = "SELECT * FROM \"Piece_Of_Count_Tbl\"";
                    using (NpgsqlCommand command = new NpgsqlCommand(pieceCountSql, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["DeviceName"].ToString());
                                item.SubItems.Add(reader["PieceOfCount"].ToString());
                                item.SubItems.Add(reader["Date"].ToString());

                                listView1.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}

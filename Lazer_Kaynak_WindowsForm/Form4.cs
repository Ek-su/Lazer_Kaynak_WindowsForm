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
                    MachineListItem selectedMachine = (MachineListItem)comboBox1.SelectedItem;
                    int selectedDeviceNo = Convert.ToInt32(selectedMachine.Value);
                    //combobox.ıtems.selectedvalue = deviceno
                    // Diğer verileri çek
                    string pieceCountSql = "SELECT * FROM  \"Piece_Of_Count_Tbl\" WHERE \"DeviceNo\" = @DeviceNo";
                    using (NpgsqlCommand command = new NpgsqlCommand(pieceCountSql, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceNo", selectedDeviceNo);
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(selectedMachine.Text);
                                item.SubItems.Add(reader["PieceOfCount"].ToString());
                                item.SubItems.Add(reader["Date"].ToString());
                                item.SubItems.Add(reader["ID"].ToString());
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
            try
            {
                // Seçilen cihazın bilgilerini al
                MachineListItem selectedMachine = (MachineListItem)comboBox1.SelectedItem;
                int selectedDeviceNo = Convert.ToInt32(selectedMachine.Value);

                // Diğer bilgileri gerekirse aynı şekilde alabilirsiniz
                int pieceOfCount = Convert.ToInt32(textBox1.Text);
                DateTime currentDate = dateTimePicker1.Value;

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Yeni kaydı eklemek için SQL sorgusu
                    string insertSql = "INSERT INTO \"Piece_Of_Count_Tbl\" (\"DeviceNo\", \"PieceOfCount\", \"Date\") " +
                                       "VALUES (@DeviceNo, @PieceOfCount, @Date) RETURNING \"ID\"";

                    using (NpgsqlCommand command = new NpgsqlCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("@DeviceNo", selectedDeviceNo);
                        command.Parameters.AddWithValue("@PieceOfCount", pieceOfCount);
                        command.Parameters.AddWithValue("@Date", currentDate);

                        // Yeni eklenen kayıdın ID değerini al
                        int newRecordId = (int)command.ExecuteScalar();

                        // listView'e ekleyin
                        ListViewItem newItem = new ListViewItem(selectedMachine.Text);
                        newItem.SubItems.Add(pieceOfCount.ToString());
                        newItem.SubItems.Add(currentDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        newItem.SubItems.Add(newRecordId.ToString()); // Yeni eklenen kaydın ID değerini ekleyin
                        listView1.Items.Add(newItem);

                        MessageBox.Show("Kayıt başarıyla eklendi!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    int itemId = int.Parse(item.SubItems[3].Text);

                    string sql = "DELETE FROM \"Piece_Of_Count_Tbl\" WHERE \"ID\" = @ID";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", itemId);
                        command.ExecuteNonQuery();
                    }
                    listView1.Items.Remove(item);
                }
                MessageBox.Show("Seçilen kayıtlar başarıyla silimdi.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (listView1.SelectedItems.Count > 0)
            //{
            //    ListViewItem selectedItem = listView1.SelectedItems[0];

            //    int itemId = int.Parse(selectedItem.SubItems[3].Text);

            //    DateTime updatedDate = dateTimePicker1.Value;
            //    int updatedPieceOfCount = Convert.ToInt32(textBox1.Text);
                 

            //}
        }
    }
}

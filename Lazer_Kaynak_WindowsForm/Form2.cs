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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Lazer_Kaynak_WindowsForm
{
    public partial class Form2 : Form
    {
        private const string connectionString = "Host=localhost; Database=LazerKaynakDB;Username=postgres;Password=Esk5877*;Pooling=true";
        public Form2()
        {
            InitializeComponent();
            LoadMachineNames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); 

            Form1 form1 = new Form1();
            form1.Show();
        }


        private void LoadMachineNames()   // tüm makinelerin isimlerini getirir
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM \"MachineList_Tbl\"";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        // Her bir makine için dinamik bir ListBox 
                        while (reader.Read())
                        {
                            ListBox dynamicListBox = new ListBox();
                            //dynamicListBox.SelectedIndexChanged += new EventHandler(DynamicListBox_SelectedIndexChanged);
                            dynamicListBox.Items.Add(reader["MachineName"].ToString());

                       

                            string deviceNo = reader["DeviceNo"].ToString();
                            LoadMaterialTypes(dynamicListBox, deviceNo);

                            // ListBox'ı form üzerine ekleyin
                            flowLayoutPanel1.Controls.Add(dynamicListBox);
                        }
                    }
                }
            }

        }
        private void LoadMaterialTypes(ListBox listBox, string deviceNo)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string programSql = "SELECT \"MaterialType\",\"MaterialThickness\", \"LaserPower\", \"WeldingSpeed\", \"ShieldingGas\", \"GasFlowRate\", \"GasDirection\",\"FocalLength\",  \"Wavelength\" FROM \"Program_Tbl\" WHERE \"DeviceNo\" = @DeviceNo";

                using (NpgsqlCommand programCommand = new NpgsqlCommand(programSql, connection))
                {
                    programCommand.Parameters.AddWithValue("@DeviceNo", deviceNo);

                    using (NpgsqlDataReader programReader = programCommand.ExecuteReader())
                    {
                        // Her bir MaterialType'ı ListBox'a ekle
                        while (programReader.Read())
                        {
                           // listBox.Items.Add(programReader["MaterialType"].ToString()); //bu şekilde sadece değer göüzükür.
                            listBox.Items.Add($"MaterialType: {programReader["MaterialType"]}"); //bu şekilde değerin ne olduğu da gözükür.
                            listBox.Items.Add($"MaterialThickness: {programReader["MaterialThickness"]}");
                            listBox.Items.Add($"LaserPower: {programReader["LaserPower"]}");
                            listBox.Items.Add($"WeldingSpeed: {programReader["WeldingSpeed"]}");
                            listBox.Items.Add($"ShieldingGas: {programReader["ShieldingGas"]}");
                            listBox.Items.Add($"GasFlowRate: {programReader["GasFlowRate"]}");
                            listBox.Items.Add($"GasDirection: {programReader["GasDirection"]}");
                            listBox.Items.Add($"FocalLength: {programReader["FocalLength"]}");
                            listBox.Items.Add($"Wavelength: {programReader["Wavelength"]}");


                            listBox.Items.Add("");
                        }
                    }
                }
            }
           
            listBox.Height = 1000;
            listBox.Width = 300;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form4 form4 = new Form4();
            form4.Show();

        }
        //private void DynamicListBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ListBox selectedListBox = sender as ListBox;

        //    if (selectedListBox.SelectedItem != null)
        //    {
        //        string selectedMachine = selectedListBox.SelectedItem.ToString();
        //        // Seçilen makine ismi ile ilgili işlemleri buraya ekleyebilirsiniz.
        //        MessageBox.Show("Seçilen makine: " + selectedMachine);
        //    }
        //}
    }
}

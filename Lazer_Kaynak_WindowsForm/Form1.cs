using Lazer_Kaynak_WindowsForm.Models;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Lazer_Kaynak_WindowsForm
{
    public partial class Form1 : Form
    {
        private const string connectionString = "Host=localhost; Database=LazerKaynakDB;Username=postgres;Password=Esk5877*;Pooling=true";

        private Form2 form2;
        public Form1()
        {
            InitializeComponent();
            listView1.MouseDoubleClick += new MouseEventHandler(listView1_MouseDoubleClick);
            form2 = new Form2();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear(); // Mevcut öðeleri temizle

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM \"Program_Tbl\"";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Veritabanýndan alýnan deðerleri ListView'e ekle
                            ListViewItem item = new ListViewItem(reader["MaterialType"].ToString());
                            item.SubItems.Add(reader["MaterialThickness"].ToString());
                            item.SubItems.Add(reader["LaserPower"].ToString());
                            item.SubItems.Add(reader["WeldingSpeed"].ToString());
                            item.SubItems.Add(reader["ShieldingGas"].ToString());
                            item.SubItems.Add(reader["GasFlowRate"].ToString());
                            item.SubItems.Add(reader["GasDirection"].ToString());
                            item.SubItems.Add(reader["FocalLength"].ToString());
                            item.SubItems.Add(reader["Wavelength"].ToString());
                            item.SubItems.Add(reader["ID"].ToString());

                            listView1.Items.Add(item);
                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ProgramTbl program = new ProgramTbl
            {
                MaterialType = textBox1.Text,
                MaterialThickness = Convert.ToInt32(textBox2.Text),
                LaserPower = Convert.ToInt32(textBox3.Text),
                WeldingSpeed = Convert.ToInt32(textBox4.Text),
                ShieldingGas = textBox5.Text,
                GasFlowRate = Convert.ToInt32(textBox6.Text),
                GasDirection = textBox7.Text,
                FocalLength = Convert.ToInt32(textBox8.Text),
                // Bu satýrý deðerinizin türüne göre düzenleyin.
                Wavelength = Convert.ToInt32(textBox9.Text),

            };

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO \"Program_Tbl\" (\"MaterialType\", \"MaterialThickness\", \"LaserPower\", \"WeldingSpeed\", \"ShieldingGas\", \"GasFlowRate\", \"GasDirection\", \"FocalLength\",  \"Wavelength\") " +
                             "VALUES (@MaterialType, @MaterialThickness, @LaserPower, @WeldingSpeed, @ShieldingGas, @GasFlowRate, @GasDirection, @FocalLength,  @Wavelength) RETURNING \"ID\"";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MaterialType", program.MaterialType);
                    command.Parameters.AddWithValue("@MaterialThickness", program.MaterialThickness);
                    command.Parameters.AddWithValue("@LaserPower", program.LaserPower);
                    command.Parameters.AddWithValue("@WeldingSpeed", program.WeldingSpeed);
                    command.Parameters.AddWithValue("@ShieldingGas", program.ShieldingGas);
                    command.Parameters.AddWithValue("@GasFlowRate", program.GasFlowRate);
                    command.Parameters.AddWithValue("@GasDirection", program.GasDirection);
                    command.Parameters.AddWithValue("@FocalLength", program.FocalLength);

                    command.Parameters.AddWithValue("@Wavelength", program.Wavelength);

                    program.Id = (int)command.ExecuteScalar();


                }
            }

            ListViewItem item = new ListViewItem(program.MaterialType);
            item.SubItems.Add(program.MaterialThickness.ToString());
            item.SubItems.Add(program.LaserPower.ToString());
            item.SubItems.Add(program.WeldingSpeed.ToString());
            item.SubItems.Add(program.ShieldingGas);
            item.SubItems.Add(program.GasFlowRate.ToString());
            item.SubItems.Add(program.GasDirection);
            item.SubItems.Add(program.FocalLength.ToString());
            item.SubItems.Add(program.Wavelength.ToString());


            item.SubItems.Add(program.Id.ToString());


            listView1.Items.Add(item);

            MessageBox.Show("Kayýt baþarýyla eklendi.");
        }
        private void button2_Click(object sender, EventArgs e)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    int itemId = int.Parse(item.SubItems[9].Text); // Assuming Id is in the 10th column

                    string sql = "DELETE FROM \"Program_Tbl\" WHERE \"ID\" = @ID";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", itemId);
                        command.ExecuteNonQuery();
                    }

                    // Ekrandaki ListView'dan da öðeyi kaldýr
                    listView1.Items.Remove(item);
                }

                MessageBox.Show("Seçilen kayýtlar baþarýyla silindi.");
            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Çift týklanan satýrdaki verileri input alanlarýna yerleþtir
            ListViewHitTestInfo hit = listView1.HitTest(e.Location);
            if (hit.Item != null)
            {
                ListViewItem selectedItem = hit.Item;
                textBox1.Text = selectedItem.SubItems[0].Text;
                textBox2.Text = selectedItem.SubItems[1].Text;
                textBox3.Text = selectedItem.SubItems[2].Text;
                textBox4.Text = selectedItem.SubItems[3].Text;
                textBox5.Text = selectedItem.SubItems[4].Text;
                textBox6.Text = selectedItem.SubItems[5].Text;
                textBox7.Text = selectedItem.SubItems[6].Text;
                textBox8.Text = selectedItem.SubItems[7].Text;
                textBox9.Text = selectedItem.SubItems[8].Text;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                // Get the selected item
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Get the ID of the selected item
                int itemId = int.Parse(selectedItem.SubItems[9].Text);

                // Retrieve the updated values from the input fields
                string updatedMaterialType = textBox1.Text;
                int updatedMaterialThickness = Convert.ToInt32(textBox2.Text);
                int updatedLaserPower = Convert.ToInt32(textBox3.Text);
                int updatedWeldingSpeed = Convert.ToInt32(textBox4.Text);
                string updatedShieldingGas = textBox5.Text;
                int updatedGasFlowRate = Convert.ToInt32(textBox6.Text);
                string updatedGasDirection = textBox7.Text;
                int updatedFocalLength = Convert.ToInt32(textBox8.Text);
                int updatedWavelength = Convert.ToInt32(textBox9.Text);

                // Update the record in the database
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE \"Program_Tbl\" SET " +
                                 "\"MaterialType\" = @MaterialType, " +
                                 "\"MaterialThickness\" = @MaterialThickness, " +
                                 "\"LaserPower\" = @LaserPower, " +
                                 "\"WeldingSpeed\" = @WeldingSpeed, " +
                                 "\"ShieldingGas\" = @ShieldingGas, " +
                                 "\"GasFlowRate\" = @GasFlowRate, " +
                                 "\"GasDirection\" = @GasDirection, " +
                                 "\"FocalLength\" = @FocalLength, " +
                                 "\"Wavelength\" = @Wavelength " +
                                 "WHERE \"ID\" = @ID";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@MaterialType", updatedMaterialType);
                        command.Parameters.AddWithValue("@MaterialThickness", updatedMaterialThickness);
                        command.Parameters.AddWithValue("@LaserPower", updatedLaserPower);
                        command.Parameters.AddWithValue("@WeldingSpeed", updatedWeldingSpeed);
                        command.Parameters.AddWithValue("@ShieldingGas", updatedShieldingGas);
                        command.Parameters.AddWithValue("@GasFlowRate", updatedGasFlowRate);
                        command.Parameters.AddWithValue("@GasDirection", updatedGasDirection);
                        command.Parameters.AddWithValue("@FocalLength", updatedFocalLength);
                        command.Parameters.AddWithValue("@Wavelength", updatedWavelength);
                        command.Parameters.AddWithValue("@ID", itemId);

                        command.ExecuteNonQuery();
                    }
                }

                // Update the selected item in the ListView
                selectedItem.SubItems[0].Text = updatedMaterialType;
                selectedItem.SubItems[1].Text = updatedMaterialThickness.ToString();
                selectedItem.SubItems[2].Text = updatedLaserPower.ToString();
                selectedItem.SubItems[3].Text = updatedWeldingSpeed.ToString();
                selectedItem.SubItems[4].Text = updatedShieldingGas;
                selectedItem.SubItems[5].Text = updatedGasFlowRate.ToString();
                selectedItem.SubItems[6].Text = updatedGasDirection;
                selectedItem.SubItems[7].Text = updatedFocalLength.ToString();
                selectedItem.SubItems[8].Text = updatedWavelength.ToString();

                MessageBox.Show("Kayýt baþarýyla güncellendi.");
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir kayýt seçin.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
           
            form2.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}
    

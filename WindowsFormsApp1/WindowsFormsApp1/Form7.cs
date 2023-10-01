using System;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        OleDbConnection conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\godsliker\\Desktop\\Screenshots - Copy\\Database31.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand cmd1 = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("remplire les champs");
            }
            else
            {
                conection.Open();
                int n = int.Parse(textBox7.Text);
                string register = " SELECT * FROM t_admin where cin=" + n;

                cmd = new OleDbCommand(register, conection);

                OleDbDataReader dr = cmd.ExecuteReader();


                if (dr.Read() == true)
                {
                    string nom = dr["nom"].ToString();
                    string prenom = dr["prenom"].ToString();
                    DateTime D;
                    DateTime.TryParse(dr["D"].ToString(), out D);
                    // Perform validation checks before running the code.
                    if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text) || textBox1.Text != textBox6.Text || comboBox1.SelectedIndex < 0)
                    {
                        MessageBox.Show("Remplir le formulaire correctement");
                    }
                    else
                    {
                        try
                        {
                            int k = int.Parse(textBox7.Text);

                            // Delete the existing record if it exists
                            string deleteQuery = "DELETE FROM t_admin WHERE cin = @Value1";
                            using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, conection))
                            {
                                deleteCmd.Parameters.AddWithValue("@Value1", k);
                                deleteCmd.ExecuteNonQuery();
                            }

                            // Insert the new record
                            string insertQuery = "INSERT INTO t_admin (cin, mdp, nom, prenom, num, adresse, type_s, D) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8)";
                            using (OleDbCommand insertCmd = new OleDbCommand(insertQuery, conection))
                            {
                                insertCmd.Parameters.AddWithValue("@Value1", k);
                                insertCmd.Parameters.AddWithValue("@Value2", textBox6.Text);
                                insertCmd.Parameters.AddWithValue("@Value3", nom);
                                insertCmd.Parameters.AddWithValue("@Value4", prenom);
                                insertCmd.Parameters.AddWithValue("@Value5", textBox5.Text);
                                insertCmd.Parameters.AddWithValue("@Value6", textBox4.Text);
                                insertCmd.Parameters.AddWithValue("@Value7", comboBox1.SelectedIndex.ToString());
                                insertCmd.Parameters.AddWithValue("@Value8", D);

                                int rowsAffected = insertCmd.ExecuteNonQuery();
                                MessageBox.Show($"{rowsAffected} record(s) inserted.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}");
                        }
                        conection.Close();
                    }
                }
            }
        }
    }
}

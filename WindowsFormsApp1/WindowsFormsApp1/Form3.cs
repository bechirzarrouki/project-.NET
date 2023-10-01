using System;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private string st;
        private string num;
        private string ste;
        private string type;
        private string nom;
        private string prenom;
        private string mdp;
        private string tel;
        private string adr;
        private DateTime D;

        public Form3(string str_value)
        {
            st = str_value;
            InitializeComponent();
        }

        OleDbConnection conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\godsliker\\Desktop\\Screenshots - Copy\\Database31.accdb");
        OleDbCommand cmd = new OleDbCommand();

        private void Form3_Load(object sender, EventArgs e)
        {
            conection.Open();
            int n = int.Parse(st);
            string register = "SELECT * FROM t_admin WHERE cin=" + n;
            cmd = new OleDbCommand(register, conection);

            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                DateTime date;
                DateTime.TryParse(dr["D"].ToString(), out date);
                D = DateTime.Today;
                TimeSpan time = D - date;
                this.type = dr["Type_s"].ToString();
                this.nom = dr["nom"].ToString();
                this.prenom = dr["prenom"].ToString();
                this.mdp = dr["mdp"].ToString();
                this.tel = dr["num"].ToString();
                this.adr = dr["adresse"].ToString();
        string type = dr["type_s"].ToString();
                int monthsPassed = (int)Math.Floor((double)time.Days / 30.436875);
                int prix;
                for (int i = 0; i < monthsPassed; i++)
                {
                    string sqlQuery = "INSERT INTO facture (cin, prix, D) VALUES (@Value1, @Value2, @Value3)";
                    cmd = new OleDbCommand(sqlQuery, conection);
                    cmd.Parameters.AddWithValue("@Value1", int.Parse(st));

                    if (type == "ADSL 4M")
                    {
                        prix = 32;
                    }
                    else
                    {
                        if (type == "ADSL 8M")
                        {
                            prix = 36;
                        }
                        else
                        {
                            prix = 40;
                        }
                    }

                    cmd.Parameters.AddWithValue("@Value2", prix);
                    
                    cmd.Parameters.AddWithValue("@Value3", (DateTime) date.AddMonths(i));

                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        // Handle the insert here if needed
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Probleme: " + ex.Message);
                    }
                }

                conection.Close();
            }

            }

        private void button1_Click(object sender, EventArgs e)
        {
            String req = "SELECT * FROM facture WHERE cin = @value ORDER BY D ASC";
            cmd = new OleDbCommand(req, conection);
            conection.Open();
            cmd.Parameters.AddWithValue("@Value", int.Parse(st));
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                label6.Text =dr["ID"].ToString();
                ste = dr["ID"].ToString();
                num = dr["ID"].ToString();
                label7.Text = dr["prix"].ToString();
                label8.Text = dr["D"].ToString();
            }
            else
            {
                MessageBox.Show("aucun facture");
            }
            conection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (num == "False")
            {
                
            }
            else
            {
                string req1 = "DELETE * FROM facture WHERE ID=@value1";
                cmd = new OleDbCommand(req1, conection);
                cmd.Parameters.AddWithValue("@Value1", ste);
                conection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                try
                {

                    // Delete the existing record if it exists
                    string deleteQuery = "DELETE FROM t_admin WHERE cin = @Value1";
                    using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, conection))
                    {
                        deleteCmd.Parameters.AddWithValue("@Value1", st);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Insert the new record
                    string insertQuery = "INSERT INTO t_admin (cin, mdp, nom, prenom, num, adresse, type_s, D) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8)";
                    using (OleDbCommand insertCmd = new OleDbCommand(insertQuery, conection))
                    {
                        insertCmd.Parameters.AddWithValue("@Value1", st);
                        insertCmd.Parameters.AddWithValue("@Value2", mdp);
                        insertCmd.Parameters.AddWithValue("@Value3", nom);
                        insertCmd.Parameters.AddWithValue("@Value4", prenom);
                        insertCmd.Parameters.AddWithValue("@Value5", tel);
                        insertCmd.Parameters.AddWithValue("@Value6", adr);
                        insertCmd.Parameters.AddWithValue("@Value7", type);
                        insertCmd.Parameters.AddWithValue("@Value8", D);

                        rowsAffected = insertCmd.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} record(s) inserted.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                rowsAffected = cmd.ExecuteNonQuery();
                
                conection.Close();
                Form3 form3 = new Form3(st);
                form3.Show();
                this.Hide();
            }
        }
    }
}

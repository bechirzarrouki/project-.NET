using System;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        OleDbConnection conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\godsliker\\Desktop\\Screenshots - Copy\\Database31.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand cmd1 = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text)||textBox7.Text != textBox6.Text || comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("remplir le formulair");
            }
            else
            {
                int n = int.Parse(textBox3.Text);
                string sqlQuery = "INSERT INTO t_admin (cin, mdp,nom,prenom,num,adresse,type_s,D) VALUES (@Value1, @Value2,@Value3,@Value4,@Value5,@Value6,@value7,@value8)";
                cmd = new OleDbCommand(sqlQuery, conection);
                cmd.Parameters.AddWithValue("@Value1", n);
                cmd.Parameters.AddWithValue("@Value2", textBox6.Text);
                cmd.Parameters.AddWithValue("@Value3", textBox1.Text);
                cmd.Parameters.AddWithValue("@Value4", textBox2.Text);
                cmd.Parameters.AddWithValue("@Value5", textBox5.Text);
                cmd.Parameters.AddWithValue("@Value6", textBox4.Text);
                cmd.Parameters.AddWithValue("@Value7", comboBox1.SelectedItem.ToString());
                DateTime currentDateTime = DateTime.Today;
                MessageBox.Show("Probleme" + currentDateTime);
                cmd.Parameters.AddWithValue("@Value8", currentDateTime);
                try
                {
                    conection.Open();

                    // Execute the INSERT query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    MessageBox.Show("Utilisateur ajouter");
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Probleme");
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else if (!(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
    }
}

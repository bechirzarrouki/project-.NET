using System;
using System.Data.OleDb;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
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
        OleDbConnection conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\godsliker\\Desktop\\Screenshots - Copy\\Database31.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand cmd1 = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("remplire les champs");
            }
            else
            {
                int n = int.Parse(textBox7.Text);
                string sqlQuery = "DELETE * FROM t_admin WHERE cin=@value1 AND mdp=@value2;";
                cmd = new OleDbCommand(sqlQuery, conection);
                cmd.Parameters.AddWithValue("@Value1", n); // Replace with your actual value
                cmd.Parameters.AddWithValue("@Value2", textBox8.Text);
                try
                {
                    conection.Open();

                    // Execute the INSERT query
                    int rowsAffected = cmd.ExecuteNonQuery();

                    MessageBox.Show("Utilisateur suprimer");
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
    }
}

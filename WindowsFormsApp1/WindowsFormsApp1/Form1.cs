using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {   

        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection conection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\godsliker\\Desktop\\Screenshots - Copy\\Database31.accdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbCommand cmd1 = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();





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

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("remplire les champs");
            }
            else
            {
                conection.Open();
                int n = int.Parse(textBox7.Text);
                string register = " SELECT * FROM t_admin where cin=" + n + "and mdp='" + textBox8.Text + "'";

                cmd = new OleDbCommand(register, conection);

                OleDbDataReader dr = cmd.ExecuteReader();


                if (dr.Read() == true)
                {

                    if (dr["t-a"].ToString() == "1")
                    {
                        Form2 form2 = new Form2();
                        form2.Show();
                        this.Hide();
                    }
                    if (dr["t-a"].ToString() != "1")
                    {
                        Form3 form3 = new Form3(textBox7.Text);
                        form3.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("login ou mot de passe incorect");
                }


                conection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Library
{
    public partial class Form1 : Form
    {
 


        public Form1()
        {
            InitializeComponent();    

        }

        
        private void button1_Click(object sender, EventArgs e)
        {

            string login = textBox1.Text;
            string password = textBox2.Text;

            using (var conn = new NpgsqlConnection("Host=localhost;Port=5432;Database=Crack-a-book;Username=postgres;Password=root;"))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT COUNT(*) FROM users WHERE login = '" + login + "' AND password = '" + password + "'";


                    int count = 0;
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out count))
                    {
                        if (count > 0)
                        {
                            MessageBox.Show("Вы успешно вошли в средство манипуляции над базой данных Crack-a-book!");
                            Form form2 = new Form2();
                            this.Hide();
                            form2.Show();
                        }
                        else
                        {
                            MessageBox.Show("Проверьте логин и пароль: кажется вы ввели неверное имя пользователя или пароль.");
                        }
                    }
                    conn.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

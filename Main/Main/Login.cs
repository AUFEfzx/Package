using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;



namespace Main
{
    public partial class Login : Form
    {
        Form1 f;
        public static string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb";
        public Login(Form1 Form1)
        {
            this.f = Form1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = String.Format("select count(*) from [User] where name='{0}' and psw='{1}'", textBox1.Text, textBox2.Text);
            
            OleDbDataReader r = datahelper.get_reader(sql);
            if (r.Read())
            {
                int count = Convert.ToInt32(r[0]);
                if (count == 1)
                {
                    this.f.ShowInTaskbar = true;
                    this.f.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("密码或者用户名错误！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reg r1 = new Reg(this);
            r1.Show();
            this.Hide();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

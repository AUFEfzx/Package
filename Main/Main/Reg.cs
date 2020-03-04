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
    public partial class Reg : Form
    {
        Login f;
        public Reg(Login login)
        {
            this.f = login;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name,pwd,pwd1,email,sex;
            int age;
            name = textBox1.Text;
            pwd = textBox3.Text;
            pwd1 = textBox2.Text;
            email = textBox4.Text;
            sex = comboBox1.Text;
            age=Convert.ToInt32(comboBox2.Text);
            if(pwd!=pwd1)
            {
                MessageBox.Show("两次输入密码不同!");
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; comboBox1.Text = ""; comboBox2.Text = "";
                return;
            }
            if(name==""||pwd==""||email==""||sex=="")
            {
                MessageBox.Show("数据不可为空!");
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; comboBox1.Text = ""; comboBox2.Text = "";
                return;
            }
            
            String sql = String.Format("insert into [User] (name,psw,sex,age,email) values ('{0}','{1}','{2}','{3}','{4}')", name, pwd, sex, age, email);
            String sql_find = String.Format("Select count(*) from [User] where name='{0}'", name);
            
            int count = 0;
            OleDbDataReader r = datahelper.get_reader(sql_find);
            if (r.Read())
            {
                count = Convert.ToInt32(r[0]);
            }
            if (count > 0)
            {
                MessageBox.Show("用户名已经注册!");
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = ""; comboBox1.Text = ""; comboBox2.Text = "";
                return;
            }
               
            int i = datahelper.get_Execu(sql);
            if (i == 1)
            {
                MessageBox.Show("注册成功！");
                this.f.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("注册失败！");
            }
            this.f.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.f.Show();
            this.Close();
        }
    }
}

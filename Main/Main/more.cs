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
    public partial class more : Form
    {
        string topic;
        public more(string s)
        {
            topic = s;
            InitializeComponent();
        }

        private void more_Load(object sender, EventArgs e)
        {
            string word1 = "", word2 = "", word3 = "";
            string sql = String.Format("select zt_key1,zt_key2,zt_key3 from zt where zt_name='{0}'", this.topic);
            OleDbDataReader r = datahelper.get_reader(sql);
            if (r.Read())
            {
                word1= Convert.ToString(r[0]);
                word2 = Convert.ToString(r[1]);
                word3 = Convert.ToString(r[2]);
            }
            if (word2 != "")
            {
                sql = String.Format("select  news_name,location,Heat,time,side from news_table where news_name like '%{0}%'", word2);
                r = datahelper.get_reader(sql);
                while(r.Read())
                {
                    listBox1.Items.Add(Convert.ToString(r[0]));
                    listBox2.Items.Add(Convert.ToString(r[1])); 
                    listBox3.Items.Add(Convert.ToString(r[2]));
                    string s = Convert.ToString(r[3]);
                    string s2 = "";
                    for (int i = 0; i < 9; i++)
                    {
                        s2 += s[i];
                    }
                    listBox4.Items.Add(s2);
                    listBox5.Items.Add(Convert.ToString(r[4]));
                }
            }
            if (word3 != "")
            {
                sql = String.Format("select  news_name,location,Heat,time,side from news_table where news_name like '%{0}%'", word3);
                r = datahelper.get_reader(sql);
                while (r.Read())
                {
                    listBox1.Items.Add(Convert.ToString(r[0]));
                    listBox2.Items.Add(Convert.ToString(r[1]));
                    listBox3.Items.Add(Convert.ToString(r[2]));
                    string s = Convert.ToString(r[3]);
                    string s2 = "";
                    for (int i = 0; i < 10; i++)
                    {
                        s2 += s[i];
                    }
                    listBox4.Items.Add(s2);
                    listBox5.Items.Add(Convert.ToString(r[4]));
                }
            }
        }
    }
}

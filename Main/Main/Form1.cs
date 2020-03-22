using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Data.OleDb;
using System.Threading;

namespace Main
{
    public partial class Form1 : Form
    {
        public static string conn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb";
        public static string url = "http://127.0.0.1:5088/?";
        public static string[] txt_news = new string[25];
        public static int[] num_news = new int[25];
        public static string[] loc_news = new string[25];
        public static string[] url_news = new string[25];
        public static string[] side_news = new string[25];
        public string main_word = "",content="",my_email="";
        public static string[] xiangqing = new string[100];
        public int email_flag=0,xiangqing_flag=0,six_flag=1,warn_flag=0;
        Bitmap p1;
        int form_flag = 1;
        public ToolTip toolTip1 = new ToolTip();
        public Form1()
        {
            InitializeComponent();
            this.Hide();
            /*
            */
        }
        public static string port_get(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            try
            {

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
               // MessageBox.Show(ex.Message);
                return null;
            }
            
        }
        //测字符串相似度
        public static float levenshtein(string str1, string str2)
        {
            int len1 = str1.Length;
            int len2 = str2.Length;
            int[,] dif = new int[len1 + 1, len2 + 1];
            for (int a = 0; a <= len1; a++)
            {
                dif[a, 0] = a;
            }
            for (int a = 0; a <= len2; a++)
            {
                dif[0, a] = a;
            }
            int temp;
            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        temp = 0;
                    }
                    else
                    {
                        temp = 1;
                    }
                    dif[i, j] = Math.Min(Math.Min(dif[i - 1, j - 1] + temp, dif[i, j - 1] + 1), dif[i - 1, j] + 1);
                }
            }
            Console.WriteLine("字符串\"" + str1 + "\"与\"" + str2 + "\"的比较");
            Console.WriteLine("差异步骤：" + dif[len1, len2]);
            float similarity = 1 - (float)dif[len1, len2] / Math.Max(str1.Length, str2.Length);
            Console.WriteLine("相似度：" + similarity);
            return similarity;
        }
        //连接字符串获取文本
        public static string get_data()
        {
            string s = "";
            return s;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table order by time desc");
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 10; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Today;
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where time='{0}' order by Heat desc", Convert.ToString(dt) );
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 10; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Today;
            dt = dt.AddDays(-1);
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where time='{0}' order by Heat desc", Convert.ToString(dt));
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 10; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Today;
            DateTime dt1 = dt.AddDays(-1);
            DateTime dt2 = dt.AddDays(-2);
            DateTime dt3 = dt.AddDays(-3);
            DateTime dt4 = dt.AddDays(-4);
            DateTime dt5 = dt.AddDays(-5);
            DateTime dt6 = dt.AddDays(-6);
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where time='{0}' or time='{1}' or time='{2}' or time='{3}' or time='{4}' or time='{5}' or time='{6}' order by Heat desc", Convert.ToString(dt), Convert.ToString(dt1), Convert.ToString(dt2),Convert.ToString(dt3),Convert.ToString(dt4),Convert.ToString(dt5), Convert.ToString(dt6));
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 10; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Thread mythread = new Thread(ThreadMain);
            //mythread.Start();
            if (form_flag==1)
            {
                Login l1 = new Login(this);
                l1.Show();
                this.Hide();
                this.ShowInTaskbar = false;
                form_flag++;
            }
            string sql_warn = String.Format("select id  from warn order by id desc");
            OleDbDataReader r_warn = datahelper.get_reader(sql_warn);
            if(r_warn.Read())
            {
                warn_flag = Convert.ToInt32(r_warn[0]);
            }

            string sql_button = String.Format("select news_name,location,side,time,url  from news_table order by Heat desc");
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }

            String sql_all = String.Format("Select count(*) from news_table");
            r = datahelper.get_reader(sql_all);
            if (r.Read())
            {
                label2.Text = Convert.ToString(r[0]);
            }

            String sql_zhong = String.Format("Select count(*) from news_table where side='中性'");
            r = datahelper.get_reader(sql_zhong);
            if (r.Read())
            {
                label7.Text = Convert.ToString(r[0]);
            }

            String sql_fu = String.Format("Select count(*) from news_table where side='消极'");
            r = datahelper.get_reader(sql_fu);
            if (r.Read())
            {
                label5.Text = Convert.ToString(r[0]);
            }

            String sql_zheng = String.Format("Select count(*) from news_table where side='积极'");
            r = datahelper.get_reader(sql_zheng);
            if (r.Read())
            {
                label9.Text = Convert.ToString(r[0]);
            }

            sql_button = String.Format("select keywords from key_table order by ID desc");
            //sql_button = String.Format("select keywords from key_table order by ID desc");
            r = datahelper.get_reader(sql_button);
            int r_i = 0;
            while (r.Read())
            {
                if(r_i==0)
                    button1.Text = Convert.ToString(r[0]).Trim('"');
                else if(r_i==1)
                    button2.Text = Convert.ToString(r[0]).Trim('"');
                else if (r_i == 2)
                    button3.Text = Convert.ToString(r[0]).Trim('"');
                else if (r_i == 3)
                    button4.Text = Convert.ToString(r[0]).Trim('"');
                r_i++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where news_name like '%{0}%'", button3.Text);
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread mythread = new Thread(ThreadMain);
            mythread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where news_name like '%{0}%'", button1.Text);
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private static void ThreadMain()
        {
            DateTime dt = DateTime.Today;
            int[] flag = new int[25];
            for (int i = 0; i < 25; i++)
                flag[i] = 0;
            //获取网易
            string new_url = url + "name=get_163()&od=1&td=1";
            txt_news[0] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_163()&od=1&td=2";
            txt_news[1] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_163()&od=1&td=3";
            txt_news[2] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_163()&od=1&td=4";
            txt_news[3] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_163()&od=1&td=5";
            txt_news[4] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            //获取豆瓣
            new_url = url + "name=get_douban()&od=1&td=1";
            txt_news[5] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_douban()&od=1&td=2";
            txt_news[6] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_douban()&od=1&td=3";
            txt_news[7] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_douban()&od=1&td=4";
            txt_news[8] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_douban()&od=1&td=5";
            txt_news[9] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            //获取搜狗
            new_url = url + "name=get_sougou()&od=1&td=1";
            txt_news[10] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_sougou()&od=1&td=2";
            txt_news[11] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_sougou()&od=1&td=3";
            txt_news[12] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_sougou()&od=1&td=4";
            txt_news[13] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_sougou()&od=1&td=5";
            txt_news[14] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            //获取贴吧
            new_url = url + "name=get_tieba()&od=1&td=1";
            txt_news[15] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_tieba()&od=1&td=2";
            txt_news[16] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_tieba()&od=1&td=3";
            txt_news[17] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_tieba()&od=1&td=4";
            txt_news[18] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            new_url = url + "name=get_tieba()&od=1&td=5";
            txt_news[19] = port_get(new_url).Trim('"').Trim(' ').Trim('"').Trim('\'');
            //插入数据库新闻表
            for (int i = 0; i < 20; i++)
            {
                txt_news[i] = txt_news[i].Replace("'", "");
                txt_news[i] = txt_news[i].Trim('"').Trim(' ').Trim('"');
                String sql_find = String.Format("Select count(*) from news_table where news_name='{0}'", txt_news[0]);
                if (get_similar(sql_find))
                { 
                    flag[i] = 1;
                }
            }


            //获取网易
            //string new_url = url + "name=get_163()&od=1&td=1";
            string new_url_num = url + "name=get_163()&od=2&td=1";
            string new_url_url = url + "name=get_163()&od=3&td=1";
            string new_url_loc = url + "name=get_163()&od=4&td=1";
            string new_url_side = url + "name=get_163()&od=5&td=1";
            if (flag[0] == 1)
            {
                num_news[0] = Convert.ToInt32(port_get(new_url_num));
                loc_news[0] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[0] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[0] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_163()&od=2&td=2";
            new_url_url = url + "name=get_163()&od=3&td=2";
            new_url_loc = url + "name=get_163()&od=4&td=2";
            new_url_side = url + "name=get_163()&od=5&td=2";
            if (flag[1] == 1)
            {
                num_news[1] = Convert.ToInt32(port_get(new_url_num));
                loc_news[1] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[1] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[1] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_163()&od=2&td=3";
            new_url_url = url + "name=get_163()&od=3&td=3";
            new_url_loc = url + "name=get_163()&od=4&td=3";
            new_url_side = url + "name=get_163()&od=5&td=3";
            if (flag[2] == 1)
            {
                num_news[2] = Convert.ToInt32(port_get(new_url_num));
                loc_news[2] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[2] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[2] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_163()&od=2&td=4";
            new_url_url = url + "name=get_163()&od=3&td=4";
            new_url_loc = url + "name=get_163()&od=4&td=4";
            new_url_side = url + "name=get_163()&od=5&td=4";
            if (flag[3] == 1)
            {
                num_news[3] = Convert.ToInt32(port_get(new_url_num));
                loc_news[3] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[3] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[3] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_163()&od=2&td=5";
            new_url_url = url + "name=get_163()&od=3&td=5";
            new_url_loc = url + "name=get_163()&od=4&td=5";
            new_url_side = url + "name=get_163()&od=5&td=5";
            if (flag[4] == 1)
            {
                num_news[4] = Convert.ToInt32(port_get(new_url_num));
                loc_news[4] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[4] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[4] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            //获取豆瓣
            new_url_num = url + "name=get_douban()&od=2&td=1";
            new_url_url = url + "name=get_douban()&od=3&td=1";
            new_url_loc = url + "name=get_douban()&od=4&td=1";
            new_url_side = url + "name=get_douban()&od=5&td=1";
            if (flag[5] == 1)
            {
                num_news[5] = Convert.ToInt32(port_get(new_url_num));
                loc_news[5] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[5] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[5] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(1, url_news[5], txt_news[5], Convert.ToString(dt.Date), loc_news[5]);
            }
            new_url_num = url + "name=get_douban()&od=2&td=2";
            new_url_url = url + "name=get_douban()&od=3&td=2";
            new_url_loc = url + "name=get_douban()&od=4&td=2";
            new_url_side = url + "name=get_douban()&od=5&td=2";
            if (flag[6] == 1)
            {
                num_news[6] = Convert.ToInt32(port_get(new_url_num));
                loc_news[6] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[6] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[6] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(1, url_news[6], txt_news[6], Convert.ToString(dt.Date), loc_news[6]);
            }
            new_url_num = url + "name=get_douban()&od=2&td=3";
            new_url_url = url + "name=get_douban()&od=3&td=3";
            new_url_loc = url + "name=get_douban()&od=4&td=3";
            new_url_side = url + "name=get_douban()&od=5&td=3";
            if (flag[7] == 1)
            {
                num_news[7] = Convert.ToInt32(port_get(new_url_num));
                loc_news[7] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[7] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[7] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(1, url_news[7], txt_news[7], Convert.ToString(dt.Date), loc_news[7]);
            }
            new_url_num = url + "name=get_douban()&od=2&td=4";
            new_url_url = url + "name=get_douban()&od=3&td=4";
            new_url_loc = url + "name=get_douban()&od=4&td=4";
            new_url_side = url + "name=get_douban()&od=5&td=4";
            if (flag[8] == 1)
            {
                num_news[8] = Convert.ToInt32(port_get(new_url_num));
                loc_news[8] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[8] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[8] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(1, url_news[8], txt_news[8], Convert.ToString(dt.Date), loc_news[8]);
            }
            new_url_num = url + "name=get_douban()&od=2&td=5";
            new_url_url = url + "name=get_douban()&od=3&td=5";
            new_url_loc = url + "name=get_douban()&od=4&td=5";
            new_url_side = url + "name=get_douban()&od=5&td=5";
            if (flag[9] == 1)
            {
                num_news[9] = Convert.ToInt32(port_get(new_url_num));
                loc_news[9] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[9] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[9] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(1, url_news[9], txt_news[9], Convert.ToString(dt.Date), loc_news[9]);
            }
            //获取搜狗
            new_url_num = url + "name=get_sougou()&od=2&td=1";
            new_url_url = url + "name=get_sougou()&od=3&td=1";
            new_url_loc = url + "name=get_sougou()&od=4&td=1";
            new_url_side = url + "name=get_sougou()&od=5&td=1";
            if (flag[10] == 1)
            {
                num_news[10] = Convert.ToInt32(port_get(new_url_num));
                loc_news[10] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[10] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[10] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_sougou()&od=2&td=2";
            new_url_url = url + "name=get_sougou()&od=3&td=2";
            new_url_loc = url + "name=get_sougou()&od=4&td=2";
            new_url_side = url + "name=get_sougou()&od=5&td=2";
            if (flag[11] == 1)
            {
                num_news[11] = Convert.ToInt32(port_get(new_url_num));
                loc_news[11] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[11] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[11] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_sougou()&od=2&td=3";
            new_url_url = url + "name=get_sougou()&od=3&td=3";
            new_url_loc = url + "name=get_sougou()&od=4&td=3";
            new_url_side = url + "name=get_sougou()&od=5&td=3";
            if (flag[12] == 1)
            {
                num_news[12] = Convert.ToInt32(port_get(new_url_num));
                loc_news[12] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[12] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[12] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_sougou()&od=2&td=4";
            new_url_url = url + "name=get_sougou()&od=3&td=4";
            new_url_loc = url + "name=get_sougou()&od=4&td=4";
            new_url_side = url + "name=get_sougou()&od=5&td=4";
            if (flag[13] == 1)
            {
                num_news[13] = Convert.ToInt32(port_get(new_url_num));
                loc_news[13] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[13] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[13] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            new_url_num = url + "name=get_sougou()&od=2&td=5";
            new_url_url = url + "name=get_sougou()&od=3&td=5";
            new_url_loc = url + "name=get_sougou()&od=4&td=5";
            new_url_side = url + "name=get_sougou()&od=5&td=5";
            if (flag[14] == 1)
            {
                num_news[14] = Convert.ToInt32(port_get(new_url_num));
                loc_news[14] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[14] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[14] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
            }
            //获取贴吧
            new_url_num = url + "name=get_tieba()&od=2&td=1";
            new_url_url = url + "name=get_tieba()&od=3&td=1";
            new_url_loc = url + "name=get_tieba()&od=4&td=1";
            new_url_side = url + "name=get_tieba()&od=5&td=1";
            if (flag[15] == 1)
            {
                num_news[15] = Convert.ToInt32(port_get(new_url_num));
                loc_news[15] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[15] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[15] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(2, url_news[15], txt_news[15], Convert.ToString(dt.Date),loc_news[15]);
            }
            new_url_num = url + "name=get_tieba()&od=2&td=2";
            new_url_url = url + "name=get_tieba()&od=3&td=2";
            new_url_loc = url + "name=get_tieba()&od=4&td=2";
            new_url_side = url + "name=get_tieba()&od=5&td=2";
            if (flag[16] == 1)
            {
                num_news[16] = Convert.ToInt32(port_get(new_url_num));
                loc_news[16] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[16] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[16] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(2, url_news[16], txt_news[16], Convert.ToString(dt.Date), loc_news[16]);
            }
            new_url_num = url + "name=get_tieba()&od=2&td=3";
            new_url_url = url + "name=get_tieba()&od=3&td=3";
            new_url_loc = url + "name=get_tieba()&od=4&td=3";
            new_url_side = url + "name=get_tieba()&od=5&td=3";
            if (flag[17] == 1)
            {
                
                num_news[17] = Convert.ToInt32(port_get(new_url_num));
                loc_news[17] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[17] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[17] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(2, url_news[17], txt_news[17], Convert.ToString(dt.Date), loc_news[17]);
            }
            new_url_num = url + "name=get_tieba()&od=2&td=4";
            new_url_url = url + "name=get_tieba()&od=3&td=4";
            new_url_loc = url + "name=get_tieba()&od=4&td=4";
            new_url_side = url + "name=get_tieba()&od=5&td=4";
            if (flag[18] == 1)
            {
                num_news[18] = Convert.ToInt32(port_get(new_url_num));
                loc_news[18] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[18] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[18] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(2, url_news[18], txt_news[18], Convert.ToString(dt.Date), loc_news[18]);
            }
            new_url_num = url + "name=get_tieba()&od=2&td=5";
            new_url_url = url + "name=get_tieba()&od=3&td=5";
            new_url_loc = url + "name=get_tieba()&od=4&td=5";
            new_url_side = url + "name=get_tieba()&od=5&td=5";
            if (flag[19] == 1)
            {
                num_news[19] = Convert.ToInt32(port_get(new_url_num));
                loc_news[19] = port_get(new_url_loc).Trim('"').Trim(' ').Trim('"');
                url_news[19] = port_get(new_url_url).Trim('"').Trim(' ').Trim('"');
                side_news[19] = port_get(new_url_side).Trim('"').Trim(' ').Trim('"');
                insert_comments(2, url_news[19], txt_news[19], Convert.ToString(dt.Date), loc_news[19]);
            }
            for (int i = 0; i < 20; i++)
            {
                if (flag[i] == 1)
                {
                    String sql = String.Format("insert into news_table (news_name,url,location,side,Heat,[time]) values ('{0}','{1}','{2}','{3}','{4}','{5}')", txt_news[i], url_news[i], loc_news[i], side_news[i], num_news[i], Convert.ToString(dt.Date));
                    insert_news(sql);
                }
            }

            //png_post()
            string s;
            for(int i=0;i<4;i++)
            {
                s = port_get("http://127.0.0.1:5088/?name=png_post()&od="+Convert.ToString(i+1)+"&td=1");
                if (s == "null" || s=="")
                    continue;
                String sql = String.Format("insert into key_table (keywords) values ('{0}')",s);
                insert_news(sql);
            }
            /************插入贴吧和豆瓣的评论*****************/
            /*
            string sql_in = String.Format("select  news_name,location,url from news_table where location='贴吧'");
            OleDbDataReader r = datahelper.get_reader(sql_in);
            while (r.Read())
            {
                string[] tieba_comments = new string[25];
                string long_s = port_get("http://127.0.0.1:5088/?name=get_more()&od=2&td="+ Convert.ToString(r[1]));
                for(int i=0;i<25;i++)
                {
                    tieba_comments[i]= "";
                    for (int j=0;j<long_s.Length;j++)
                    {
                        if (long_s[j] == '$')
                            break;
                        tieba_comments[i]+= long_s[j];
                    }
                }
            }*/
        }
         
        public static void insert_comments(int flag,string url,string news_name,string mytime,string loc)//flag：1是豆瓣，2是百度贴吧
        {
            if(flag==1)
            {
                string new_url = "";
                string old_url = url;
                for (int i = 37; i < old_url.Length; i++)
                {
                    if (old_url[i] < 48 || old_url[i] > 57)
                        break;
                    new_url += old_url[i];
                }
                string long_s = "";
                try
                {
                    long_s = port_get("http://127.0.0.1:5088/?name=get_more()&od=1&td=" + new_url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                int val = 1;
                for (int i = 0; ; i++)
                {
                    string comm = "";
                    for (int j = val; j < long_s.Length; j++)
                    {
                        val = j + 1;
                        if (long_s[j] == '$')
                        {
                            break;
                        }
                        comm += long_s[j];
                    }
                    if (val >= long_s.Length)
                        break;
                    if (comm == " ")
                        continue;
                    string comm_side = "";
                    comm_side = port_get("http://127.0.0.1:5088/?name=get_side&od=" + comm);
                    if (comm_side == null) 
                        comm_side = "中性";
                    String sql_insert = String.Format("insert into comments (comment_content,topic,side,[time],location) values ('{0}','{1}','{2}','{3}','{4}')", comm, news_name, comm_side.Trim('"').Trim('/'),mytime,loc);
                    insert_news(sql_insert);
                }
            }
            else if(flag==2)
            {
                string long_s = "";
                try
                {
                    long_s = port_get("http://127.0.0.1:5088/?name=get_more()&od=2&td=" + url);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if(long_s==null)
                {
                    MessageBox.Show("error!");
                    return;
                }
                int val = 1;
                for (int i = 0; ; i++)
                {
                    string comm = "";
                    for (int j = val; j < long_s.Length; j++)
                    {

                        val = j + 1;
                        if (long_s[j] == '$')
                        {
                            break;
                        }
                        comm += long_s[j];
                    }
                    if (val >= long_s.Length)
                        break;
                    if (comm == " ")
                        continue;
                    string comm_side = "";
                    comm_side = port_get("http://127.0.0.1:5088/?name=get_side&od=" + comm);
                    if (comm_side == null) comm_side = "中性";
                    String sql_insert = String.Format("insert into comments (comment_content,topic,side,[time],location) values ('{0}','{1}','{2}','{3}','{4}')", comm, news_name, comm_side.Trim('"').Trim('/'),mytime,loc);
                    insert_news(sql_insert);
                }
            }
                
            
        }

        public static bool get_similar(string sql_find)
        {
            int count = 0;
            OleDbDataReader r = datahelper.get_reader(sql_find);
            if (r.Read())
            {
                count = Convert.ToInt32(r[0]);
            }
            if (count > 0)
            {
                //MessageBox.Show("已经有该新闻!");
                return false;
            }
            return true;
        }

        public static void insert_news(string sql)
        {
            int i = datahelper.get_Execu(sql);
            if (i == 1)
            {
               // MessageBox.Show("插入成功！");
            }
            else
            {
                //MessageBox.Show("插入失败！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where news_name like '%{0}%'", button2.Text);
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where news_name like '%{0}%'", textBox1.Text);
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where news_name like '%{0}%'", button4.Text);
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           DateTime dt = dateTimePicker1.Value.Date;
            
            string sql_button = String.Format("select news_name,location,side,time,url  from news_table where time='{0}' order by Heat desc", Convert.ToString(dt));
            OleDbDataReader r = datahelper.get_reader(sql_button);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox11.Items.Clear();
            listBox12.Items.Clear();
            while (r.Read())
            {
                listBox1.Items.Add(Convert.ToString(r[0]).Trim('"'));
                listBox2.Items.Add(Convert.ToString(r[1]).Trim('"'));
                listBox3.Items.Add(Convert.ToString(r[2]).Trim('"'));
                string s_time = "";
                string s_time2 = Convert.ToString(r[3]).Trim('"');
                for (int i = 0; i < 9; i++)
                {
                    s_time += s_time2[i];
                }
                listBox12.Items.Add(s_time);
                listBox11.Items.Add(Convert.ToString(r[4]).Trim('"'));
                if (listBox1.Items.Count > 6)
                    break;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select news_name,url,side,Heat from news_table order by Heat desc";
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex==1)
            {
                DataSet ds = new DataSet();
                string sql = "select news_name,url,side,Heat from news_table order by Heat desc";
                OleDbDataAdapter ada = new OleDbDataAdapter(sql, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");
                ada.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                Bitmap p1 = new Bitmap("D:/result.png");
                pictureBox2.Image = p1;
            }
            if(tabControl1.SelectedIndex==3)
            {
                String sql = String.Format("Select * from zt");
                OleDbDataReader r = datahelper.get_reader(sql);

                listBox4.Items.Clear();
                while (r.Read())
                {
                    listBox4.Items.Add(Convert.ToString(r[0]) + "(关键词为:" + Convert.ToString(r[1]) + '、' + Convert.ToString(r[2]) + '、' + Convert.ToString(r[3]) + ")");
                }
            }
            if(tabControl1.SelectedIndex == 4)
            {
                int max_item = 0;
                listBox13.Items.Clear();
                listBox14.Items.Clear();
                string sql = String.Format("select id from warn order by id desc");
                OleDbDataReader r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    max_item = Convert.ToInt32(r[0]);
                }
                for (int i = 1; i <= max_item; i++)
                {
                    sql = String.Format("select id,key_w from warn where id={0}", i);
                    r = datahelper.get_reader(sql);
                    string content = "";
                    while (r.Read())
                    {
                        content += "/" + Convert.ToString(r[1]);
                    }
                    if (content != "")
                    {
                        listBox14.Items.Add(i.ToString() + '、' + content);
                    }
                }
            }
            if(tabControl1.SelectedIndex==5)
            {
                string sql = String.Format("select topic  from comments");
                OleDbDataReader r = datahelper.get_reader(sql);
                string final_topic = "";
                Random rnd = new Random();
                int index = rnd.Next(1, 1200), index_i = 0;
                while (r.Read())
                {
                    index_i++;
                    if (index == index_i)
                    {
                        final_topic = Convert.ToString(r[0]);
                        break;
                    }
                }

                toolTip1.AutoPopDelay = 10000;
                toolTip1.InitialDelay = 500;
                toolTip1.ReshowDelay = 500;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(this.chart1, "【topic】"+final_topic);

                int posi = 0, nega = 0, neutral = 0;
                sql = String.Format("select count(*)  from comments where topic='{0}' and side='积极'", final_topic);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    posi = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from comments where topic='{0}' and side='消极'", final_topic);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    nega = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from comments where topic='{0}'and side='中性'", final_topic);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    neutral = Convert.ToInt32(r[0]);
                }
                //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
               // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                List<string> xData = new List<string>() { "积极", "消极", "中性" };
                List<int> yData = new List<int>() { posi, nega, neutral };
                chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
                chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
                /***************单折线图***********************/
                sql = String.Format("select time,count(*)  from comments group by time order by time desc");
                r = datahelper.get_reader(sql);
                string[] x_name = new string[6];
                int[] y_num = new int[6];
                int index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = "";
                    string val_i= Convert.ToString(r[0]);
                    for (int i_index = 0; i_index < 9; i_index++)
                    {
                        x_name[index_3] += val_i[i_index];
                    }
                    x_name[index_3] += "    ";
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 >5)
                        break;
                }
                List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
                /***************全部情感趋势图***********************/
                sql = String.Format("select time,count(*)  from comments where side='积极' group by time order by time desc");
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = "";
                    string val_i = Convert.ToString(r[0]);
                    for (int i_index = 0; i_index < 9; i_index++)
                    {
                        x_name[index_3] += val_i[i_index];
                    }
                    x_name[index_3] += "    ";
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[0].LegendText = "积极";

                List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from comments where side='消极' group by time order by time desc");
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = "";
                    string val_i = Convert.ToString(r[0]);
                    for (int i_index = 0; i_index < 9; i_index++)
                    {
                        x_name[index_3] += val_i[i_index];
                    }
                    x_name[index_3] += "    ";
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[1].LegendText = "消极";

                List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from comments where side='中性' group by time order by time desc");
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = "";
                    string val_i = Convert.ToString(r[0]);
                    for (int i_index = 0; i_index < 9; i_index++)
                    {
                        x_name[index_3] += val_i[i_index];
                    }
                    x_name[index_3] += "    ";
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[2].LegendText = "中性";

                List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
            }
            if (tabControl1.SelectedIndex == 6)
            {
                String sql = String.Format("Select count(*) from news_table where location='网易' and side='积极'");
                OleDbDataReader r = datahelper.get_reader(sql);
                listBox7.Items.Clear();
                if (r.Read())
                {
                    listBox7.Items.Add("");
                    listBox7.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='搜狗' and side='积极'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox7.Items.Add("");
                    listBox7.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='贴吧' and side='积极'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox7.Items.Add("");
                    listBox7.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='豆瓣' and side='积极'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox7.Items.Add("");
                    listBox7.Items.Add(Convert.ToString(r[0]));
                }

                //listbox8
                sql = String.Format("Select count(*) from news_table where location='网易' and side='消极'");
                r = datahelper.get_reader(sql);
                listBox8.Items.Clear();
                if (r.Read())
                {
                    listBox8.Items.Add("");
                    listBox8.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='搜狗' and side='消极'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox8.Items.Add("");
                    listBox8.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='贴吧' and side='消极'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox8.Items.Add("");
                    listBox8.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='豆瓣' and side='消极'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox8.Items.Add("");
                    listBox8.Items.Add(Convert.ToString(r[0]));
                }
                //listbox9
                sql = String.Format("Select count(*) from news_table where location='网易'");
                r = datahelper.get_reader(sql);
                listBox9.Items.Clear();
                if (r.Read())
                {
                    listBox9.Items.Add("");
                    listBox9.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='搜狗' ");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox9.Items.Add("");
                    listBox9.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='贴吧'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox9.Items.Add("");
                    listBox9.Items.Add(Convert.ToString(r[0]));
                }

                sql = String.Format("Select count(*) from news_table where location='豆瓣'");
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    listBox9.Items.Add("");
                    listBox9.Items.Add(Convert.ToString(r[0]));
                }
                sql = String.Format("Select news_name from news_table order by time desc");
                r = datahelper.get_reader(sql);
                int i = 1;
                while(r.Read())
                {
                    listBox10.Items.Add(i.ToString()+"、"+Convert.ToString(r[0]));
                    i++;
                    if (i == 6)
                        break;
                }
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select news_name,url,side,Heat from news_table where location='贴吧' order by Heat desc";
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            p1 = new Bitmap("D:/tieba_news.png");
            pictureBox3.Image = p1;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select news_name,url,side,Heat from news_table where location='搜狗' order by Heat desc";
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            p1 = new Bitmap("D:/sougou_news.png");
            pictureBox3.Image = p1;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select news_name,url,side,Heat from news_table where location='网易' order by Heat desc";
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            p1 = new Bitmap("D:/wangyi_news.png");
            pictureBox3.Image = p1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string sql = "select news_name,url,side,Heat from news_table where location='豆瓣' order by Heat desc";
            OleDbDataAdapter ada = new OleDbDataAdapter(sql, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");
            ada.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            p1 = new Bitmap("D:/douban_news.png");
            pictureBox3.Image = p1;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string path = "bad.txt";
            FoundationHelper.FilterWord filter = new FoundationHelper.FilterWord(path);
            filter.SourctText = textBox3.Text;
            string msg = filter.Filter('*');
            textBox4.Text = msg;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            create_zt c1 = new create_zt();
            c1.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            String sql = String.Format("Select * from zt");
            string word1="", word2 = "", word3 = "",topic="";
            OleDbDataReader r = datahelper.get_reader(sql);
            listBox4.Items.Clear();
            listBox5.Items.Clear();
            while (r.Read())
            {
                topic = Convert.ToString(r[0]);
                word1 = Convert.ToString(r[1]);
                word2 = Convert.ToString(r[2]);
                word3 = Convert.ToString(r[3]);
                listBox4.Items.Add(Convert.ToString(r[0])+"(关键词为:"+ Convert.ToString(r[1])+'、'+ Convert.ToString(r[2])+ '、'+ Convert.ToString(r[3])+")");

                if (word1 != "")
                {
                    sql = String.Format("select news_name from news_table where news_name like '%{0}%'", word1);
                    OleDbDataReader r1 = datahelper.get_reader(sql);
                    if (r1.Read())
                    {
                        listBox5.Items.Add(topic + "---" + Convert.ToString(r1[0]));
                    }
                    else if(word2!="")
                    {
                        sql = String.Format("select news_name from news_table where news_name like '%{0}%'", word2);
                        r1 = datahelper.get_reader(sql);
                        if (r1.Read())
                        {
                            listBox5.Items.Add(topic + "---" + Convert.ToString(r1[0]));
                        }
                        else if(word3!="")
                        {
                            sql = String.Format("select news_name from news_table where news_name like '%{0}%'", word3);
                            r1 = datahelper.get_reader(sql);
                            if (r1.Read())
                            {
                                listBox5.Items.Add(topic + "---" + Convert.ToString(r1[0]));
                            }
                        }
                    }
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                string s = listBox4.Items[listBox4.SelectedIndex].ToString();
                string s1 = "";
                for (int i = 0; ; i++)
                {
                    if (s[i] == '(')
                        break;
                    s1 += s[i];
                }
                int del = listBox4.SelectedIndex;
                listBox4.Items.RemoveAt(del);
                try
                {
                    listBox5.Items.RemoveAt(del);
                }
                catch
                {
                    ;
                }
                String sql = String.Format("delete from zt where zt_name='{0}'", s1);
                insert_news(sql);//删除数据
            }
            catch(Exception ex)
            {
                ;
            }
            


        }

        private void listBox5_DoubleClick(object sender, EventArgs e)
        {
            string s = listBox5.Items[listBox5.SelectedIndex].ToString();
            string s1 = "";
            for (int i = 0; ; i++)
            {
                if (s[i] == '-')
                    break;
                s1 += s[i];
            }
            more m1 = new more(s1);
            m1.Show();
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            DateTime dt_l = DateTime.Now;
            string s = dt_l.ToString();
            string s2 = "";
            string s3 = "";
            for (int i = 0; i < 10; i++)
            {
                s2 += s[i];
            }
            for (int i = 10; i < s.Length; i++)
            {
                s3 += s[i];
            }
            label26.Text = s2;
            label27.Text = s3;
        }


        private void button21_Click(object sender, EventArgs e)
        {
            six_flag = 1;
            /*****************饼状图******************************/
            string sql = String.Format("select topic  from comments");
            OleDbDataReader r = datahelper.get_reader(sql);
            string final_topic = "";
            Random rnd = new Random();
            int index = rnd.Next(1, 1200), index_i = 0;
            while (r.Read())
            {
                index_i++;
                if (index == index_i)
                {
                    final_topic = Convert.ToString(r[0]);
                    break;
                }
            }


            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.chart1, "【全部】" + final_topic);

            int posi = 0, nega = 0, neutral = 0;
            sql = String.Format("select count(*)  from comments where topic='{0}' and side='积极'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                posi = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from comments where topic='{0}' and side='消极'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                nega = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from comments where topic='{0}'and side='中性'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                neutral = Convert.ToInt32(r[0]);
            }
            //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            List<string> xData = new List<string>() { "积极", "消极", "中性" };
            List<int> yData = new List<int>() { posi, nega, neutral };
            chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
            chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
            /***************全部单折线图***********************/
            sql = String.Format("select time,count(*)  from comments group by time order by time desc");
            r = datahelper.get_reader(sql);
            string[] x_name = new string[6];
            int[] y_num = new int[6];
            int index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
            /***************全部情感趋势图***********************/
            sql = String.Format("select time,count(*)  from comments where side='积极' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[0].LegendText = "积极";
            
            List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from comments where side='消极' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[1].LegendText = "消极";

            List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData3= new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from comments where side='中性' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[2].LegendText = "中性";

            List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据

        }

        private void button22_Click(object sender, EventArgs e)
        {
            six_flag = 2;
            string sql = String.Format("select news_name  from news_table where location='豆瓣'");
            OleDbDataReader r = datahelper.get_reader(sql);
            string final_topic = "";
            Random rnd = new Random();
            int index = rnd.Next(1, 14), index_i = 0;
            while (r.Read())
            {
                index_i++;
                if (index == index_i)
                {
                    final_topic = Convert.ToString(r[0]);
                    break;
                }
            }
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.chart1, "【豆瓣】" + final_topic);

            int posi = 0, nega = 0, neutral = 0;
            sql = String.Format("select count(*)  from comments where topic='{0}' and side='积极'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                posi = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from comments where topic='{0}' and side='消极'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                nega = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from comments where topic='{0}'and side='中性'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                neutral = Convert.ToInt32(r[0]);
            }
            //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            List<string> xData = new List<string>() { "积极", "消极", "中性" };
            List<int> yData = new List<int>() { posi, nega, neutral };
            chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
            chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
            /***************豆瓣单折线图***********************/
            sql = String.Format("select time,count(*)  from comments where topic in (select cstr(news_name) from news_table where location='豆瓣') group by time order by time desc");
            r = datahelper.get_reader(sql);
            string[] x_name = new string[6];
            int[] y_num = new int[6];
            int index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
            /***************全部情感趋势图***********************/
            sql = String.Format("select time,count(*)  from comments where side='积极' and topic in (select cstr(news_name) from news_table where location='豆瓣') group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[0].LegendText = "积极";

            List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from comments where side='消极' and topic in (select cstr(news_name) from news_table where location='豆瓣') group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[1].LegendText = "消极";

            List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from comments where side='中性' and topic in (select cstr(news_name) from news_table where location='豆瓣') group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[2].LegendText = "中性";

            List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据

        }

        private void tabPage7_Enter(object sender, EventArgs e)
        {

        }

        private void chart1_Enter(object sender, EventArgs e)
        {
        }

        private void button23_Click(object sender, EventArgs e)
        {
            six_flag = 3;
            string sql = String.Format("select news_name  from news_table where location='贴吧'");
            OleDbDataReader r = datahelper.get_reader(sql);
            string final_topic = "";
            Random rnd = new Random();
            int index = rnd.Next(1, 14), index_i = 0;
            while (r.Read())
            {
                index_i++;
                if (index == index_i)
                {
                    final_topic = Convert.ToString(r[0]);
                    break;
                }
            }
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.chart1, "【百度贴吧】" + final_topic);

            int posi = 0, nega = 0, neutral = 0;
            sql = String.Format("select count(*)  from comments where topic='{0}' and side='积极'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                posi = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from comments where topic='{0}' and side='消极'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                nega = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from comments where topic='{0}'and side='中性'", final_topic);
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                neutral = Convert.ToInt32(r[0]);
            }
            //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            List<string> xData = new List<string>() { "积极", "消极", "中性" };
            List<int> yData = new List<int>() { posi, nega, neutral };
            chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
            chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
            /***************贴吧***********************/
            sql = String.Format("select time,count(*)  from comments where topic in (select cstr(news_name) from news_table where location='贴吧') group by time order by time desc");
            r = datahelper.get_reader(sql);
            string[] x_name = new string[6];
            int[] y_num = new int[6];
            int index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
            /***************全部情感趋势图***********************/
            sql = String.Format("select time,count(*)  from comments where side='积极' and topic in (select cstr(news_name) from news_table where location='贴吧') group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[0].LegendText = "积极";

            List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from comments where side='消极' and topic in (select cstr(news_name) from news_table where location='贴吧') group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[1].LegendText = "消极";

            List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from comments where side='中性' and topic in (select cstr(news_name) from news_table where location='贴吧') group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[2].LegendText = "中性";

            List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
        }

        private void button25_Click(object sender, EventArgs e)
        {
            six_flag = 4;
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.chart1, "【网易新闻】");

            int posi = 0, nega = 0, neutral = 0;
            string sql = String.Format("select count(*)  from news_table where location='网易' and side='积极'");
            OleDbDataReader r = datahelper.get_reader(sql);
            if (r.Read())
            {
                posi = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from news_table where location='网易' and side='消极'");
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                nega = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from news_table where location='网易' and side='中性'");
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                neutral = Convert.ToInt32(r[0]);
            }
            //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            List<string> xData = new List<string>() { "积极", "消极", "中性" };
            List<int> yData = new List<int>() { posi, nega, neutral };
            chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
            chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
            /***************网易新闻***********************/
            sql = String.Format("select time,count(*)  from news_table where location='网易' group by time order by time desc");
            r = datahelper.get_reader(sql);
            string[] x_name = new string[6];
            int[] y_num = new int[6];
            int index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
            /***************全部情感趋势图***********************/
            sql = String.Format("select time,count(*)  from news_table where side='积极' and location='网易' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[0].LegendText = "积极";

            List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from news_table where side='消极' and location='网易' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[1].LegendText = "消极";

            List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from news_table where side='中性' and location='网易' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[2].LegendText = "中性";

            List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
        }

        private void button26_Click(object sender, EventArgs e)
        {
            six_flag = 5;
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.chart1, "【搜狗新闻】");

            int posi = 0, nega = 0, neutral = 0;
            string sql = String.Format("select count(*)  from news_table where location='搜狗' and side='积极'");
            OleDbDataReader r = datahelper.get_reader(sql);
            if (r.Read())
            {
                posi = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from news_table where location='搜狗' and side='消极'");
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                nega = Convert.ToInt32(r[0]);
            }
            sql = String.Format("select count(*)  from news_table where location='搜狗' and side='中性'");
            r = datahelper.get_reader(sql);
            if (r.Read())
            {
                neutral = Convert.ToInt32(r[0]);
            }
            //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            List<string> xData = new List<string>() { "积极", "消极", "中性" };
            List<int> yData = new List<int>() { posi, nega, neutral };
            chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
            chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
            /***************搜狗新闻***********************/
            sql = String.Format("select time,count(*)  from news_table where location='搜狗' group by time order by time desc");
            r = datahelper.get_reader(sql);
            string[] x_name = new string[6];
            int[] y_num = new int[6];
            int index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0]};
            chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
            /***************全部情感趋势图***********************/
            sql = String.Format("select time,count(*)  from news_table where side='积极' and location='搜狗' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[0].LegendText = "积极";

            List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from news_table where side='消极' and location='搜狗' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[1].LegendText = "消极";

            List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

            sql = String.Format("select time,count(*)  from news_table where side='中性' and location='搜狗' group by time order by time desc");
            r = datahelper.get_reader(sql);
            x_name = new string[6];
            y_num = new int[6];
            index_3 = 0;
            while (r.Read())
            {
                x_name[index_3] = "";
                string val_i = Convert.ToString(r[0]);
                for (int i_index = 0; i_index < 9; i_index++)
                {
                    x_name[index_3] += val_i[i_index];
                }
                x_name[index_3] += "    ";
                y_num[index_3] = Convert.ToInt32(r[1]);
                index_3++;
                if (index_3 > 5)
                    break;
            }
            chart2.Series[2].LegendText = "中性";

            List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
            List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
            chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
        }
        private void button24_Click(object sender, EventArgs e)
        {
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            string final_topic = textBox12.Text;
            toolTip1.SetToolTip(this.chart1, "【自定义】" + final_topic);
            if(six_flag==1)
            {
                int posi = 0, nega = 0, neutral = 0;
                string sql = String.Format("select count(*)  from comments where topic like '%{0}%' and side='积极'", final_topic);
                OleDbDataReader r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    posi = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from comments where topic like '%{0}%' and side='消极'", final_topic);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    nega = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from comments where topic like '%{0}%' and side='中性'", final_topic);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    neutral = Convert.ToInt32(r[0]);
                }
                //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                List<string> xData = new List<string>() { "积极", "消极", "中性" };
                List<int> yData = new List<int>() { posi, nega, neutral };
                chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
                chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
                /************单折线图**************************/
                sql = String.Format("select time,count(*)  from comments where topic like '%{0}%' group by time order by time desc", final_topic);
                r = datahelper.get_reader(sql);
                string[] x_name = new string[6];
                int[] y_num = new int[6];
                int index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = "";
                    string val_i = Convert.ToString(r[0]);
                    for (int i_index = 0; i_index < 9; i_index++)
                    {
                        x_name[index_3] += val_i[i_index];
                    }
                    x_name[index_3] += "    ";
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
                /***************全部情感趋势图***********************/
                sql = String.Format("select time,count(*)  from comments where side='积极' and topic like '%{0}%' group by time order by time desc", final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = "";
                    string val_i = Convert.ToString(r[0]);
                    for (int i_index = 0; i_index < 9; i_index++)
                    {
                        x_name[index_3] += val_i[i_index];
                    }
                    x_name[index_3] += "    ";
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[0].LegendText = "积极";

                List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from comments where side='消极' and topic like '%{0}%' group by time order by time desc", final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[1].LegendText = "消极";

                List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from comments where side='中性' and topic like '%{0}%' group by time order by time desc", final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[2].LegendText = "中性";

                List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
            }
            else if(six_flag==2 || six_flag==3)//检测上级按钮
            {
                string loc_now = "";
                if (six_flag == 2) loc_now = "豆瓣";
                if (six_flag == 3) loc_now = "贴吧";
                int posi = 0, nega = 0, neutral = 0;
                string sql = String.Format("select count(*)  from comments where topic like '%{0}%' and side='积极' and location='{1}'", final_topic, loc_now);
                OleDbDataReader r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    posi = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from comments where topic like '%{0}%' and side='消极' and location='{1}'", final_topic, loc_now);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    nega = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from comments where topic like '%{0}%' and side='中性' and location='{1}'", final_topic, loc_now);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    neutral = Convert.ToInt32(r[0]);
                }
                //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                List<string> xData = new List<string>() { "积极", "消极", "中性" };
                List<int> yData = new List<int>() { posi, nega, neutral };
                chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
                chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
                /************单折线图**************************/
                sql = String.Format("select time,count(*)  from comments where topic like '%{0}%' and location='{1}' group by time order by time desc", final_topic, loc_now);
                r = datahelper.get_reader(sql);
                string[] x_name = new string[6];
                int[] y_num = new int[6];
                int index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
                /***************全部情感趋势图***********************/
                sql = String.Format("select time,count(*)  from comments where side='积极' and location='{0}' and topic like '%{1}%' group by time order by time desc", loc_now, final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[0].LegendText = "积极";

                List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from comments where side='消极' and location='{0}' and topic like '%{1}%' group by time order by time desc", loc_now, final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[1].LegendText = "消极";

                List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from comments where side='中性' and location='{0}' and topic like '%{1}%' group by time order by time desc", loc_now, final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[2].LegendText = "中性";

                List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
            }
            else if(six_flag==4 || six_flag==5)
            {
                string loc_now = "";
                if (six_flag == 2) loc_now = "网易";
                if (six_flag == 3) loc_now = "搜狗";
                int posi = 0, nega = 0, neutral = 0;
                string sql = String.Format("select count(*)  from news_table where news_name like '%{0}%' and side='积极' and location='{1}'", final_topic, loc_now);
                OleDbDataReader r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    posi = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from news_table where news_name like '%{0}%' and side='消极' and location='{1}'", final_topic, loc_now);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    nega = Convert.ToInt32(r[0]);
                }
                sql = String.Format("select count(*)  from news_table where news_name like '%{0}%' and side='中性' and location='{1}'", final_topic, loc_now);
                r = datahelper.get_reader(sql);
                if (r.Read())
                {
                    neutral = Convert.ToInt32(r[0]);
                }
                //chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
                // chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
                List<string> xData = new List<string>() { "积极", "消极", "中性" };
                List<int> yData = new List<int>() { posi, nega, neutral };
                chart1.Series[0].Label = "#VALX;\n#PERCENT";  //VALX表示X轴的值，设置内容为百分比显示，P2为精确位数为两位小数
                chart1.Series[0].Points.DataBindXY(xData, yData); //序列数据点集合1绑定数据
                /************单折线图**************************/
                sql = String.Format("select time,count(*)  from news_table where news_name like '%{0}%' and location='{1}' group by time order by time desc", final_topic, loc_now);
                r = datahelper.get_reader(sql);
                string[] x_name = new string[6];
                int[] y_num = new int[6];
                int index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                List<string> xData1 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData1 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart3.Series[0].Points.DataBindXY(xData1, yData1); //序列数据点集合1绑定数据
                /***************全部情感趋势图***********************/
                sql = String.Format("select time,count(*)  from news_table where side='积极' and location='{0}' and news_name like '%{1}%' group by time order by time desc", loc_now, final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[0].LegendText = "积极";

                List<string> xData2 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData2 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[0].Points.DataBindXY(xData2, yData2); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from news_table where side='消极' and location='{0}'  and news_name like '%{1}%' group by time order by time desc", loc_now, final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[1].LegendText = "消极";

                List<string> xData3 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData3 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[1].Points.DataBindXY(xData3, yData3); //序列数据点集合1绑定数据

                sql = String.Format("select time,count(*)  from news_table where side='中性' and location='{0}' and news_name like '%{1}%' group by time order by time desc", loc_now, final_topic);
                r = datahelper.get_reader(sql);
                x_name = new string[6];
                y_num = new int[6];
                index_3 = 0;
                while (r.Read())
                {
                    x_name[index_3] = Convert.ToString(r[0]);
                    y_num[index_3] = Convert.ToInt32(r[1]);
                    index_3++;
                    if (index_3 > 5)
                        break;
                }
                chart2.Series[2].LegendText = "中性";

                List<string> xData4 = new List<string>() { x_name[5], x_name[4], x_name[3], x_name[2], x_name[1], x_name[0] };
                List<int> yData4 = new List<int>() { y_num[5], y_num[4], y_num[3], y_num[2], y_num[1], y_num[0] };
                chart2.Series[2].Points.DataBindXY(xData4, yData4); //序列数据点集合1绑定数据
            }

            

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {

            max_pic m1 = new max_pic(p1);
            m1.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if(textBox5.Text=="")
            {
                MessageBox.Show("不可添加空字符串！");
                textBox5.Text = "";
            }
            else
            {
                listBox13.Items.Add(textBox5.Text);
                textBox5.Text = "";
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                string s = Convert.ToString(listBox14.Items[listBox14.SelectedIndex]);
                int i = Convert.ToInt32(s[0]-48);
                String sql = String.Format("delete from warn where id={0}",i);
                insert_news(sql);//删除数据
                listBox14.Items.RemoveAt(listBox14.SelectedIndex);

                string sql_warn = String.Format("select id  from warn order by id desc");
                OleDbDataReader r_warn = datahelper.get_reader(sql_warn);
                if (r_warn.Read())
                {
                    warn_flag = Convert.ToInt32(r_warn[0]);
                }
                else
                {
                    warn_flag = 0;
                }
            }
            catch
            {
                ;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            int index = 0,flag = 0; ;
            if (textBox9.Text == "")
            {
                MessageBox.Show("邮箱不可为空！");
            }
            else if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("时间未设置！");
            }
            else
            {
                MessageBox.Show("设置完成!");
                string sql = String.Format("select key_w from warn where id<>0");
                OleDbDataReader r = datahelper.get_reader(sql);
                while(r.Read())
                {
                    main_word = Convert.ToString(r[0]);
                    string sql1 = String.Format("select news_name,location from news_table where news_name like '%{0}%'", main_word);
                    OleDbDataReader r1 = datahelper.get_reader(sql1);
                    if (r1.Read())
                    {
                        my_email = textBox9.Text;
                        content = Convert.ToString(r1[0])+Convert.ToString(r1[1]);
                        flag = 1;
                    }
                }

            }
            if(flag==1)
            {
                email_flag = 1;
                timer2.Enabled = true;
                if (comboBox1.SelectedIndex == 0)
                    this.timer2.Interval = 3600000;
                else if (comboBox1.SelectedIndex == 1)
                    this.timer2.Interval = 86400000;
                else if (comboBox1.SelectedIndex == 2)
                    this.timer2.Interval = 172800000;
                else if (comboBox1.SelectedIndex == 3)
                    this.timer2.Interval = 604800000;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                listBox13.Items.RemoveAt(listBox13.SelectedIndex);
            }
            catch
            {
                ;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string url_text = port_get(textBox2.Text);
            string new_text = "";
            string path = "bad.txt";
            FoundationHelper.FilterWord filter = new FoundationHelper.FilterWord(path);
            filter.SourctText = url_text;
            for(int i=0;i< url_text.Count();i++)
            {
                char ch = url_text[i];
                if (64 < ch && ch < 123)
                    new_text +="";
                else
                    new_text += url_text[i];
            }
            MessageBox.Show(Convert.ToString(url_text.Count()));
            MessageBox.Show(Convert.ToString(new_text.Count()));
            string msg = filter.Filter('*');
            if (msg == "")
                MessageBox.Show("无敏感字符");
            textBox4.Text = msg;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            int max_item = 0;
            if(listBox13.Items.Count==0)
            {
                MessageBox.Show("当前没有字符串");
                return;
            }
            warn_flag++;
            foreach (var item in listBox13.Items)
            {
                String sql_insert = String.Format("insert into warn  values ('{0}','{1}')",warn_flag,Convert.ToString(item));
                insert_news(sql_insert);
            }
            listBox13.Items.Clear();
            listBox14.Items.Clear();
            string sql = String.Format("select id from warn order by id desc");
            OleDbDataReader r = datahelper.get_reader(sql);
            if(r.Read())
            {
                max_item = Convert.ToInt32(r[0]);
            }
            for(int i=1;i<=max_item;i++)
            {
                sql = String.Format("select id,key_w from warn where id={0}",i);
                r = datahelper.get_reader(sql);
                string content = "";
                while(r.Read())
                {
                    content += "/"+Convert.ToString(r[1]);
                }
                if(content!="")
                {
                    listBox14.Items.Add(i.ToString()+'、'+content);
                }
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            url = string.Format("http://127.0.0.1:5088/?name=send_email()&od={0}&td={1}",my_email, content);
            port_get(url);
        }
    }
}

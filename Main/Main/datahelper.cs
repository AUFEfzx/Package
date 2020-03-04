using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace Main
{
    class datahelper
    {
        static OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=./news_data.mdb");//登录语句
        public static OleDbDataReader get_reader(string sql)
        {
            try
            {
                try 
                {
                    con.Open();
                }
                catch(Exception ex)
                {
                    ;
                }
                
                OleDbCommand com = new OleDbCommand(sql, con);
                //return com.ExecuteReader();
                return com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public static int get_Execu(string sql)
        {
            try
            {
                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    ;
                }
                OleDbCommand com = new OleDbCommand(sql, con);
                return com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return -1;
            }

        }
    }
}

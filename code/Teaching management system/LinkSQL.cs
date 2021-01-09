using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teaching_management_system
{
    class LinkSQL
    {
        //查询，返回DataTable
        public DataTable QuerySQL(string sql)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
                conn.Open();
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    try
                    {
                        da.Fill(ds);
                    }
                    catch
                    {
                        
                    }
                }
            }
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables[0].Copy();
            }
            catch
            {
                
            }
            return dt;
        }

        //添加数据
        public void AddSQL(string sql)
        {
            //连接数据库
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
            conn.Open();

            //将数据存入数据库
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();

            //断开连接，释放资源
            conn.Close();
        }

        //修改数据
        public void AlterSQL(string sql)
        {
            // 连接数据库
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"server=DESKTOP-5T68OT4\SQLEXPRESS;database=jxgl;integrated security=true";
            conn.Open();

            //将数据存入数据库
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();

            //断开连接，释放资源
            conn.Close();
        }
    }
}

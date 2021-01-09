using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teaching_management_system
{
    public partial class QueryGrade : Form
    {
        LinkSQL linkSql;
        DataTable dt = new DataTable();

        public QueryGrade()
        {
            InitializeComponent();
            timer1.Interval = 1000; // 设置时间间隔为1000ms
            timer1.Start();// 启动计时器
        }

        private void QueryGrade_Load(object sender, EventArgs e)
        {
            linkSql = new LinkSQL();
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            string sql = string.Format("select sno as '学号',cno as '课程',grade as '成绩' from dbo.sc where sno = '{0}'",Program.loginId);
            this.dataGridView1.DataSource = linkSql.QuerySQL(sql);

            //学分统计
            string sql2 = string.Format("select SUM(credit) from class where cno in (select cno from sc where sno = '{0}' and grade >= 60)", Program.loginId);
            dt = linkSql.QuerySQL(sql2);
            int loginCredit = 0;
            try
            {
                loginCredit = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            catch
            {

            }
            label1.Text = string.Format("您已选修学分为{0}学分，最高可选修120学分", loginCredit);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            QueryGrade_Load(sender, e);
            dataGridView1.Refresh();
        }
    }
}

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
    public partial class ElectiveCourse : Form
    {
        LinkSQL linkSql = new LinkSQL();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        List<string> cnoList = new List<string>();

        private bool isFind = false;

        public ElectiveCourse()
        {
            InitializeComponent(); 
        }

        private void ElectiveCourse_Load(object sender, EventArgs e)
        {
            //获取全部课程
            string sql1 = "select * from class";
            dt1 = linkSql.QuerySQL(sql1);

            //创建表头
            this.listView1.Columns.Clear(); //先清除再添加保证数据的一致性
            this.listView1.Columns.Add("", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("课程号", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("课程名称", 150, HorizontalAlignment.Left);
            this.listView1.Columns.Add("学时", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("课程类型", 150, HorizontalAlignment.Left);
            this.listView1.Columns.Add("学分", 60, HorizontalAlignment.Left);

            //绑定数据
            foreach (DataRow row in dt1.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(row["cno"].ToString());
                item.SubItems.Add(row["cname"].ToString());
                item.SubItems.Add(row["chours"].ToString());
                item.SubItems.Add(row["ctype"].ToString());
                item.SubItems.Add(row["credit"].ToString());
                listView1.Items.Add(item);
            }

            //查询已选课程
            string sql2;
            sql2 = string.Format("select cno from sc where sno = '{0}'", Program.loginId);
            dt2 = linkSql.QuerySQL(sql2);

            //移除已选课程
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                for(int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (listView1.Items[i].SubItems[1].Text == dt2.Rows[j]["cno"].ToString())
                    {
                        listView1.Items.RemoveAt(i);
                    }
                }
            }

            //学分统计
            string sql3 = string.Format("select SUM(credit) from class where cno in (select cno from sc where sno = '{0}' and grade >= 60)",Program.loginId);
            dt3 = linkSql.QuerySQL(sql3);
            int loginCredit = 0;
            try
            {
                loginCredit = Convert.ToInt32(dt3.Rows[0][0].ToString());
            }
            catch
            {

            }
            label1.Text = string.Format("您已选修学分为{0}学分，最高可选修120学分", loginCredit);
            //如果学分大于120，则不能选课
            if(loginCredit > 120)
            {
                //将复选框关闭
                listView1.CheckBoxes = false;
            }

            //选课人数统计
            string sql4 = "select sc.cno,count(distinct sno) as snoCount,count(distinct tno) as tnoCount from sc,tc where sc.cno = tc.cno group by sc.cno";
            dt4 = linkSql.QuerySQL(sql4);
            foreach(DataRow dr in dt4.Rows)
            {
                //选课人数小于教师数*15，不开课
                if (Convert.ToInt32(dr["snoCount"].ToString()) < 15 * Convert.ToInt32(dr["tnoCount"].ToString()))
                {
                    Program.startClass = false;
                }
                //选课人数大于教师数*50，无法选课
                else if (Convert.ToInt32(dr["snoCount"].ToString()) > 50 * Convert.ToInt32(dr["tnoCount"].ToString()))
                {
                    cnoList.Add(dr["cno"].ToString()); //记录下课程号
                }
                //正常选课，开课
                else
                {
                    Program.startClass = true;
                }
            }
            //移除选课人数已满的课程
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                for (int j = 0; j < cnoList.Count; j++)
                {
                    if (listView1.Items[i].SubItems[1].Text == cnoList[j])
                    {
                        listView1.Items.RemoveAt(i);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Checked)
                {
                    sql = string.Format("insert into sc(sno, cno) values('{0}','{1}')", Program.loginId, listView1.Items[i].SubItems[1].Text);
                    linkSql.AddSQL(sql);
                    isFind = true;
                    listView1.Items.RemoveAt(i);
                }
            }
            if (isFind)
            {
                MessageBox.Show("选课成功！");
            }
        }

        //取消选择
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].Checked = false;
            }
        }
    }
}

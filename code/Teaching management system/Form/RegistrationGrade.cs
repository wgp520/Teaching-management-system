﻿using System;
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
    public partial class RegistrationGrade : Form
    {
        LinkSQL linkSQL = new LinkSQL();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

        public RegistrationGrade()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string sql = string.Format("select dbo.sc.sno as '学号',sname as '姓名',grade as '成绩' from dbo.student,dbo.sc where dbo.student.sno = dbo.sc.sno and cno = '{0}'", comboBox2.SelectedValue);
            dt1 = linkSQL.QuerySQL(sql);
            this.dataGridView1.DataSource = dt1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string SCsno;
            string SCgrade;
            string sql;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                SCsno = dt1.Rows[i]["学号"].ToString();
                SCgrade = dt1.Rows[i]["成绩"].ToString();
                sql = string.Format("update dbo.sc set grade = {1} where sno = '{0}' and cno = '{2}'", SCsno, SCgrade, comboBox2.SelectedValue);
                linkSQL.AlterSQL(sql);
            }
            MessageBox.Show("保存成功！");
        }

        private void RegistrationGrade_Load(object sender, EventArgs e)
        {
            string sql = string.Format("select tc.cno,cname from tc,class where tc.cno = class.cno and tno = '{0}'",Program.loginId);
            dt2 = linkSQL.QuerySQL(sql);
            comboBox2.DataSource = dt2; //将表绑定到控件
            comboBox2.DisplayMember = "cname";//定义要显示的内容为列名为x的内容
            comboBox2.ValueMember = "cno";//定义要映射的值为y的值
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
